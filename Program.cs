using System;
using System.Threading.Tasks;

namespace WeatherAPI
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Enter the name of the city:");
			string city = Console.ReadLine();

			WeatherAPI weatherApi = new WeatherAPI();
			await weatherApi.GetWeatherDataAsync(city);
		}
	}
}
