using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class MapSearchView : ContentView {

		private MapViewModel mapViewModel;

		public MapSearchView () {
			InitializeComponent();
		}

		public void SetMapView(MapViewModel mapViewModel) {
			BindingContext = this.mapViewModel = mapViewModel;
		}

		public void SetMoreResults(bool enabled) {
			moreResults.IsVisible = enabled;
		}

		void OnMoreResultsClicked (object sender, System.EventArgs e) {
			if (mapViewModel != null && mapViewModel.PinModels != null) {
				if (mapViewModel.PinModels.Count > 0) {
					mapViewModel.Limit = "50";
					mapViewModel.Distance = "100";
					mapViewModel.RequestMapDataCommand.Execute(mapViewModel.CategoryHolder);
					Navigation.PushAsync(new MoreResultsPage(mapViewModel));
				} else {
					Application.Current.MainPage.DisplayAlert("Request Error", "There's no available establishment.", "OK");
				}
			} else {
				Application.Current.MainPage.DisplayAlert("Request Error", "There's no available establishment.", "OK");
			}
		}

		void OnItemTapped (object sender, Xamarin.Forms.ItemTappedEventArgs e) {
			var pinModel = (PinModel) e.Item;
			if (pinModel != null) {
				string recentPinsData = Preferences.Get("RecentPins", null);
				string pinData = null;
				if (!string.IsNullOrEmpty(recentPinsData)) {
					var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(recentPinsData);
					bool hasSaved = savedPins.Exists(x => x.EstablishmentName == pinModel.EstablishmentName &&
												 x.Address == pinModel.Address);
					if (hasSaved) {
						savedPins.Remove(pinModel);
						savedPins.Add(pinModel);
					} else {
						savedPins.Add(pinModel);
					}
					pinData = JsonConvert.SerializeObject(savedPins);
				} else {
					var pinList = new List<PinModel>() { pinModel };
					pinData = JsonConvert.SerializeObject(pinList);
				}
				Preferences.Set("RecentPins", pinData);
				Navigation.PushAsync(new PinDetailPage(pinModel) {
					Title = pinModel.EstablishmentName
				});
				//NavigationPage newPage = new NavigationPage(new PinDetailPage(pinModel)) {
				//	BarBackgroundColor = Color.FromHex("#C54F4E")
				//};
				//Navigation.PushAsync(newPage);
			}
		}
	}
}
