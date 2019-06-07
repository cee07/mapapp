using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class FeedPage : ContentPage {

		private FeedViewModel feedViewModel;

		private bool isInitialized = false;

		public FeedPage () {
			InitializeComponent();
			BindingContext = feedViewModel = new FeedViewModel();
		}

		protected override void OnAppearing () {
			base.OnAppearing();
			if (!isInitialized) {
				isInitialized = true;
				return;
			}
			feedViewModel.RequestFeedCommand.Execute(null);
		}

		async void OnTappedFeed (object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
			var feedModel = (FeedModel) e.SelectedItem;
			if (feedModel != null) {
				FeedDetailPage feedDetailPage = new FeedDetailPage(feedModel);
				await Navigation.PushAsync(feedDetailPage);
				feedList.SelectedItem = null;
			}
		}
	}
}
