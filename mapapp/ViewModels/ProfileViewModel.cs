using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class ProfileViewModel : BaseDataViewModel {

		public EventHandler SettingsClicked;
		public EventHandler LogoutClicked;

		public ProfileViewModel () {
			SettingsClickedCommand = new Command(ExecuteSettingsClickedCommand);
			LogoutClickedCommand = new Command(ExecuteLogoutClickedCommand);
		}

		void ExecuteSettingsClickedCommand () {
			SettingsClicked?.Invoke(null, null);
		}

		void ExecuteLogoutClickedCommand() {
			LogoutClicked?.Invoke(null, null);
		}

		public ICommand SettingsClickedCommand { get; private set; }
		public ICommand LogoutClickedCommand { get; private set; }
	}
}
