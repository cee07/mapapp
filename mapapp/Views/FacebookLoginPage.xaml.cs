using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class FacebookLoginPage : ContentPage {

		private const string ClientId = "632674237193926";

		private FacebookViewModel facebookViewModel;

		public FacebookLoginPage () {
			InitializeComponent();
			BindingContext = facebookViewModel = new FacebookViewModel();
			var apiRequest =
				"https://www.facebook.com/v3.3/dialog/oauth?client_id="
				+ ClientId
				+ "&display=popup&response_type=token&scope=email&redirect_uri=https://www.facebook.com/connect/login_success.html";

			var webView = new WebView {
				Source = apiRequest,
				HeightRequest = 1
			};

			webView.Navigated += WebViewOnNavigated;
			Content = webView;
		}

		private async void WebViewOnNavigated (object sender, WebNavigatedEventArgs e) {
			var accessToken = ExtractAccessTokenFromUrl(e.Url);
			if (!string.IsNullOrEmpty(accessToken)) {
				System.Diagnostics.Debug.WriteLine("Access token: " + accessToken);
				var facebookProfile = await facebookViewModel.RequestFacebookProfileAsync(accessToken);

			}
		}

		private string ExtractAccessTokenFromUrl (string url) {
			if (url.Contains("access_token") && url.Contains("&expires_in=")) {
				var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
				var accessToken = at.Remove(at.IndexOf("&expires_in="));
				accessToken = accessToken.Remove(accessToken.IndexOf("&data_access_expiration_time"));
				return accessToken;
			}
			return string.Empty;
		}
	}
}
