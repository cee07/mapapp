using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class EmergencyContactPage : ContentPage {

		private EmergencyViewModel emergencyViewModel;

		public EmergencyContactPage () {
			InitializeComponent();
			BindingContext = emergencyViewModel = new EmergencyViewModel();

			foreach (var emergencyModel in emergencyViewModel.EmergencyList) {
				EmergencyContactView emergencyContactView = new EmergencyContactView(emergencyModel);
				emergencyParent.Children.Add(emergencyContactView);
			}
		}


	}
}
