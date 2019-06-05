using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Models;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class SubmitPinsListViewModel : BaseDataViewModel {

		public System.Action OnPinsRequested;

		private SubmittedPinsListHandler submittedPinsListHandler;

		public ObservableRangeCollection<PinModel> PinList { get; private set; }

		public SubmitPinsListViewModel () {
			PinList = new ObservableRangeCollection<PinModel>();
			submittedPinsListHandler = new SubmittedPinsListHandler();
			submittedPinsListHandler.OnPinsRequested += OnPinListRequested;
			RequestSubmittedPins = new Command(async () => await ExecuteRequestSubmittedPins());
		}

		void OnPinListRequested (List<PinModel> obj) {
			PinList.Clear();
			PinList.AddRange(obj);
			OnPinsRequested?.Invoke();
		}

		private async Task ExecuteRequestSubmittedPins() {
			try {
				IsBusy = true;
				var email = Preferences.Get("email", null);
				if (!string.IsNullOrEmpty(email))
					await submittedPinsListHandler.RequestPinList(email);
			} finally {
				IsBusy = false;
			}
		}

		public ICommand RequestSubmittedPins { get; private set; }

	}
}
