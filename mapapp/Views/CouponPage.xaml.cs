using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class CouponPage : ContentPage {
		public CouponPage (string html) {
			InitializeComponent();
			zoomableContent.Source = html;
		}
	}
}
