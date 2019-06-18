using System;
using System.Collections.Generic;
using mapapp.Models;
using Plugin.Messaging;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class EmergencyContactView : ContentView {

		private EmergencyModel emergencyModel;

		public EmergencyContactView (EmergencyModel model) {
			InitializeComponent();
			BindingContext = model;
			emergencyModel = model;
		}

		void OnNumberTapped (object sender, System.EventArgs e) {
			var phoneDialer = CrossMessaging.Current.PhoneDialer;
			if (phoneDialer.CanMakePhoneCall)
				phoneDialer.MakePhoneCall(emergencyModel.Contact);
			else
				Application.Current.MainPage.DisplayAlert("Error",
				                                          "Unable to make phone call",
				                                          "OK");
		}
	}
}
