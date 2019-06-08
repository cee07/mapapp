using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class FacebookPictureURIModel {

		[JsonProperty("url")]
		public string URL { get; set; }

	}
}
