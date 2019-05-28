using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Helpers;
using mapapp.Models;
using MvvmHelpers;
using Plugin.Messaging;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.ViewModels {
	public class PinDetailPageViewModel : BaseDataViewModel {

		private RateHandler rateHandler;

		public PinDetailPageViewModel(PinModel pin) {
			Pins = new ObservableRangeCollection<CustomPin>();
			PinModelData = pin;
			CustomPin customPin = new CustomPin() {
				Label = pin.EstablishmentName,
				Address = pin.Address,
				Position = new Position(Convert.ToDouble(pin.Latitude), Convert.ToDouble(pin.Longitude)),
				Type = Xamarin.Forms.Maps.PinType.Place,
				PinType = pin.PinModelType
			};
			Pins.AddRange(new List<CustomPin>() { customPin });
			CallCommand = new Command(ExecuteCallCommand);
			CheckDataCommand = new Command(async () => await ExecuteCheckDataCommand());
			RateCommand = new Command(async () => await ExecuteRateCommand());
			rateHandler = new RateHandler();
			rateHandler.OnRateRequested += OnRateChecked;
		}

		void ExecuteCallCommand() {
			var phoneDialer = CrossMessaging.Current.PhoneDialer;
			if (phoneDialer.CanMakePhoneCall)
				phoneDialer.MakePhoneCall(PinModelData.Contact);
		}

		void OnRateChecked(string response) {
			HasRated = response.Equals("400");
		}

		private async Task ExecuteRateCommand() {
			try {
				IsBusy = true;
				string email = Preferences.Get("email", null);
				if (!string.IsNullOrEmpty(email))
					await rateHandler.Rate(email, "1", PinModelData.EstablishmentID, RatingValue.ToString());
			} catch (Exception e) {
				await Application.Current.MainPage.DisplayAlert("Error",
					                                            e.Message,
					                                            "OK");
			} finally {
				IsBusy = false;
			}
		}

		private async Task ExecuteCheckDataCommand () {
			try {
				IsBusy = true;
				Preferences.Set("email", "ceenuevas@gmail.com");
				string email = Preferences.Get("email", null);
				if (!string.IsNullOrEmpty(email))
					await rateHandler.Rate(email, "0", PinModelData.EstablishmentID, "0");
			} catch (Exception e) {
				await Application.Current.MainPage.DisplayAlert("Error",
																e.Message,
																"OK");
			} finally {
				IsBusy = false;
			}
		}

		private PinModel pinModel;
		public PinModel PinModelData {
			get { return pinModel; }
			set { SetProperty(ref pinModel, value); }
		}

		public Position CurrentPosition {
			get {
				return new Position(Convert.ToDouble(PinModelData.Latitude),
									Convert.ToDouble(PinModelData.Longitude));
			}
		}

		private bool hasRated;
		public bool HasRated {
			get { return hasRated; }
			set { SetProperty(ref hasRated, value); }
		}

		public int RatingValue { get; set; }

		public ObservableRangeCollection<CustomPin> Pins { get; private set; }

		public ICommand CallCommand { get; private set; }
		public ICommand RateCommand { get; private set; }
		public ICommand CheckDataCommand { get; private set; }
	}
}