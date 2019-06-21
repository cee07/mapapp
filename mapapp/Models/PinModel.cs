using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mapapp.Models {
	public class PinModel {
		[JsonProperty("establishment_id")]
		public string EstablishmentID { get; set; }

		[JsonProperty("establishment")]
		public string EstablishmentName { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("contact")]
		public string Contact { get; set; }

		[JsonProperty("operation")]
		public string Operation { get; set; }

		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("latitude")]
		public string Latitude { get; set; }

		[JsonProperty("longitude")]
		public string Longitude { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("details")]
		public string Details { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("fb")]
		public string Facebook { get; set; }

		[JsonProperty("ig")]
		public string Instagram { get; set; }

		[JsonProperty("twitter")]
		public string Twitter { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("e_like")]
		public string ELike { get; set; }

		[JsonProperty("rate")]
		public string Rate { get; set; }

		[JsonProperty("coupon")]
		public string Coupon { get; set; }

		[JsonProperty("coupon_image")]
		public string CouponImage { get; set; }

		[JsonProperty("coupon_link")]
		public string CouponLink { get; set; }

		[JsonProperty("distance")]
		public string Distance { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("active")]
		public string Active { get; set; }

		public PinType PinModelType {
			get {
				PinType pinType = PinType.Default;
				switch (Coupon) {
					case "1":
						pinType = PinType.Coupon;
						break;
				}
				return pinType;
			}
		}

		public string StarRating {
			get { return Rate + " STARS"; }
		}
	}
}
