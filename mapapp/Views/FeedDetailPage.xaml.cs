using System;
using System.Collections.Generic;
using mapapp.Models;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class FeedDetailPage : ContentPage {

		public FeedDetailPage (FeedModel feedModel) {
			InitializeComponent();
			BindingContext = feedModel;
		}

	}
}
