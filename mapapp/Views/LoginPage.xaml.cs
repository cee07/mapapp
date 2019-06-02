using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
//using Plugin.GoogleClient;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class LoginPage : ContentPage {

		private FacebookViewModel facebookViewModel;
		private GoogleViewModel googleViewModel;

		public LoginPage () {
			InitializeComponent();
			facebookViewModel = new FacebookViewModel();
		}

		async void OnFacebookClicked (object sender, System.EventArgs e) {
			var permission = await CrossFacebookClient.Current.LoginAsync(new string[] { "email" }, FacebookPermissionType.Read);
			if (permission.Status == FacebookActionStatus.Completed) {
				var response = await CrossFacebookClient.Current.RequestUserDataAsync(new string[] { "email" }, new string[] { "email" });
				if (response.Status == FacebookActionStatus.Completed) {
					var facebookResponseModel = JsonConvert.DeserializeObject<FacebookProfileModel>(response.Data);
					await facebookViewModel.Register(facebookResponseModel.Email);
					Preferences.Set("email", facebookResponseModel.Email);
					App.GoToMainPage();
				}
			} 
		}

		async void OnGoogleLoginClicked (object sender, System.EventArgs e) {
			//await CrossGoogleClient.Current.LoginAsync();
			//CrossGoogleClient.Current.OnLogin += (s, a) => {
			//	switch (a.Status) {
			//		case GoogleActionStatus.Completed:
			//			googleViewModel.SignInCommand.Execute(a.Data.Email);
			//			break;
			//	}
			//};
		}

		void OnClickSkip (object sender, System.EventArgs e) {
			Xamarin.Essentials.Preferences.Set("IsLoggedIn", false);
			App.GoToMainPage();
		}
	}
}