using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class SubscriptionViewModel : BaseDataViewModel {

		public System.Action<List<SubscriptionModel>> OnSubscriptionsRequested;

		private SubscriptionHandler subscriptionHandler;

		public SubscriptionViewModel () {
			subscriptionHandler = new SubscriptionHandler();
			subscriptionHandler.OnSubsRequested += OnSubsRequested;
			RequestSubscriptionsCommand = new Command(async () => await ExecuteRequestSubscriptionsCommand());
		}

		void OnSubsRequested (List<SubscriptionModel> obj) {
			OnSubscriptionsRequested?.Invoke(obj);
			IsBusy = false;
		}

		private async Task ExecuteRequestSubscriptionsCommand() {
			try {
				if (IsBusy)
					return;
				IsBusy = true;
				var email = Preferences.Get("email", null);
				if (!string.IsNullOrEmpty(email))
					await subscriptionHandler.RequestSubscriptions(email);
				else
					await Application.Current.MainPage.DisplayAlert("Error",
																 "You are not logged in to use this feature.",
																 "OK");
			} finally {

			}
		}

		public ICommand RequestSubscriptionsCommand { get; private set; }
	}
}
