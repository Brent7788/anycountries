using Countries.Application.Dtos;

namespace Countries.Application;

public static class CachingLayer
{
    private static readonly List<CountryDto> AllCountries = new();
    private static readonly Dictionary<string, RegionDto> RegionDtos = new();
    private static readonly Dictionary<string, SubRegionDto> SubRegionDtos = new();

    public static void SetAllCountries(List<CountryDto> countryDtos)
    {
        if (AllCountriesSet()) return;
        
        foreach (var countryDto in countryDtos)
        {
            AllCountries.Add(countryDto); 
        }
    }

    public static List<CountryDto> GetAllCountries()
    {
        return AllCountries;
    }

    public static bool AllCountriesSet()
    {
        return AllCountries.Any();
    }

    public static void SetRegion(RegionDto regionDto)
    {
        if (AnyRegionByName(regionDto.Name))
            return;
        
        RegionDtos.Add(regionDto.Name, regionDto);
    }

    public static bool AnyRegionByName(string name)
    {
        return RegionDtos.Any(r => r.Key == name);
    }

    public static RegionDto GetRegionByName(string name)
    {
        return RegionDtos.GetValueOrDefault(name) ?? throw new Exception("Cannot find region");
    }
    
    public static void SetSubregion(SubRegionDto subRegionDto)
    {
        if (AnySubregionByName(subRegionDto.SubRegion))
            return;
        
        SubRegionDtos.Add(subRegionDto.SubRegion, subRegionDto);
    }

    public static bool AnySubregionByName(string name)
    {
        return SubRegionDtos.Any(r => r.Key == name);
    }

    public static SubRegionDto GetSubregionByName(string name)
    {
        return SubRegionDtos.GetValueOrDefault(name) ?? throw new Exception("Cannot find subregion");
    }
}