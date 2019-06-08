using System;
using System.Collections.Generic;
using mapapp.Models;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SubscriptionTicketsView : ContentView {

		private SubscriptionModel subscriptionModel;

		public SubscriptionTicketsView (SubscriptionModel subscriptionModel) {
			InitializeComponent();
			BindingContext = subscriptionModel;
			this.subscriptionModel = subscriptionModel;
		}

		public string SubscriptionLink { get { return subscriptionModel.URL; } }
	}
}
