using System;
using System.Linq;
using System.Xml.Linq;
using Android.App;
using Android.Glass.App;
using Android.OS;
using Android.Views;

namespace PGATourLeaderboard.Glass
{
	[Activity (Label = "Main Activity", Icon = "@drawable/Icon", MainLauncher = true, Enabled = true)]
	[IntentFilter (new String[]{ "com.google.android.glass.action.VOICE_TRIGGER" })]
	[MetaData ("com.google.android.glass.VoiceTrigger", Resource = "@xml/voicetriggerstart")]
	public class MainActivity : Activity
	{
		#region Constants

		private static string _apiKey;
		public static string API_KEY
		{
			get {
				if (string.IsNullOrEmpty (_apiKey)) {
					var apiKeyDoc = XDocument.Load (Assets.Open ("leaderboard_honda_classic.xml")).Descendants ("sportsdata");

					if (apiKeyDoc != null) {
						_apiKey = apiKeyDoc.Attributes ().Where (a => a.Name.LocalName == "apikey").FirstOrDefault ();
					}
				}

				return _apiKey;
			}
		}

		#endregion

		#region Activity overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var card = new Card (this);
			card.SetText ("Welcome to the PGA Tour Leaderboard!");
			card.SetFootnote ("Tap to see the list of tournaments...");

			SetContentView (card.ToView ());
		}

		public override bool OnKeyDown (Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.DpadCenter) {
				StartActivity (typeof(TournamentsActivity));
				return true;
			}

			return base.OnKeyDown (keyCode, e);
		}

		#endregion
	}
}