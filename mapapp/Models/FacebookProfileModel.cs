using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class FacebookProfileModel {

		[JsonProperty("id")]
		public string ID { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("picture")]
		public FacebookPictureDataModel Picture { get; set; }
	}
}
