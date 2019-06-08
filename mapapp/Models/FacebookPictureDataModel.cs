using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class FacebookPictureDataModel {

		[JsonProperty("data")]
		public FacebookPictureURIModel Data { get; set; }

	}
}
