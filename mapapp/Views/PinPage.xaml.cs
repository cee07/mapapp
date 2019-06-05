using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class PinPage : ContentPage {

		private SavedPinView savedPinView;

		public PinPage () {
			InitializeComponent();
		}

		protected override void OnAppearing () {
			base.OnAppearing();
			InitializeViews();
			SetPageState(PinPageState.SavedPins);
		}

		void InitializeViews() {
			savedPinView = new SavedPinView();
			pinDetailPanel.Children.Clear();
			pinDetailPanel.Children.Add(savedPinView);
		}

		void OnClickedButtonState (object sender, System.EventArgs e) {
			var imageButton = (ImageButton) sender;
			SetPageState((PinPageState) imageButton.CommandParameter);
		}

		void SetPageState (PinPageState pinPageState) {
			ResetPageState();
			switch (pinPageState) {
				case PinPageState.SavedPins:
					savedPinView.IsVisible = true;
					savedPinView.InitializeSavedPins();
					break;
				case PinPageState.RecentPins:
					break;
				case PinPageState.SubmittedPins:
					break;
			}
		}

		void ResetPageState() {
			savedPinView.IsVisible = false;
		}
	}

	public enum PinPageState {
		SavedPins,
		RecentPins,
		SubmittedPins
	}
}
