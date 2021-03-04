using System.Text.Json.Serialization;

namespace CallRestService.APIModel
{
		public class Coordinates
		{
			[JsonPropertyName("lon")]
			public double Longitude { get; set; }

			[JsonPropertyName("lat")]
			public double Latitude { get; set; }
		}

}
