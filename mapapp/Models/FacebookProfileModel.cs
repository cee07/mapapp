using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class FacebookProfileModel {

		[JsonProperty("email")]
		public string Email { get; set; }

	}
}
