using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

public class JsonWebRequest<T> {

	private const string AUTHORIZATION = "Authorization";
	private const string AUTHORIZATION_VALUE = "Basic YXBwOjQ5ZDM1MDM5OTRkYTY5MzFmOWYzNzhmMzJlNzYzOWU2YjE3NGRkZGU=";
	private const string X_API_KEY = "x-api-key";
	private const string X_API_KEY_VALUE = "8587c927d30c40ea09d36a76c08d2520710a9089";

	private const double REQUEST_TIMEOUT = 20;

	public Action HasTimedOut;
	public Func<Task> HasError;
	public Func<Task> OnAPICallSuccessful;

	private string url;

	private HttpMethod httpMethod;
	private ApiUrl.API apiCall;
	private APIForm form;

	public T Data { get; private set; }
	public bool IsExecuting { get; private set; } = false;

	///<summary>
	///Creates a JSON web request. Ignore the "form" parameter if you're using GET.
	///</summary>
	public static JsonWebRequest<T> CreateRequest(HttpMethod method, ApiUrl.API apiCall, APIForm form = null) {
		JsonWebRequest<T> request = new JsonWebRequest<T> (method, apiCall, form);
		return request;
	}

	private JsonWebRequest(HttpMethod method, ApiUrl.API apiCall, APIForm form = null) {
		this.httpMethod = method;
		this.apiCall = apiCall;
		this.form = form;
		url = ApiUrl.urls[apiCall].URL;
		this.Data = default (T);
	}

	public async Task GetData() {
		if (CrossConnectivity.Current.IsConnected) {
			try {
				HttpClient httpClient = CreateHttpClient ();
				var request = await CreateHttpRequest (httpClient);

				await ProcessHttpRequest (request);
			} catch (HttpRequestException ex) {
				Debug.WriteLine ("Exception Handled: " + ex.InnerException.Message);
			} finally {

			}
		} else {
			await Application.Current.MainPage.DisplayAlert ("No Internet Access","Please check your internet connection.","OK");
		}
	}

	private HttpClient CreateHttpClient() {
		HttpClient httpClient = new HttpClient ();
		httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
		httpClient.Timeout = TimeSpan.FromSeconds (REQUEST_TIMEOUT);
		return httpClient;
	}

	private async Task<HttpResponseMessage> CreateHttpRequest(HttpClient httpClient) {
		HttpResponseMessage request = null;
		switch(httpMethod) {
		case HttpMethod.POST:
				FormUrlEncodedContent formContent = new FormUrlEncodedContent (form.FormContent);
				request = await httpClient.PostAsync (url, formContent);
				break;
		case HttpMethod.GET:
				request = await httpClient.GetAsync (url);
				break;
		}
		return request;
	}

	private async Task ProcessHttpRequest(HttpResponseMessage request) {
		switch (request.StatusCode) {
		case HttpStatusCode.OK:
		case HttpStatusCode.BadRequest:
		case HttpStatusCode.NotFound:
				var response = await request.Content.ReadAsByteArrayAsync ();
				await RequestCompleted (request, System.Text.Encoding.UTF8.GetString(response));
			break;
		case HttpStatusCode.RequestTimeout:
				await Application.Current.MainPage.DisplayAlert ("Request timed out",
																 "Your request has been timed out. Please try again later.",
																 "OK");
				await Task.Run (() => HasTimedOut?.Invoke ());
				break;
		}
	}

	private async Task RequestCompleted(HttpResponseMessage request, string response) {
		JsonSerializerSettings settings = new JsonSerializerSettings {
			MissingMemberHandling = MissingMemberHandling.Error
		};
		try {
			switch (request.StatusCode) {
				case HttpStatusCode.OK:
					await HandleSuccess (request,settings,response);
					break;
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.NotFound:
					await HandleError (request,settings,response);
					break;
			}
		} finally {

		}
	}

	private async Task HandleSuccess (HttpResponseMessage request, JsonSerializerSettings settings, string response) {
		try {
			T dataModel = JsonConvert.DeserializeObject<T>(response);
			if (dataModel != null) {
				Data = dataModel;
				await OnAPICallSuccessful?.Invoke();
			}
		} catch (Exception e) {
			Debug.WriteLine("Parsing Error: " + e.Message);
		}
	}

	private async Task HandleError (HttpResponseMessage request,JsonSerializerSettings settings,string response) {
		try {
			ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel> (response);
			if (errorModel != null) {
				Device.BeginInvokeOnMainThread(() => {
					Application.Current.MainPage.DisplayAlert(errorModel.ErrorTitle,
																 errorModel.ErrorDescription,
																 "OK");
					HasError?.Invoke();
				});
			}
		} finally {

		}
	}
}

public enum HttpMethod {
	GET,
	POST
}