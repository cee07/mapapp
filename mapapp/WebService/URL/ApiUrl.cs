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
		urls.Add(API.RATE, new ApiUrl("rate.php"));
		urls.Add(API.CHECKIN, new ApiUrl("checkin.php"));
		urls.Add(API.FEED, new ApiUrl("get_article2.php"));
		urls.Add(API.SUBMIT_PIN_LIST, new ApiUrl("submit_pin_list.php"));
		urls.Add(API.SUBSCRIPTION_LIST, new ApiUrl("get_coupon.php"));
		urls.Add(API.BADGE_LIST, new ApiUrl("badge.php"));
		urls.Add(API.SUBMIT_PIN, new ApiUrl("submit_pin.php"));
	}

	private ApiUrl(string url) {
		this.URL = HOST_URL + url;
	}

	public enum API {
		GET_ESTABLISHMENT,
		REGISTER,
		RATE,
		CHECKIN,
		FEED,
		SUBMIT_PIN_LIST,
		SUBSCRIPTION_LIST,
		BADGE_LIST,
		SUBMIT_PIN
	}
}
