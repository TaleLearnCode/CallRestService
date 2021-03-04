using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CallRestService
{
	partial class Program
	{
		public class WeatherResults
		{

			[JsonPropertyName("coord")]
			public Coordinates Coorindates { get; set; }

			[JsonPropertyName("weather")]
			public List<Weather> Weather { get; set; }

			[JsonPropertyName("main")]
			public CurrentConditions CurrentConditions { get; set; }

			[JsonPropertyName("visibility")]
			public int Visibility { get; set; }

			[JsonPropertyName("wind")]
			public Wind Wind { get; set; }

			[JsonPropertyName("clouds")]
			public Clouds Clouds { get; set; }

			[JsonPropertyName("dt")]
			public int UnixTime { get; set; }

			[JsonPropertyName("sys")]
			public SunriseSunseet SunriseSunset { get; set; }

			[JsonPropertyName("timezone")]
			public int Timezone { get; set; }

			[JsonPropertyName("id")]
			public int Id { get; set; }

			[JsonPropertyName("name")]
			public string Name { get; set; }

		}

	}

}