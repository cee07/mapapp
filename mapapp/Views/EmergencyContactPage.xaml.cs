﻿using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class EmergencyContactPage : ContentPage {

		private EmergencyViewModel emergencyViewModel;

		public EmergencyContactPage () {
			InitializeComponent();
			BindingContext = emergencyViewModel = new EmergencyViewModel();

			for (int index = 0 ; index < emergencyViewModel.EmergencyList.Count ; index++) {
				var emergencyModel = emergencyViewModel.EmergencyList[index];
				if (emergencyModel != null) {
					if (index > 0) {
						BoxView boxView =new BoxView() { Color = Color.Teal, HeightRequest = 3, Margin = new Thickness(15,0,15,0) };
						emergencyParent.Children.Add(boxView);
					}
					EmergencyContactView emergencyContactView = new EmergencyContactView(emergencyModel);
					emergencyParent.Children.Add(emergencyContactView);
				}
			}
		}
	}
}
