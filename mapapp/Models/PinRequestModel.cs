using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class PinRequestModel {

		[JsonProperty("cat")]
		public string Category { get; set; }

		[JsonProperty("lat")]
		public string Latitude { get; set; }

		[JsonProperty("long")]
		public string Longitude { get; set; }

		[JsonProperty("dist")]
		public string Distance { get; set; }

		[JsonProperty("limit")]
		public string Limit { get; set; }

	}
}
