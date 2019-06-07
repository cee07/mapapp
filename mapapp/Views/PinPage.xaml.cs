using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class PinPage : ContentPage {

		private SavedPinView savedPinView;
		private RecenPinView recenPinView;
		private SubmitPinsListView submitPinsListView;

		private ToolbarItem addToolbarItem;

		private PinPageState pageState;

		private bool hasInitialized = false;

		public PinPage () {
			InitializeComponent();
			addToolbarItem = new ToolbarItem() {
				Text = "Add Pin"
			};
			InitializeViews();
		}

		protected override void OnAppearing () {
			base.OnAppearing();
			if (!hasInitialized) {
				hasInitialized = true;
				SetPageState(PinPageState.RecentPins);
			} else
				SetPageState(pageState);
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
					Title = "Saved Pins";
					savedPinView.IsVisible = true;
					savedPinView.InitializeSavedPins();
					break;
				case PinPageState.RecentPins:
					Title = "Recent Pins";
					recenPinView.IsVisible = true;
					recenPinView.InitializeRecentPins();
					break;
				case PinPageState.SubmittedPins:
					Title = "Submitted Pins";
					ToolbarItems.Add(addToolbarItem);
					submitPinsListView.IsVisible = true;
					submitPinsListView.InitializeSubmittedPins();
					break;
			}
			pageState = pinPageState;
		}

		void ResetPageState() {
			savedPinView.IsVisible = false;
			recenPinView.IsVisible = false;
			submitPinsListView.IsVisible = false;
			toolBarAdd.IsEnabled = false;
			ToolbarItems.Clear();
		}
	}

	public enum PinPageState {
		SavedPins,
		RecentPins,
		SubmittedPins
	}
}
