using System;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class SubmitPinPageViewModel : BaseDataViewModel {

		public EventHandler OnPinSubmitted;

		private SubmitPinHandler submitPinHandler;

		private bool showPrompt;
		public bool IsShowingPrompt {
			get { return showPrompt; }
			set { SetProperty(ref showPrompt, value); }
		}

		public SubmitPinPageViewModel () {
			submitPinHandler = new SubmitPinHandler();
			submitPinHandler.OnPinRequested += SubmitPinHandler_OnPinRequested;
			SubmitPinCommand = new Command(async () => await ExecuteSubmitPinCommand());
		}

		async void SubmitPinHandler_OnPinRequested (object sender, System.EventArgs e) {
			await SubmitPinPrompt();
			OnPinSubmitted?.Invoke(null, null);
		}

		private async Task SubmitPinPrompt () {
			IsShowingPrompt = true;
			await Task.Delay(2000);
			IsShowingPrompt = false;
		}

		private async Task ExecuteSubmitPinCommand() {
			try {
				IsBusy = true;
				if (CompletedFields()) {
					string email = Preferences.Get("email", null);
					if (!string.IsNullOrEmpty(email))
						await submitPinHandler.SubmitPin(email, Name, Address, null, Category, Description);
				} else {
					await Application.Current.MainPage.DisplayAlert("Validation Error", 
					                                                "Please complete the required field/s.",
					                                                "OK");
				}
			} finally {
				IsBusy = false;
			}
		}

		public ICommand SubmitPinCommand { get; private set; }

		private bool CompletedFields() {
			return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Category) &&
						  !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Description);
		}

		private string name;
		public string Name {
			get { return name; }
			set { SetProperty(ref name, value); }
		}

		private string category;
		public string Category {
			get { return category; }
			set { SetProperty(ref category, value); }
		}

		private string address;
		public string Address {
			get { return address; }
			set { SetProperty(ref address, value); }
		}

		private string description;
		public string Description {
			get { return description; }
			set { SetProperty(ref description, value); }
		}
	}
}
