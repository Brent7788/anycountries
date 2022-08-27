namespace Countries.Application.Dtos;

public class RegionDto
{
    public string Name { get; set; }
    public long Population { get; set; }
    public List<CountryDto> Countries { get; set; }

    public RegionDto()
    {
        Countries = new List<CountryDto>();
    }
}

