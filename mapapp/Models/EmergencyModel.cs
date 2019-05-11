using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class EmergencyModel {

		[JsonProperty("contact_name")]
		public string ContactName { get; set; }

		[JsonProperty("contact_description")]
		public string ContactDescription { get; set; }

		[JsonProperty("contact_numbers")]
		public List<string> ContactNumbers { get; set; }

	}
}
