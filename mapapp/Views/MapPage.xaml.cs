using System;
using System.Collections.Generic;
using mapapp.Helpers;
using mapapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.Views {
	public partial class MapPage : ContentPage {

		private MapViewModel mapViewModel;
		private CustomMap currentMap;

		public MapPage () {
			InitializeComponent();
			BindingContext = mapViewModel = new MapViewModel();
			mapViewModel.OnPinsRefreshed += MapViewModel_OnPinsRefreshed;
			mapViewModel.RequestMapDataCommand.Execute("Baby Needs Store");
		}

		void MapViewModel_OnPinsRefreshed (List<CustomPin> customPins) {
			if (currentMap != null)
				absLayout.Children.Remove(currentMap);
			filters.IsVisible = false;
			mapViewModel.CurrentPosition = new Position(14.6333, 121.0439);
			currentMap = CreateCustomMap(customPins);
			absLayout.Children.Add(currentMap);
		}

		private CustomMap CreateCustomMap (List<CustomPin> customPins) {
			CustomMap customMap = new CustomMap() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				MapType = MapType.Street,
				IsShowingUser = true,
				CustomPins = customPins,
				Origin = mapViewModel.CurrentPosition,
				HeightRequest = 700,
				WidthRequest = 450,
				BackgroundColor = Color.Red
			};
			return customMap;
		}

		void OnClickFilterMenu (object sender, System.EventArgs e) {
			filters.IsVisible = !filters.IsVisible;
		}

		void OnClickCategory (object sender, System.EventArgs e) {
			var button = (Button) sender;
			mapViewModel.RequestMapDataCommand.Execute(button.Text);
		}
	}
}