using Countries.Application.Dtos;

namespace Countries.Infrastructure;

public interface IRestCountriesApi
{
    Task<List<CountryDto>> GetAll();
    Task<CountryDto> GetCountryByName(string name);
    Task<RegionDto> GetCountriesByRegion(string region);
    Task<SubRegionDto> GetCountriesBySubRegion(string subRegion);
}