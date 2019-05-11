using System;
using System.Collections.Generic;
using mapapp.Models;
using Plugin.Messaging;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class EmergencyContactView : ContentView {

		public EmergencyContactView (EmergencyModel model) {
			InitializeComponent();
			BindingContext = model;
		}

		void OnNumberTapped (object sender, System.EventArgs e) {
			var label = (Label) sender;
			if (label != null) {
				var phoneDialer = CrossMessaging.Current.PhoneDialer;
				if (phoneDialer.CanMakePhoneCall)
					phoneDialer.MakePhoneCall(label.Text);
				else
					Application.Current.MainPage.DisplayAlert("Error",
					                                          "Unable to make phone call",
					                                          "OK");
			}
		}
	}
}
