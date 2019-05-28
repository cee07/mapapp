using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
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

		void OnMoreResultsClicked (object sender, System.EventArgs e) {
			if (mapViewModel.PinModels.Count > 0) {
				mapViewModel.Limit = "50";
				mapViewModel.RequestMapDataCommand.Execute(mapViewModel.CategoryHolder);
				Navigation.PushAsync(new MoreResultsPage(mapViewModel));
			} else {
				Application.Current.MainPage.DisplayAlert("Request Error", "There's no available establishment.", "OK");
			}
		}

		void OnItemTapped (object sender, Xamarin.Forms.ItemTappedEventArgs e) {
			var pinModel = (PinModel) e.Item;
			if (pinModel != null)
				Navigation.PushAsync(new PinDetailPage(pinModel));
		}
	}
}
