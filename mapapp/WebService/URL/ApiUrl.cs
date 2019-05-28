using System;
using System.Collections.Generic;

public class ApiUrl {
	private const string HOST_URL = "https://mapp.com.ph/php/";
	private const string LOGIN_ENDPOINT = ""; 

	public string URL { get; private set; } = "UnknownUrl";

	public static Dictionary<API, ApiUrl> urls;

	static ApiUrl() {
		urls = new Dictionary<API, ApiUrl> ();
		urls.Add(API.GET_ESTABLISHMENT, new ApiUrl("get_establishmentV3.php"));
		urls.Add(API.REGISTER, new ApiUrl("registrationV2.php"));
	}

	private ApiUrl(string url) {
		this.URL = HOST_URL + url;
	}

	public enum API {
		GET_ESTABLISHMENT,
		REGISTER,
		RATE
	}
}
