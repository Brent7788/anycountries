using System.Text.Json;
using Countries.Application.RestCountriesDtos;

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

    //TODO Make more DRY
    
    public async Task<List<CountryDto>> GetAll()
    {
        var responseMessage = await _client.GetAsync("all");

        if (!responseMessage.IsSuccessStatusCode)
            return EmptyDtos;

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);
        
        return countryDtos ?? EmptyDtos;
    }

    public async Task<List<CountryDto>> GetCountriesByName(string name)
    {
        var responseMessage = await _client.GetAsync($"name/{name}");

        if (!responseMessage.IsSuccessStatusCode)
            return EmptyDtos;

        var readAsString= await responseMessage.Content.ReadAsStringAsync();

        var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(readAsString, Options);
        
        return countryDtos ?? EmptyDtos;
    }

    public async Task<RegionDto> GetCountriesByRegion(string region)
    {
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
        
        return regionDto;
    }

    public async Task<SubRegionDto> GetCountriesBySubRegion(string subRegion)
    {
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
        
        return subRegionDto;
    }
}