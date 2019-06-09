using System;
using System.Collections.Generic;
using mapapp.Models;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class BadgeValueView : ContentView {

		public BadgeValueView () {
			InitializeComponent();
		}

		public BadgeValueView (BadgeModel badgesModel) {
			InitializeComponent();
			BindingContext = badgesModel;
		}
	}
}
