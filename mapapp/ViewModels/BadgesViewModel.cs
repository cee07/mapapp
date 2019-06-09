using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class BadgesViewModel : BaseDataViewModel {

		public System.Action<List<BadgeModel>> OnBadgesRequested;

		private BadgesHandler badgesHandler;

		public BadgesViewModel () {
			badgesHandler = new BadgesHandler();
			badgesHandler.OnBadgesReceived += OnBadgesReceived;
			RequestBadgesCommand = new Command(async () => await ExecuteRequestBadgesCommand());
		}

		void OnBadgesReceived (List<Models.BadgeModel> obj) {
			IsBusy = false;
			OnBadgesRequested?.Invoke(obj);
		}

		private async Task ExecuteRequestBadgesCommand() {
			try {
				if (IsBusy)
					return;
				IsBusy = true;
				var email = Preferences.Get("email", null);
				if (!string.IsNullOrEmpty(email))
					await badgesHandler.RequestBadges(email);
			} finally {

			}
		}

		public ICommand RequestBadgesCommand { get; private set; }
	}
}
