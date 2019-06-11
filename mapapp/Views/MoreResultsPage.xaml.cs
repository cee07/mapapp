using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class MoreResultsPage : ContentPage {

		public MoreResultsPage (MapViewModel mapViewModel) {
			InitializeComponent();
			BindingContext = mapViewModel;
		}

		void OnItemTapped (object sender, Xamarin.Forms.ItemTappedEventArgs e) {
			var pinModel = (PinModel) e.Item;
			if (pinModel != null) {
				string recentPinsData = Preferences.Get("RecentPins", null);
				string pinData = null;
				if (!string.IsNullOrEmpty(recentPinsData)) {
					var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(recentPinsData);
					if (savedPins.Contains(pinModel)) {
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