using System;
using System.Collections.Generic;
using mapapp.Helpers;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Xamarin.Essentials;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;

namespace mapapp.Views {
	public partial class PinDetailPage : ContentPage {

		private const string STAR_PREFIX = "star_";
		private const string STAR_DEFAULT = "default";
		private const string STAR_PRESSED = "pressed";
		private const string STAR_EXTENSION = ".png";

		private List<ImageButton> imageButtons = new List<ImageButton>();

		private PinDetailPageViewModel pinDetailPageViewModel;

		private PinModel pinModel;

		public PinDetailPage (PinModel pinModel) {
			InitializeComponent();
			BindingContext = pinDetailPageViewModel = new PinDetailPageViewModel(pinModel);
			this.pinModel = pinModel;
			var customPins = pinDetailPageViewModel.Pins.ToList();
			CustomMap customMap = CreateCustomMap(customPins);
			mapParent.Children.Add(customMap);
			imageButtons.Add(star0);
			imageButtons.Add(star1);
			imageButtons.Add(star2);
			imageButtons.Add(star3);
			imageButtons.Add(star4);
			pinDetailPageViewModel.CheckDataCommand.Execute(null);
			if (string.IsNullOrEmpty(pinModel.CouponImage))
				couponImage.Source = "xamarin_logo.png";
			else {
				couponImage.Source = new UriImageSource {
					Uri = new Uri(pinModel.CouponImage)
				};
			}

			if (!string.IsNullOrEmpty(pinModel.CouponLink)) {
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.Tapped += async (sender, e) => {
					Position currentPosition = new Position(0, 0);
					var stat = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
					if (stat != PermissionStatus.Granted) {
						var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
						if (results.ContainsKey(Permission.Location)) {
							var locator = CrossGeolocator.Current;
							var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
							currentPosition = new Position(position.Latitude, position.Longitude);
							Device.OpenUri(new Uri(string.Format("{0}?email={1}&est_id={2}&lat={3}&long={4}",
															 pinModel.CouponLink,
															 Preferences.Get("email", null),
															 pinModel.EstablishmentID, currentPosition.Latitude, currentPosition.Longitude)));
						}
					} else if (stat == PermissionStatus.Granted) {
						var locator = CrossGeolocator.Current;
						var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30));
						currentPosition = new Position(position.Latitude, position.Longitude);
						Device.OpenUri(new Uri(string.Format("{0}?est_id={1}&email={2}&lat={3}&long={4}", 
						                                     pinModel.CouponLink,
						                                     pinModel.EstablishmentID,
						                                     Preferences.Get("email", null),
															 currentPosition.Latitude, currentPosition.Longitude)));
					} else {
						await Application.Current.MainPage.DisplayAlert("Error", "Could not get your location. Please enable location service in your settings", "OK");
					}
				};
				couponImage.GestureRecognizers.Add(tapGestureRecognizer);
			}
		}

		void ResetStarButtonStates() {
			foreach (ImageButton imageButton in imageButtons)
				imageButton.Source = STAR_PREFIX + STAR_DEFAULT + STAR_EXTENSION;
		}

		void OnClickStarButton (object sender, System.EventArgs e) {
			ResetStarButtonStates();
			var starButton = (ImageButton) sender;
			if (starButton != null) {
				int limit = Convert.ToInt32(starButton.CommandParameter) + 1;
				for (int index = 0 ; index < limit; index++ ) {
					ImageButton imageButton = imageButtons[index];
					imageButton.Source = STAR_PREFIX + STAR_PRESSED + STAR_EXTENSION;
				}
				pinDetailPageViewModel.RatingValue = limit;
				pinDetailPageViewModel.RateCommand.Execute(null);
			}
		}

		void OnClickWaze(object sender, System.EventArgs e) {
			string latLong = string.Format("https://waze.com/ul?ll={0},{1}&z=10", pinModel.Latitude, pinModel.Longitude);
			Device.OpenUri(new Uri(latLong));
		}

		private CustomMap CreateCustomMap (List<CustomPin> customPins) {
			CustomMap customMap = new CustomMap() {
				MapType = MapType.Street,
				IsShowingUser = true,
				CustomPins = customPins,
				Origin = pinDetailPageViewModel.CurrentPosition,
			};
			return customMap;
		}

	}
}