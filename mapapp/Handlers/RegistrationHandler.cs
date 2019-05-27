using System;
using System.Threading.Tasks;

namespace mapapp.Handlers {
	public class RegistrationHandler : BaseDataHandler {

		public System.Action OnRequestFinished;

		private JsonWebRequest<BaseDataModel> request;

		public async Task Register(string email) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			request = JsonWebRequest<BaseDataModel>.CreateRequest(HttpMethod.POST, ApiUrl.API.REGISTER, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnRequestFinished?.Invoke();
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
			OnRequestFinished?.Invoke();
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
