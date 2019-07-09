using System;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class FeedModel {

		[JsonProperty("article_title")]
		public string Title { get; set; }

		[JsonProperty("article_teaser")]
		public string Excerpt { get; set; }

		[JsonProperty("article_main")]
		public string Content { get; set; }

		[JsonProperty("article_thumbnail")]
		public string ArticleThumbnail { get; set; }

		[JsonProperty("author")]
		public string Author { get; set; }

		public string Thumbnail {
			get {
				if (string.IsNullOrEmpty(ArticleThumbnail))
					return "xamarin_logo.png";
				return string.Format("https://mapp.com.ph/mcms/public/images/original/{0}", ArticleThumbnail);
			}
		}
	}
}
