using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class LoginPage : ContentPage {

		public LoginPage () {
			InitializeComponent();
		}

		void OnFacebookClicked (object sender, System.EventArgs e) {
			App.GoToFBLogin();
		}

		void OnClickSkip (object sender, System.EventArgs e) {
			Xamarin.Essentials.Preferences.Set("IsLoggedIn", false);
			App.GoToMainPage();
		}
	}
}
