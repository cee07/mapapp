using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class SubscriptionModel {

		[JsonProperty("id")]
		public string ID { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("url_link")]
		public string URL { get; set; }

	}
}
