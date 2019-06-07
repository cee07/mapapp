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

		private async Task ExecuteRequestMapDataCommand (string category) {
			try {
				IsBusy = true;
				PinModels.Clear();



#if __ANDROID
				//var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
				//if (status != PermissionStatus.Granted) {
					//if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location)) {
					//	await Application.Current.MainPage.DisplayAlert("Error", "Your location is needed.", "OK");
					//}

					var stat = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationAlways);
					if (!stat.ContainsKey(Permission.Permission.LocationAlways)) {
						await Application.Current.MainPage.DisplayAlert("Error", "Please enable the location permission it in your settings", "OK");
						return;
					}
				//}
#endif
				await ExecuteGetCurrentPositionCommand(category);
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

		private async Task ExecuteGetCurrentPositionCommand(string category) {
			try {
				var locator = CrossGeolocator.Current;
				var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
				CurrentPosition = new Position(position.Latitude, position.Longitude);
			} catch(Exception e) {
				await Application.Current.MainPage.DisplayAlert("Error", "Could not get your location.", "OK");
			}
			await RequestMapPinsCommand(category);
		}

		private async Task RequestMapPinsCommand(string category) {
			PinRequestModel pinRequestModel = CreatePinRequestModel(category);
			await pinRequestHandler.RequestPins(pinRequestModel);
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
