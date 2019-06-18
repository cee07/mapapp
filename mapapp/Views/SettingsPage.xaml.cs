using System;
using System.Collections.Generic;
using mapapp.Helpers;
using Plugin.FacebookClient;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SettingsPage : ContentPage {
		public SettingsPage () {
			InitializeComponent();
		}

		void OnLocationTapped (object sender, System.EventArgs e) {
			DependencyService.Register<ISettingsService>();
			DependencyService.Get<ISettingsService>().OpenSettings();
		}

		void OnLogoutTapped (object sender, System.EventArgs e) {
			CrossFacebookClient.Current.Logout();
			Preferences.Set("email", null);
			App.GoToLogin();
		}

		void OnFacebookTapped (object sender, System.EventArgs e) {
			Device.OpenUri(new Uri("https://www.facebook.com/MAppPH/"));
		}

		void OnInstagramTapped (object sender, System.EventArgs e) {
			Device.OpenUri(new Uri("https://www.instagram.com/mapp.ph/"));
		}
	}
}