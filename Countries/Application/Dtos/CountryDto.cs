namespace Countries.Application.RestCountriesDtos;

public class CountryDto
{
    public CountryNameDto Name { get; init; }
    public string Region { get; init; }
    public string Subregion { get; init; }
    public string[] Capital { get; init; }
    public Dictionary<string, object> Currencies { get; init; }
    public Dictionary<string, string> Languages { get; init; }
    public long Population { get; init; }
    public string[] Borders { get; init; }
}