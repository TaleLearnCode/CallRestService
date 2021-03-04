using System.Text.Json.Serialization;

namespace CallRestService.APIModel
{
	public class SunriseSunseet
	{

		[JsonPropertyName("sunrise")]
		public int Sunrise { get; set; }

		[JsonPropertyName("sunset")]
		public int Sunset { get; set; }

	}

}
