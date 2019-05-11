using System;
using Newtonsoft.Json;

public class ErrorModel : BaseDataModel {

	[JsonProperty("error")]
	public string ErrorTitle { get; set; }

	[JsonProperty("error_description")]
	public string ErrorDescription { get; set; }

}