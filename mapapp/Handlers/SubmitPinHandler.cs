using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mapapp.Handlers {
	public class SubmitPinHandler : BaseDataHandler {

		public System.Action OnPinRequested;

		private JsonWebRequest<BaseDataModel[]> request; 

		public async Task SubmitPin (string email, string establishmentName,
		                            string address, string contact,
		                            string category, string info) {
			APIForm aPIForm = new APIForm();
			aPIForm.AddField("email", email);
			aPIForm.AddField("establishment", establishmentName);
			aPIForm.AddField("address", address);
			aPIForm.AddField("contact", contact);
			aPIForm.AddField("category", category);
			aPIForm.AddField("add_info", info);
			request = JsonWebRequest<BaseDataModel[]>.CreateRequest(HttpMethod.POST, ApiUrl.API.SUBMIT_PIN, aPIForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			await Application.Current.MainPage.DisplayAlert("Thank you for submitting a pin!",
			                                                "Once verified, all your submitted pins will appear here." +
			                                                " This will help you earn a badge.",
			                                                "OK");
			OnPinRequested?.Invoke();
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
