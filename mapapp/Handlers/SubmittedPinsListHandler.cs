using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;

namespace mapapp.Handlers {
	public class SubmittedPinsListHandler : BaseDataHandler {

		public System.Action<List<PinModel>> OnPinsRequested;

		private JsonWebRequest<List<PinModel>> request;

		public async Task RequestPinList(string email) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			request = JsonWebRequest<List<PinModel>>.CreateRequest(HttpMethod.POST, ApiUrl.API.SUBMIT_PIN_LIST, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnPinsRequested?.Invoke(request.Data);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
