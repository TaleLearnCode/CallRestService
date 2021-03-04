using System.Text.Json.Serialization;

namespace CallRestService
{
	partial class Program
	{

		public class Clouds
		{
			[JsonPropertyName("all")]
			public int Cloudiness { get; set; }
		}


	}


}
