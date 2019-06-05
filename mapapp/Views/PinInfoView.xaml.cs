using System;
using System.Collections.Generic;
using mapapp.Models;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class PinInfoView : ContentView {

		public PinInfoView (PinModel pinModel) {
			InitializeComponent();
			BindingContext = pinModel;
			PinModel = pinModel;
		}

		public PinModel PinModel { get; private set; }
	}
}