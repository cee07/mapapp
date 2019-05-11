using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class EmergencyPage : ContentPage {

		private EmergencyViewModel emergencyViewModel;

		public EmergencyPage () {
			InitializeComponent();
			BindingContext = emergencyViewModel = new EmergencyViewModel();
		}
	}
}
