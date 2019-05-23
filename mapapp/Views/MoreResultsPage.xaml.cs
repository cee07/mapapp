using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class MoreResultsPage : ContentPage {

		public MoreResultsPage (MapViewModel mapViewModel) {
			InitializeComponent();
			BindingContext = mapViewModel;
		}

	}
}
