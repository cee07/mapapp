using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class PictureURIModel {

		[JsonProperty("url")]
		public string URL { get; set; }

	}
}
