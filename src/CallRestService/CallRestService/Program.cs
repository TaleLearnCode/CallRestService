using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CallRestService
{
	partial class Program
	{

		private static readonly HttpClient _httpClient = new HttpClient();


		static async Task Main(string[] args)
		{
			var exit = false;
			while (!exit)
			{
				switch (ProvideMenuOptions())
				{
					case MenuOption.CurrentTemperature:
						Console.WriteLine($"The current temperature is {await GetCurrentTemperature()}");
						Console.WriteLine("Press any key continue...");
						Console.ReadKey();
						break;
					case MenuOption.Exit:
						exit = true;
						break;
				}
			}



		}

		private static MenuOption ProvideMenuOptions()
		{

			ConsoleColor foregroundColor = Console.ForegroundColor;

			int returnValue = -1;

			int minDemoOptionValue = (int)Enum.GetValues(typeof(MenuOption)).Cast<MenuOption>().First();
			int maxDemoOptionValue = (int)Enum.GetValues(typeof(MenuOption)).Cast<MenuOption>().Last();
			while (returnValue < minDemoOptionValue || returnValue > maxDemoOptionValue)
			{
				Console.Clear();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(@"_________            .___       .____                .__              .__.__  .__          ");
				Console.WriteLine(@"\_   ___ \  ____   __| _/____   |    |    ____  __ __|__| _________  _|__|  | |  |   ____  ");
				Console.WriteLine(@"/    \  \/ /  _ \ / __ |/ __ \  |    |   /  _ \|  |  \  |/  ___/\  \/ /  |  | |  | _/ __ \ ");
				Console.WriteLine(@"\     \___(  <_> ) /_/ \  ___/  |    |__(  <_> )  |  /  |\___ \  \   /|  |  |_|  |_\  ___/ ");
				Console.WriteLine(@" \______  /\____/\____ |\___  > |_______ \____/|____/|__/____  >  \_/ |__|____/____/\___  >");
				Console.WriteLine(@"        \/            \/    \/          \/                   \/                         \/ "); Console.WriteLine();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine("Choose the demo to run:");
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("\t [1]  Get Current Temperature");
				Console.WriteLine("\t [2]  Convert Temperature");
				Console.WriteLine("\t[ESC] Exit demo");
				var keyPress = Console.ReadKey(true);
				switch (keyPress.Key)
				{
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						returnValue = (int)MenuOption.CurrentTemperature;
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						returnValue = (int)MenuOption.ConvertTemperature;
						break;
					case ConsoleKey.Escape:
						returnValue = (int)MenuOption.Exit;
						break;
				}
			}

			Console.ForegroundColor = foregroundColor;
			return (MenuOption)returnValue;

		}

		private static TemperatureUnit GetTemperatureUnit()
		{
			int returnValue = -1;

			int minTemperatureUnitValue = (int)Enum.GetValues(typeof(TemperatureUnit)).Cast<TemperatureUnit>().First();
			int maxTemperatureUnitValue = (int)Enum.GetValues(typeof(TemperatureUnit)).Cast<TemperatureUnit>().Last();
			while (returnValue < minTemperatureUnitValue + 1 || returnValue > maxTemperatureUnitValue)
			{
				Console.WriteLine("Choose the temperature unit to return:");
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("\t [1]  Kelvin");
				Console.WriteLine("\t [2]  Fahrenheit");
				Console.WriteLine("\t [3]  Celsius");
				var keyPress = Console.ReadKey(true);
				return keyPress.Key switch
				{
					ConsoleKey.D1 or ConsoleKey.NumPad1 => TemperatureUnit.Kelvin,
					ConsoleKey.D2 or ConsoleKey.NumPad2 => TemperatureUnit.Fahrenheit,
					ConsoleKey.D3 or ConsoleKey.NumPad3 => TemperatureUnit.Celsius,
					_ => TemperatureUnit.Unknown
				};

			}

			return (TemperatureUnit)returnValue;

		}


		// TODO: Make unit selectable

		private static double ConvertKelvinToFahrenheit(double kelvin)
		{
			return kelvin * 1.8 - 459.67;
		}

		private static double ConvertKelvinToCelsius(double kelvin)
		{
			return kelvin * 1.8 + 32;
		}

		private static async Task<string> GetCurrentTemperature()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Location: ");
			string location = Console.ReadLine();
			TemperatureUnit temperatureUnit = GetTemperatureUnit();
			WeatherResults weatherResults = await GetAPIData(location, temperatureUnit);

			double temperature = weatherResults.CurrentConditions.Temperature;
			return temperatureUnit switch
			{
				TemperatureUnit.Celsius => $"{temperature} C",
				TemperatureUnit.Fahrenheit => $"{temperature} F",
				TemperatureUnit.Kelvin => $"{temperature} K",
				_ => temperature.ToString()
			};

		}

		private static async Task<WeatherResults> GetAPIData(string location, TemperatureUnit temperatureUnit)
		{
			string units = temperatureUnit switch
			{
				TemperatureUnit.Celsius => "metric",
				TemperatureUnit.Fahrenheit => "imperial",
				_ => "standard",
			};

			string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={Environment.GetEnvironmentVariable("OpenWeatherAPI")}&units={units}";

			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return await _httpClient.GetFromJsonAsync<WeatherResults>(apiUrl);
		

		}


	}


}
