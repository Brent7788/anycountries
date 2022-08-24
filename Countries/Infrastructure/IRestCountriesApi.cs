using Countries.Application.RestCountriesDtos;

namespace Countries.Infrastructure;

public interface IRestCountriesApi
{
    Task<List<CountryDto>> GetAll();
    Task<List<CountryDto>> GetCountriesByName(string name);
    Task<RegionDto> GetCountriesByRegion(string region);
    Task<SubRegionDto> GetCountriesBySubRegion(string subRegion);
}