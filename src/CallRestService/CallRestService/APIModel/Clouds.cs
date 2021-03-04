using System.Text.Json.Serialization;

namespace CallRestService.APIModel
{

		public class Clouds
		{
			[JsonPropertyName("all")]
			public int Cloudiness { get; set; }
		}



}
