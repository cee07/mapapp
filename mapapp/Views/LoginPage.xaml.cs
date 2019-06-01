using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class LoginPage : ContentPage {

		private FacebookViewModel facebookViewModel;

		public LoginPage () {
			InitializeComponent();
			facebookViewModel = new FacebookViewModel();
		}

		async void OnFacebookClicked (object sender, System.EventArgs e) {
			var permission = await CrossFacebookClient.Current.LoginAsync(new string[] { "email" }, FacebookPermissionType.Read);
			if (permission.Status == FacebookActionStatus.Completed) {
				var response = await CrossFacebookClient.Current.RequestUserDataAsync(new string[] { "email" }, new string[] { "email" });
				var facebookResponseModel = JsonConvert.DeserializeObject<FacebookProfileModel>(response.Data);
				await facebookViewModel.Register(facebookResponseModel.Email);
			} else {
				await Application.Current.MainPage.DisplayAlert("Login", "You have cancelled facebook login.", "OK");
			}
		}

		void OnClickSkip (object sender, System.EventArgs e) {
			Xamarin.Essentials.Preferences.Set("IsLoggedIn", false);
			App.GoToMainPage();
		}
	}
}
