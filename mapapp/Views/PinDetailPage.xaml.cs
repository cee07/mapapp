using System;
using System.Collections.Generic;
using mapapp.Helpers;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace mapapp.Views {
	public partial class PinDetailPage : ContentPage {

		private PinDetailPageViewModel pinDetailPageViewModel;

		public PinDetailPage () {
			InitializeComponent();
		}

		public PinDetailPage (PinModel pinModel) {
			InitializeComponent();
			BindingContext = pinDetailPageViewModel = new PinDetailPageViewModel(pinModel);
			Title = pinModel.EstablishmentName;
			var customPins = pinDetailPageViewModel.Pins.ToList();
			mapParent.Children.Add(CreateCustomMap(customPins));
		}

		private CustomMap CreateCustomMap (List<CustomPin> customPins) {
			CustomMap customMap = new CustomMap() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				MapType = MapType.Street,
				IsShowingUser = true,
				CustomPins = customPins,
				Origin = pinDetailPageViewModel.CurrentPosition,
			};
			return customMap;
		}
	}
}
