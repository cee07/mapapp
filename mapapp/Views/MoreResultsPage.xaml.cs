using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class MoreResultsPage : ContentPage {

		public MoreResultsPage (MapViewModel mapViewModel) {
			InitializeComponent();
			BindingContext = mapViewModel;
		}

		void OnItemTapped (object sender, Xamarin.Forms.ItemTappedEventArgs e) {
			var pinModel = (PinModel) e.Item;
			if (pinModel != null)
				Navigation.PushAsync(new PinDetailPage(pinModel));
		}
	}
}
