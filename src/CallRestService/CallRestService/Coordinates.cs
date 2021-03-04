using System.Text.Json.Serialization;

namespace CallRestService
{
	partial class Program
	{
		public class Coordinates
		{
			[JsonPropertyName("lon")]
			public double Longitude { get; set; }

			[JsonPropertyName("lat")]
			public double Latitude { get; set; }
		}

	}


}
