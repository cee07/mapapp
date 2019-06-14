using System;
using System.Collections.Generic;
using mapapp.Helpers;
using mapapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.Views {
	public partial class MapPage : ContentPage {

		private MapViewModel mapViewModel;
		private MapSearchView mapSearchView;
		private CustomMap currentMap;

		public MapPage () {
			InitializeComponent();
			BindingContext = mapViewModel = new MapViewModel();
			mapSearchView = new MapSearchView();
			mapViewModel.OnCurrentLocationRequested += LoadMap;
			mapViewModel.RequestCurrentLocationCommand.Execute(null);
			mapViewModel.OnPinsRefreshed += MapViewModel_OnPinsRefreshed;
		}

		void LoadMap() {
			currentMap = CreateCustomMap(null);
			absLayout.Children.Add(currentMap);
		}
 
		void MapViewModel_OnPinsRefreshed (List<CustomPin> customPins) {
			mapSearchView.SetMapView(mapViewModel);
			if (currentMap != null)
				absLayout.Children.Remove(currentMap);
			filters.IsVisible = false;
			//mapViewModel.CurrentPosition = new Position(14.6333, 121.0439); //TODO: REMOVE, FOR TESTING ONLY
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
				WidthRequest = 450
			};
			AbsoluteLayout.SetLayoutFlags(customMap, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(customMap, new Rectangle(1,1,0.5,0.5));
			return customMap;
		}

		void OnSearchBarChanged (object sender, Xamarin.Forms.TextChangedEventArgs e) {
			string stringValue = e.NewTextValue;

			mapViewModel.CatKey = "key";

			if (!string.IsNullOrEmpty(stringValue)) {
				if (stringValue.Length > 30)
					stringValue = stringValue.Remove(stringValue.Length - 1);
			}

			searchbar.Text = stringValue;

			if (searchbar.Text.Length > 2) {
				mapViewModel.Limit = "5";
				mapViewModel.Distance = "10";
				mapViewModel.CategoryHolder = stringValue;
				if (!grid.Children.Contains(mapSearchView)) {
					grid.Children.Add(mapSearchView, 0, 0);
					Grid.SetColumnSpan(mapSearchView, 2);
				} else {
					mapSearchView.IsVisible = true;
				}
				mapViewModel.RequestMapDataCommand.Execute(stringValue);
			} else {
				if (grid.Children.Contains(mapSearchView))
					mapSearchView.IsVisible = false;
			}
		}

		void OnClickFilterMenu (object sender, System.EventArgs e) {
			filters.IsVisible = !filters.IsVisible;
		}

		void OnClickCategory (object sender, System.EventArgs e) {
			var button = (ImageButton) sender;
			mapViewModel.CatKey = "cat";
			mapViewModel.Limit = "10";
			mapViewModel.RequestMapDataCommand.Execute(button.CommandParameter);
			filters.IsVisible = false;
		}
	}
}