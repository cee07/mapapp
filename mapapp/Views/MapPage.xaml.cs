using System;
using System.Collections.Generic;
using mapapp.Helpers;
using mapapp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.Views {
	public partial class MapPage : ContentPage {

		private MapViewModel mapViewModel;

		public MapPage () {
			InitializeComponent();
			BindingContext = mapViewModel = new MapViewModel();
			mapViewModel.OnPinsRefreshed += MapViewModel_OnPinsRefreshed;
			mapViewModel.RequestPinsCommand.Execute(null);
		}

		void MapViewModel_OnPinsRefreshed (List<CustomPin> customPins) {
			mapViewModel.CurrentPosition = new Position(14.6333, 121.0439);
			CustomMap map = CreateCustomMap(customPins);
			absLayout.Children.Add(map);
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
	}
}