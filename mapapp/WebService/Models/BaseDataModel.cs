using System;
using Newtonsoft.Json;

public class BaseDataModel {

	[JsonProperty("success")]
	public string Success { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }
}

