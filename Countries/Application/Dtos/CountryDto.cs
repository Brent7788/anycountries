namespace Countries.Application.Dtos;

public class CountryDto
{
    public CountryNameDto Name { get; init; }
    public string Region { get; init; }
    public string Subregion { get; init; }
    public string[] Capital { get; init; }
    public Dictionary<string, CurrencyDto> Currencies { get; init; }
    public Dictionary<string, string> Languages { get; init; }
    public List<string> OfficialCurrencies { get; set; }
    public List<string> OfficialLanguages { get; set; }
    public long Population { get; init; }
    public string[] Borders { get; init; }

    public CountryDto()
    {
        OfficialCurrencies = new List<string>();
        OfficialLanguages = new List<string>();
    }
}