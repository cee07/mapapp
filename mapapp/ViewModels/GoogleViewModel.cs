using System;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class GoogleViewModel : BaseViewModel {

		private RegistrationHandler registrationHandler;

		public GoogleViewModel () {
			registrationHandler = new RegistrationHandler();
			SignInCommand = new Command<string>(async (x) => await ExecuteSignInCommand(x));
		}

		public ICommand SignInCommand { get; set; }

		private async Task ExecuteSignInCommand(string email) {
			await registrationHandler.Register(email);
			Preferences.Set("email", email);
			App.GoToMainPage();
		}
	}
}
