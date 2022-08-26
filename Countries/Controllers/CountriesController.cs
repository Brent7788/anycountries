using Countries.Application.Dtos;
using Countries.Application.RestCountriesDtos;
using Countries.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly IRestCountriesApi _restCountriesApi;
    //TODO Log errors

    public CountriesController(IRestCountriesApi restCountriesApi)
    {
        _restCountriesApi = restCountriesApi;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CountryDto>>> GetAll()
    {
        try
        {
            return await _restCountriesApi.GetAll();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Oops! Something went wrong"); 
        }
    }
    
    [HttpGet("name/{name}")]
    public async Task<ActionResult<CountryDto>> GetCountriesByName(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return new BadRequestObjectResult("Please provide country name or symbol");
        
            return await _restCountriesApi.GetCountryByName(name);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Oops! Something went wrong"); 
        }
    }
    
    [HttpGet("region/{region}")]
    public async Task<ActionResult<RegionDto>> GetCountriesByRegion(string region)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(region))
                return new BadRequestObjectResult("Please provide region");
        
            return await _restCountriesApi.GetCountriesByRegion(region);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Oops! Something went wrong"); 
        }
    }
    
    [HttpGet("subregion/{subregion}")]
    public async Task<ActionResult<SubRegionDto>> GetCountriesBySubRegion(string subregion)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(subregion))
                return new BadRequestObjectResult("Please provide Subregion");
        
            return await _restCountriesApi.GetCountriesBySubRegion(subregion);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Oops! Something went wrong"); 
        }
    }
}