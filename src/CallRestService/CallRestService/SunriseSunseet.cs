using System.Text.Json.Serialization;

namespace CallRestService
{
	partial class Program
	{
		public class SunriseSunseet
		{

			[JsonPropertyName("sunrise")]
			public int Sunrise { get; set; }

			[JsonPropertyName("sunset")]
			public int Sunset { get; set; }

		}



	}


}
