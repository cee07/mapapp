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

		public ObservableRangeCollection<PinModel> PinModels { get; private set; }

		public MapViewModel () {
			Limit = "10";
			pinRequestHandler = new PinRequestHandler();
			PinModels = new ObservableRangeCollection<PinModel>();
			pinRequestHandler.OnPinsRequested += PinRequestHandler_OnPinsRequested;
			RequestMapDataCommand = new Command<string>(async (x) => await ExecuteRequestMapDataCommand(x));
		}

		private async Task ExecuteRequestMapDataCommand(string category) {
			try {
				IsBusy = true;
				PinModels.Clear();
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
			pinRequestModel.Limit = Limit;
			return pinRequestModel;
		}

		void PinRequestHandler_OnPinsRequested (List<CustomPin> pins) {
			PinModels.Clear();
			PinModels.AddRange(pinRequestHandler.PinModels);
			OnPinsRefreshed?.Invoke(pins);
		}

		private async Task ExecuteGetCurrentPositionCommand() {
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
			CurrentPosition = new Position(position.Latitude, position.Longitude);
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

		public string CategoryHolder { get; set; }
		public string Limit { get; set; }
	}
}
