using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mapapp.Views;
using Xamarin.Essentials;
using Plugin.FacebookClient;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace mapapp {
	public partial class App : Application {

		public App () {
			InitializeComponent();

			if (IsLoggedIn()) {
				MainPage = new MainPage();
			} else {
				MainPage = new LoginPage();
			}
		}

		protected override void OnStart () {
			// Handle when your app starts
		}

		protected override void OnSleep () {
			// Handle when your app sleeps
		}

		protected override void OnResume () {
			// Handle when your app resumes
		}

		public static void GoToFBLogin() {
			Current.MainPage = new FacebookLoginPage();
		}

		public static void GoToMainPage () {
			Current.MainPage = new MainPage();
		}

		private bool IsLoggedIn() {
			string email = Preferences.Get("email", null);
			return CrossFacebookClient.Current.IsLoggedIn || !string.IsNullOrEmpty(email);
		}
	}
}
