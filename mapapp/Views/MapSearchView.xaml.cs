using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class MapSearchView : ContentView {

		public MapSearchView (MapViewModel mapViewModel) {
			InitializeComponent();
		}

		void Handle_Clicked (object sender, System.EventArgs e) {
			Navigation.PushAsync(new EmergencyContactPage());
		}
	}
}
