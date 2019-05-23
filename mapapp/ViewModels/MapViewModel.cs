using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Helpers;
using mapapp.Models;
using MvvmHelpers;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.ViewModels {
	public class MapViewModel : BaseDataViewModel {

		public System.Action<List<CustomPin>> OnPinsRefreshed;

		private PinRequestHandler pinRequestHandler;

		public ICommand RequestMapDataCommand { get; private set; }
		public ICommand RequestPinsCommand { get; private set; }

		public MapViewModel () {
			pinRequestHandler = new PinRequestHandler();
			pinRequestHandler.OnPinsRequested += PinRequestHandler_OnPinsRequested;
			RequestMapDataCommand = new Command<string>(async (x) => await ExecuteRequestMapDataCommand(x));
			RequestPinsCommand = new Command<PinRequestModel>(async (x) => await ExecuteRequestPins(x));
		}

		private async Task ExecuteRequestMapDataCommand(string category) {
			try {

				IsBusy = true;
				await ExecuteGetCurrentPositionCommand();

				PinRequestModel pinRequestModel = CreatePinRequestModel(category);

				await pinRequestHandler.RequestPins(pinRequestModel);
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
			} finally {
				IsBusy = false;
			}
		}

		private PinRequestModel CreatePinRequestModel(string category) {
			PinRequestModel pinRequestModel = new PinRequestModel();
			pinRequestModel.Category = category;
			//pinRequestModel.Latitude = CurrentPosition.Latitude.ToString();
			//pinRequestModel.Longitude = CurrentPosition.Longitude.ToString();
			pinRequestModel.Latitude = "14.633202";
			pinRequestModel.Longitude = "121.043982";
			pinRequestModel.Distance = "5";
			pinRequestModel.Limit = "10";
			return pinRequestModel;
		}

		void PinRequestHandler_OnPinsRequested (List<CustomPin> pins) {
			OnPinsRefreshed?.Invoke(pins);
		}

		private async Task ExecuteGetCurrentPositionCommand() {
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
			CurrentPosition = new Position(position.Latitude, position.Longitude);
		}

		private async Task ExecuteRequestPins(PinRequestModel pinRequestModel) {
			try {
				IsBusy = true;
				await pinRequestHandler.RequestPins(pinRequestModel);
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
