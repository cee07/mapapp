using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class BadgeModel {

		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("count")]
		public string Count { get; set; }

		[JsonProperty("image")]
		public string Badge { get; set; }

		public float Progress { 
			get {
				float progress = Convert.ToInt32(Count) / 300f;
				return progress;
			}
		}

	}
}
