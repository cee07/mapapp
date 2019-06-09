using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SubmitNewPin : ContentPage {

		private SubmitPinPageViewModel submitPinPageViewModel;

		public SubmitNewPin () {
			InitializeComponent();
			BindingContext = submitPinPageViewModel = new SubmitPinPageViewModel();
			submitPinPageViewModel.OnPinSubmitted += SubmitPinPageViewModel_OnPinSubmitted;
		}

		void Handle_Clicked (object sender, System.EventArgs e) {
			submitPinPageViewModel.Name = pinName.Text;
			submitPinPageViewModel.Address = pinAddress.Text;
			submitPinPageViewModel.Description = pinDescription.Text;
			submitPinPageViewModel.SubmitPinCommand.Execute(null);
		}

		async void SubmitPinPageViewModel_OnPinSubmitted (object sender, System.EventArgs e) {
			await Navigation.PopAsync();
		}

		void Handle_SelectedIndexChanged (object sender, System.EventArgs e) {
			Picker picker = sender as Picker;
			var selectedItem = picker.SelectedItem;
			submitPinPageViewModel.Category = selectedItem.ToString();
		}
	}
}
