﻿using System;
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
		public string Thumbnail { get; set; }

		[JsonProperty("author")]
		public string Author { get; set; }

	}
}
