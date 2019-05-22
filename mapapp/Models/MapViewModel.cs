using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Helpers;
using MvvmHelpers;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.Models {
	public class MapViewModel : BaseDataViewModel {

		public System.Action<List<CustomPin>> OnPinsRefreshed;

		private PinRequestHandler pinRequestHandler;

		public ICommand RequestMapDataCommand { get; private set; }
		public ICommand RequestPinsCommand { get; private set; }

		public MapViewModel () {
			pinRequestHandler = new PinRequestHandler();
			pinRequestHandler.OnPinsRequested += PinRequestHandler_OnPinsRequested;
			RequestMapDataCommand = new Command(async () => await ExecuteRequestMapDataCommand());
			RequestPinsCommand = new Command(async () => await ExecuteRequestPins());
		}

		private async Task ExecuteRequestMapDataCommand() {
			try {
				IsBusy = true;
				await ExecuteGetCurrentPositionCommand();
				await pinRequestHandler.RequestPins();
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
			} finally {
				IsBusy = false;
			}
		}

		void PinRequestHandler_OnPinsRequested (List<CustomPin> pins) {
			OnPinsRefreshed?.Invoke(pins);
		}

		private async Task ExecuteGetCurrentPositionCommand() {
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
			CurrentPosition = new Position(position.Latitude, position.Longitude);
		}

		private async Task ExecuteRequestPins() {
			try {
				IsBusy = true;
				await pinRequestHandler.RequestPins();
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
			} finally {
				IsBusy = false;
			}
		}

		private Position currentPos;
		public Position CurrentPosition {
			get {
				return currentPos;
			}
			set {
				SetProperty(ref currentPos, value);
			}
		}
	}
}
