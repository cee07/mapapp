using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace mapapp.Droid {
	[Activity(Label = "MApp", Theme = "@style/MyTheme.Splash", Icon = "@drawable/mapp_icon", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : AppCompatActivity {

		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		public override void OnCreate (Bundle savedInstanceState, PersistableBundle persistentState) {
			base.OnCreate(savedInstanceState, persistentState);
		}

		// Launches the startup task
		protected override void OnResume () {
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		// Simulates background work that happens behind the splash screen
		async void SimulateStartup () {
			await Task.Delay(1000); // Simulate a bit of startup work.
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}
