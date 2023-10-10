using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI
{
	internal class WeatherAPI
	{
		private const string ApiKey = "0b5c5355060bd5c2de663049f8306d57"; // Your OpenWeatherMap API key
		private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

		public async Task GetWeatherDataAsync(string city)
		{
			bool retry = false;
			do
			{
				Console.WriteLine("Enter the name of the city:");
				city = Console.ReadLine();

				using (HttpClient client = new HttpClient())
				{
					string apiUrl = $"{BaseUrl}?q={city}&appid={ApiKey}&units=imperial";

					try
					{
						HttpResponseMessage response = await client.GetAsync(apiUrl);

						if (response.IsSuccessStatusCode)
						{
							string json = await response.Content.ReadAsStringAsync();
							JObject weatherData = JObject.Parse(json);

							Console.WriteLine($"Weather in {city}:");
							Console.WriteLine($"Temperature: {weatherData["main"]["temp"]}°F");
							Console.WriteLine($"Description: {weatherData["weather"][0]["description"]}");
							Console.WriteLine($"Wind Speed: {weatherData["wind"]["speed"]} mph");
							Console.WriteLine($"Visibility: {weatherData["visibility"]} meters");

							retry = false; // Successfully fetched data, exit the loop
						}
						else
						{
							Console.WriteLine("Error: Unable to fetch weather data.");
							Console.WriteLine("Do you want to try again? (yes/no)");
							string retryInput = Console.ReadLine().Trim().ToLower();
							retry = (retryInput == "yes");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"An error occurred: {ex.Message}");
					}
				}
			} while (retry);
		}
	}
}
