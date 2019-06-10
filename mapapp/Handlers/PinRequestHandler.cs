using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Helpers;
using mapapp.Models;
using Xamarin.Forms.Maps;

namespace mapapp.Handlers {
	public class PinRequestHandler : BaseDataHandler {

		public System.Action<List<CustomPin>> OnPinsRequested;

		private JsonWebRequest<List<PinModel>> request;

		public List<PinModel> PinModels {
			get; private set;
		}

		public async Task RequestPins(PinRequestModel pinRequestModel) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("key", pinRequestModel.Category);
			apiForm.AddField("lat", pinRequestModel.Latitude);
			apiForm.AddField("long", pinRequestModel.Longitude);
			apiForm.AddField("dist", pinRequestModel.Distance);
			apiForm.AddField("limit", pinRequestModel.Limit);
			request = JsonWebRequest<List<PinModel>>.CreateRequest(HttpMethod.POST, ApiUrl.API.GET_ESTABLISHMENT, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			PinModels = request.Data;
			OnPinsRequested?.Invoke(GetCustomPins(request.Data));
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}

		public List<CustomPin> GetCustomPins (List<PinModel> pins) {
			List<CustomPin> customPins = new List<CustomPin>();

			foreach (PinModel pin in pins) {
				CustomPin customPin = new CustomPin() {
					Label = pin.EstablishmentName,
					//Address = pin.Address,
					Position = new Position(Convert.ToDouble(pin.Latitude), Convert.ToDouble(pin.Longitude)),
					Type = Xamarin.Forms.Maps.PinType.Place,
					PinType = pin.PinModelType,
					CouponCount = Convert.ToInt32(pin.Coupon),
					Model = pin
				};
				customPins.Add(customPin);
			}

			return customPins;
		}
	}
}
