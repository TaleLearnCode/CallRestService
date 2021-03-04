using CallRestService.APIModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CallRestService
{
	public class Program
	{

		private static readonly HttpClient _httpClient = new HttpClient();
		private const string _unableToConvertTemperature = "Unable to convert temperature";

		static async Task Main(string[] args)
		{
			var exit = false;
			while (!exit)
			{
				switch (ProvideMenuOptions())
				{
					case MenuOption.CurrentTemperature:
						string results = await GetCurrentTemperature();
						if (results != default)
							Console.WriteLine($"The current temperature is {results}");
						else
							PrintErrorMessage("Unable to retrieve weather data...");
						Console.WriteLine("Press any key continue...");
						Console.ReadKey();
						break;
					case MenuOption.ConvertTemperature:
						string convertResults = ConvertTemperature();
						if (convertResults != _unableToConvertTemperature)
							Console.WriteLine($"Converted temperature is {convertResults}");
						else
							PrintErrorMessage(convertResults);
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
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.WriteLine("Choose the demo to run:");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("\t [1]  Get Current Temperature");
				Console.WriteLine("\t [2]  Convert Temperature");
				Console.WriteLine("\t[ESC] Exit demo");
				Console.ForegroundColor = foregroundColor;
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

		private static TemperatureUnit GetTemperatureUnit(string message)
		{
			int returnValue = -1;
			ConsoleColor foregroundColor = Console.ForegroundColor;

			int minTemperatureUnitValue = (int)Enum.GetValues(typeof(TemperatureUnit)).Cast<TemperatureUnit>().First();
			int maxTemperatureUnitValue = (int)Enum.GetValues(typeof(TemperatureUnit)).Cast<TemperatureUnit>().Last();
			while (returnValue < minTemperatureUnitValue + 1 || returnValue > maxTemperatureUnitValue)
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine(message);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("\t [1]  Kelvin");
				Console.WriteLine("\t [2]  Fahrenheit");
				Console.WriteLine("\t [3]  Celsius");
				Console.ForegroundColor = foregroundColor;
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

		private static async Task<string> GetCurrentTemperature()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Location: ");
			string location = Console.ReadLine();
			TemperatureUnit temperatureUnit = GetTemperatureUnit("Choose the temperature unit to return:");

			Console.WriteLine();
			Console.WriteLine("Retrieving weather data...");
			WeatherResults weatherResults = await GetAPIData(location, temperatureUnit);

			if (weatherResults != default)
			{
				double temperature = weatherResults.CurrentConditions.Temperature;
				return temperatureUnit switch
				{
					TemperatureUnit.Celsius => $"{temperature}° C",
					TemperatureUnit.Fahrenheit => $"{temperature}° F",
					TemperatureUnit.Kelvin => $"{temperature} K",
					_ => temperature.ToString()
				};

			}
			else
			{
				return default;
			}
		}

		private static string ConvertTemperature()
		{

			Console.WriteLine();
			Console.WriteLine();
			TemperatureUnit originTemperatureUnit = GetTemperatureUnit("Origin Temperature Unit: ");
			TemperatureUnit destinationTemperatureUnit = GetTemperatureUnit("Destination Temperature Unit: ");
			Console.WriteLine();
			Console.Write("Value to Convert: ");
			string inputToConvert = Console.ReadLine();
			// TODO: Add input validation

			double valueToConvert = Convert.ToDouble(inputToConvert);
			switch (originTemperatureUnit)
			{
				case TemperatureUnit.Kelvin:
					return destinationTemperatureUnit switch
					{
						TemperatureUnit.Celsius => $"{ConvertKelvinToCelsius(valueToConvert):0.##}° C",
						TemperatureUnit.Fahrenheit => $"{ConvertKelvinToFahrenheit(valueToConvert):0.##}° F",
						TemperatureUnit.Kelvin => $"{inputToConvert} K",
						_ => _unableToConvertTemperature
					};
				case TemperatureUnit.Celsius:
					return destinationTemperatureUnit switch
					{
						TemperatureUnit.Celsius => $"{inputToConvert}° C",
						TemperatureUnit.Fahrenheit => $"{ConvertCelsiusToFahrenheit(valueToConvert):0.##}° F",
						TemperatureUnit.Kelvin => $"{ConvertCelsiusToKelvin(valueToConvert):0.##} K",
						_ => _unableToConvertTemperature
					};
				case TemperatureUnit.Fahrenheit:
					return destinationTemperatureUnit switch
					{
						TemperatureUnit.Celsius => $"{ConvertFahrenheitToCelsuis(valueToConvert):0.##}° C",
						TemperatureUnit.Fahrenheit => $"{inputToConvert}° F",
						TemperatureUnit.Kelvin => $"{ConvertFahrenheitToKelvin(valueToConvert):0.##} K",
						_ => _unableToConvertTemperature
					};
				default:
					return _unableToConvertTemperature;
			}

		}

		private static double ConvertKelvinToFahrenheit(double kelvin)
		{
			return kelvin * 1.8 - 459.67;
		}

		private static double ConvertKelvinToCelsius(double kelvin)
		{
			return kelvin - 273.15;
		}

		private static double ConvertFahrenheitToKelvin(double fahrenheit)
		{
			return (fahrenheit + 459.67) / 1.8;
		}

		private static double ConvertFahrenheitToCelsuis(double fahrenheit)
		{
			return (fahrenheit - 32) / 1.8;
		}

		private static double ConvertCelsiusToKelvin(double celsius)
		{
			return celsius + 273.15;
		}

		private static double ConvertCelsiusToFahrenheit(double celsius)
		{
			return celsius * 1.8 + 32;
		}

		private static void PrintErrorMessage(string errorMessage)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(errorMessage);
			Console.ForegroundColor = foregroundColor;
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

			try
			{
				return await _httpClient.GetFromJsonAsync<WeatherResults>(apiUrl);
			}
			catch (HttpRequestException ex)
			{

				Dictionary<string, string> inputParameters = new();
				inputParameters.Add("location", location);
				inputParameters.Add("temperatureUnit", temperatureUnit.ToString());
				LogError("GetAPIData", inputParameters, ex);

				return default;
			}


		}

		private static void LogError(string source, Dictionary<string, string> inputParameters, Exception exception)
		{
			StringBuilder logEntry = new();
			logEntry.AppendLine($"Source: {source}");
			logEntry.AppendLine($"Date/Time: {DateTime.UtcNow}");
			logEntry.AppendLine($"Exception: {exception.GetType()}");
			logEntry.AppendLine($"Exception Message: {exception.Message}");
			if (inputParameters.Any())
			{
				logEntry.AppendLine("Input Parameters:");
				foreach (KeyValuePair<string, string> inputParameter in inputParameters)
				{
					logEntry.AppendLine($"\t{inputParameter.Key} :: {inputParameter.Value}");
				}
			}
			logEntry.AppendLine();
			File.AppendAllText("Error.log", logEntry.ToString());
		}

	}

}