using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
using Plugin.GoogleClient;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class LoginPage : ContentPage {

		private FacebookViewModel facebookViewModel;
		private GoogleViewModel googleViewModel;

		public LoginPage () {
			InitializeComponent();
			facebookViewModel = new FacebookViewModel();
			googleViewModel = new GoogleViewModel();
		}

		async void OnFacebookClicked (object sender, System.EventArgs e) {
			var permission = await CrossFacebookClient.Current.LoginAsync(new string[] { "email" }, FacebookPermissionType.Read);
			if (permission.Status == FacebookActionStatus.Completed) {
				var response = await CrossFacebookClient.Current.RequestUserDataAsync(new string[] { "email", "name", "picture" },
				                                                                      new string[] { "email" });
				if (response.Status == FacebookActionStatus.Completed) {
					var facebookResponseModel = JsonConvert.DeserializeObject<FacebookProfileModel>(response.Data);
					await facebookViewModel.Register(facebookResponseModel.Email);
					Preferences.Set("email", facebookResponseModel.Email);
					Preferences.Set("name", facebookResponseModel.Name);
					Preferences.Set("picture", facebookResponseModel.Picture.Data.URL);
					App.GoToMainPage();
				}
			} 
		}

		async void OnGoogleLoginClicked (object sender, System.EventArgs e) {
			var login = await CrossGoogleClient.Current.LoginAsync();
			if (login.Status == GoogleActionStatus.Completed) {
				googleViewModel.SignInCommand.Execute(login.Data.Email);
				Preferences.Set("email", login.Data.Email);
				Preferences.Set("name", login.Data.Name);
				Preferences.Set("picture", login.Data.Picture.OriginalString);
				App.GoToMainPage();
			}
		}

		void OnClickSkip (object sender, System.EventArgs e) {
			skipPopup.IsVisible = true;
		}

		void OnClickPopupOption (object sender, System.EventArgs e) {
			var button = (Button) sender;
			if (button.Text.Equals("NO"))
				skipPopup.IsVisible = false;
			else {
				Preferences.Set("email", null);
				Preferences.Set("name", null);
				Preferences.Set("picture", null);
				App.GoToMainPage();
			}
		}
	}
}