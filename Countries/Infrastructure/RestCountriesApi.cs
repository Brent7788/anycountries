using System.Text.Json;
using Countries.Application;
using Countries.Application.Dtos;

namespace Countries.Infrastructure;

public class RestCountriesApi : IRestCountriesApi
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly List<CountryDto> EmptyDtos = new();

    public RestCountriesApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CountryDto>> GetAll()
    {
        if (CachingLayer.AllCountriesSet())
            return CachingLayer.GetAllCountries();
        
        var responseMessage = await _client.GetAsync("all");

        if (!responseMessage.IsSuccessStatusCode)
            return EmptyDtos;

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);

        if (countryDtos is null) return EmptyDtos;

        PopulateOfficialCurrenciesAndLanguages(countryDtos);
        
        CachingLayer.SetAllCountries(countryDtos);

        return countryDtos;
    }

    public async Task<CountryDto> GetCountryByName(string name)
    {
        var responseMessage = await _client.GetAsync($"name/{name}");

        if (!responseMessage.IsSuccessStatusCode)
            return new CountryDto();

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);
        
        if (countryDtos is null) return  new CountryDto();

        PopulateOfficialCurrenciesAndLanguages(countryDtos);
        
        return countryDtos?.FirstOrDefault() ?? new CountryDto();
    }
    
    private void PopulateOfficialCurrenciesAndLanguages(List<CountryDto> countryDtos)
    {
        foreach (var countryDto in countryDtos)
        {
            if (countryDto.Currencies is null || countryDto.Languages is null) continue;
            
            foreach (var countryDtoCurrency in countryDto.Currencies)
            {
                countryDto.OfficialCurrencies.Add(countryDtoCurrency.Value.Name);   
            }

            foreach (var countryDtoLanguage in countryDto.Languages)
            {
                countryDto.OfficialLanguages.Add(countryDtoLanguage.Value);
            }
        }
    }

    public async Task<RegionDto> GetCountriesByRegion(string region)
    {
        if (CachingLayer.AnyRegionByName(region))
            return CachingLayer.GetRegionByName(region);
        
        var regionDto = new RegionDto();
        
        var responseMessage = await _client.GetAsync($"region/{region}");

        if (!responseMessage.IsSuccessStatusCode)
            return regionDto;

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);

        if (countryDtos is null)
            return regionDto;

        regionDto.Name = region.Replace("%20", " ");
        
        foreach (var countryDto in countryDtos)
        {
            regionDto.Population += countryDto.Population;
        }
        
        regionDto.Countries = countryDtos;
        
        CachingLayer.SetRegion(regionDto);
        
        return regionDto;
    }

    public async Task<SubRegionDto> GetCountriesBySubRegion(string subRegion)
    {
        if (CachingLayer.AnySubregionByName(subRegion))
            return CachingLayer.GetSubregionByName(subRegion);
        
        var subRegionDto = new SubRegionDto();
        
        var responseMessage = await _client.GetAsync($"subregion/{subRegion}");

        if (!responseMessage.IsSuccessStatusCode)
            return subRegionDto;

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);
        
        if (countryDtos is null)
            return subRegionDto;

        subRegionDto.SubRegion = subRegion.Replace("%20", " ");
        
        foreach (var countryDto in countryDtos)
        {
            subRegionDto.Population += countryDto.Population;

            if (string.IsNullOrEmpty(subRegionDto.Region))
                subRegionDto.Region = countryDto.Region;
        }

        subRegionDto.Countries = countryDtos;
        
        CachingLayer.SetSubregion(subRegionDto);
        
        return subRegionDto;
    }
}