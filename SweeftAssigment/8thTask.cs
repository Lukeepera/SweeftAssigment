using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class CountryInfo
{
    public Name Name { get; set; }
    public string Region { get; set; }
    public string Subregion { get; set; }
    public double[] Latlng { get; set; }
    public double Area { get; set; }
    public long Population { get; set; }
}

public class Name
{
    public string Common { get; set; }
}

public class CountryDataGenerator
{

    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task GenerateCountryDataFiles()
    {
        var response = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");

        var countryList = JsonSerializer.Deserialize<CountryInfo[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        string directoryPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\CountriesData"));
        Directory.CreateDirectory(directoryPath);

        foreach (var country in countryList)
        {
            if (country == null || country.Name == null) continue;

            string fileName = Path.Combine(directoryPath, $"{country.Name.Common}.txt");

            await File.WriteAllTextAsync(fileName,
                $"Region: {country.Region}\n" +
                $"Subregion: {country.Subregion}\n" +
                $"Latitude, Longitude: {string.Join(", ", country.Latlng ?? new double[0])}\n" +
                $"Area: {country.Area} sq km\n" +
                $"Population: {country.Population}\n");
        }
    }
}
