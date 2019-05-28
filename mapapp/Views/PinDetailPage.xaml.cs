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

		private const string STAR_PREFIX = "star_";
		private const string STAR_DEFAULT = "default";
		private const string STAR_PRESSED = "pressed";
		private const string STAR_EXTENSION = ".png";

		private List<ImageButton> imageButtons = new List<ImageButton>();

		private PinDetailPageViewModel pinDetailPageViewModel;

		public PinDetailPage (PinModel pinModel) {
			InitializeComponent();
			BindingContext = pinDetailPageViewModel = new PinDetailPageViewModel(pinModel);
			Title = pinModel.EstablishmentName;
			var customPins = pinDetailPageViewModel.Pins.ToList();
			CustomMap customMap = CreateCustomMap(customPins);
			mapParent.Children.Add(customMap);
			imageButtons.Add(star0);
			imageButtons.Add(star1);
			imageButtons.Add(star2);
			imageButtons.Add(star3);
			imageButtons.Add(star4);
			pinDetailPageViewModel.CheckRateCommand.Execute(null);
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