using System.Text.Json.Serialization;

namespace CallRestService
{
	partial class Program
	{
		public class CurrentConditions
		{

			[JsonPropertyName("temp")]
			public double Temperature { get; set; }

			[JsonPropertyName("feels_like")]
			public double FeelsLike { get; set; }

			[JsonPropertyName("temp_min")]
			public double MinimumTemperature { get; set; }

			[JsonPropertyName("temp_max")]
			public double MaximumTemperature { get; set; }

			[JsonPropertyName("pressure")]
			public int Pressure { get; set; }

			[JsonPropertyName("humidity")]
			public int Humidity { get; set; }
		}



	}


}
