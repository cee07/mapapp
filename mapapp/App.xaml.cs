using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mapapp.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace mapapp {
	public partial class App : Application {

		public App () {
			InitializeComponent();

			MainPage = new LoginPage();
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
	}
}
