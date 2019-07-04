#define TEST

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Helpers;
using mapapp.Models;
using MvvmHelpers;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.ViewModels {
	public class MapViewModel : BaseDataViewModel {

		public System.Action OnCurrentLocationRequested;
		public System.Action<List<CustomPin>> OnPinsRefreshed;

		private PinRequestHandler pinRequestHandler;

		public ICommand RequestCurrentLocationCommand { get; private set; }
		public ICommand RequestMapDataCommand { get; private set; }
		public ICommand RequestPinsCommand { get; private set; }

		public ObservableRangeCollection<PinModel> PinModels { get; private set; }

		public MapViewModel () {
			Limit = "10";
			Distance = "7";
			pinRequestHandler = new PinRequestHandler();
			PinModels = new ObservableRangeCollection<PinModel>();
			pinRequestHandler.OnPinsRequested += PinRequestHandler_OnPinsRequested;
			RequestMapDataCommand = new Command<string>(async (x) => await ExecuteRequestMapDataCommand(x));
			RequestCurrentLocationCommand = new Command(async () => await RequestCurrentLocation());
		}

		private async Task RequestCurrentLocation () {
			IsBusy = true;
			var stat = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
			if (stat != PermissionStatus.Granted) {
				var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
				if (results.ContainsKey(Permission.Location)) {
					var locator = CrossGeolocator.Current;
					var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
					CurrentPosition = new Position(position.Latitude, position.Longitude);
#if TEST
					CurrentPosition = new Position(14.6333, 121.0439);
#endif
					OnCurrentLocationRequested?.Invoke();
				}
			} else if (stat == PermissionStatus.Granted) {
				var locator = CrossGeolocator.Current;
				var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
				CurrentPosition = new Position(position.Latitude, position.Longitude);
#if TEST
					CurrentPosition = new Position(14.6333, 121.0439);
#endif
				OnCurrentLocationRequested?.Invoke();
			} else {
				await Application.Current.MainPage.DisplayAlert("Error", "Could not get your location. Please enable location service in your settings", "OK");
			}
			IsBusy = false;
		}

		private async Task ExecuteRequestMapDataCommand (string category) {
			try {
				IsBusy = true;
				var stat = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationAlways);
				if (stat.ContainsKey(Permission.LocationAlways)) {
					await ExecuteGetCurrentPositionCommand(category);
				} else {
					await Application.Current.MainPage.DisplayAlert("Error", "Please enable the location permission it in your settings", "OK");
				}
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
			} finally {
				IsBusy = false;
			}
		}

		private PinRequestModel CreatePinRequestModel(string category) {
			PinRequestModel pinRequestModel = new PinRequestModel();
			pinRequestModel.Category = category;
			pinRequestModel.Latitude = CurrentPosition.Latitude.ToString();
			pinRequestModel.Longitude = CurrentPosition.Longitude.ToString();
#if TEST
			pinRequestModel.Latitude = "14.633202";
			pinRequestModel.Longitude = "121.043982";
#endif
			pinRequestModel.Distance = Distance;
			pinRequestModel.Limit = Limit;
			pinRequestModel.CatKey = CatKey;
			return pinRequestModel;
		}

		void PinRequestHandler_OnPinsRequested (List<CustomPin> pins) {
			PinModels.Clear();
			PinModels.AddRange(pinRequestHandler.PinModels);
			OnPinsRefreshed?.Invoke(pins);
		}

		private async Task ExecuteGetCurrentPositionCommand(string category) {
			try {
				var locator = CrossGeolocator.Current;
				var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
				CurrentPosition = new Position(position.Latitude, position.Longitude);
#if TEST
				CurrentPosition = new Position(14.6333, 121.0439);
#endif
			} catch(Exception e) {
				await Application.Current.MainPage.DisplayAlert("Error", "Could not get your location.", "OK");
			}
			await RequestMapPinsCommand(category);
		}

		private async Task RequestMapPinsCommand(string category) {
			PinModels.Clear();
			PinRequestModel pinRequestModel = CreatePinRequestModel(category);
			await pinRequestHandler.RequestPins(pinRequestModel);
			System.Diagnostics.Debug.WriteLine("REQUESTING: " + pinRequestModel.Category);
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
		public string Distance { get; set; }
		public string CatKey { get; set; }

		private bool hasPins;
		public bool HasPins {
			get { return HasPins; }
			set { SetProperty(ref hasPins, value); }
		}
	}
}
