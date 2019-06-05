using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class PinPage : ContentPage {

		private SavedPinView savedPinView;
		private RecenPinView recenPinView;
		private SubmitPinsListView submitPinsListView;

		public PinPage () {
			InitializeComponent();
			InitializeViews();
			SetPageState(PinPageState.SavedPins);
		}

		void InitializeViews() {
			savedPinView = new SavedPinView();
			recenPinView = new RecenPinView();
			submitPinsListView = new SubmitPinsListView();
			pinDetailPanel.Children.Clear();
			pinDetailPanel.Children.Add(savedPinView);
			pinDetailPanel.Children.Add(recenPinView);
			pinDetailPanel.Children.Add(submitPinsListView);
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
					recenPinView.IsVisible = true;
					recenPinView.InitializeRecentPins();
					break;
				case PinPageState.SubmittedPins:
					submitPinsListView.IsVisible = true;
					submitPinsListView.InitializeSubmittedPins();
					break;
			}
		}

		void ResetPageState() {
			savedPinView.IsVisible = false;
			recenPinView.IsVisible = false;
			submitPinsListView.IsVisible = false;
		}
	}

	public enum PinPageState {
		SavedPins,
		RecentPins,
		SubmittedPins
	}
}
