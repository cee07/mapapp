using System;
using System.Collections.Generic;

public class ApiUrl {
	private const string HOST_URL = "https://eplifeapistage.eperformax.com/merchants/v1/";
	private const string LOGIN_ENDPOINT = ""; 

	public string URL { get; private set; } = "UnknownUrl";

	public static Dictionary<API, ApiUrl> urls;

	static ApiUrl() {
		urls = new Dictionary<API, ApiUrl> ();
		urls.Add(API.LOGIN, new ApiUrl("merchants/login"));
		urls.Add(API.PROFILE, new ApiUrl("merchants/profile"));
		urls.Add(API.CHANGE_PASSWORD, new ApiUrl("merchants/change_password"));
		urls.Add(API.PURCHASE, new ApiUrl("sales/purchase"));
		urls.Add(API.SUMMARY, new ApiUrl("sales/transactions"));
		urls.Add(API.HISTORY, new ApiUrl("sales/history"));
		urls.Add(API.TRANSACTION_NUMBER, new ApiUrl("sales/qr"));
		urls.Add(API.YEARS, new ApiUrl("sales/year"));
		urls.Add(API.FORCE_UPDATE, new ApiUrl("api/force_update_merchant_app_android"));
	}

	private ApiUrl(string url) {
		this.URL = HOST_URL + url;
	}

	public enum API {
		LOGIN,
		PROFILE,
		CHANGE_PASSWORD,
		PURCHASE,
		SUMMARY,
		HISTORY,
		TRANSACTION_NUMBER,
		YEARS,
		FORCE_UPDATE
	}
}
