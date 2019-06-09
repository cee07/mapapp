using System;
using mapapp.Models;
using Xamarin.Forms.Maps;

namespace mapapp.Helpers {
	public class CustomPin : Pin {
		public PinType PinType { get; set; }
		public int CouponCount { get; set; }
		public PinModel Model { get; set; }
	}
}

public enum PinType {
	Default,
	Coupon
}