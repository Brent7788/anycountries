namespace Countries.Application.Dtos;

public class SubRegionDto
{
    public string SubRegion { get; set; }
    public long Population { get; set; }
    public string Region { get; set; }
    public List<CountryDto> Countries { get; set; }
}