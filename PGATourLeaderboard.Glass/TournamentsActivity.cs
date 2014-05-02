using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Glass.App;
using Android.Glass.Widget;
using Android.OS;
using GlassProgressBar;
using PGATourLeaderboard.Glass.Models;
using System.Timers;
using Android.Views;

namespace PGATourLeaderboard.Glass
{
	[Activity (Label = "2014 PGA Tour Tournaments")]			
	public class TournamentsActivity : BaseActivity
	{
		#region Properties

		private CardScrollView TournamentsScrollView { get; set; }
		private Timer _loadingTimer;

		#endregion

		#region Activity overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (TournamentManager.IsLoadingTournaments) {
				base.ShowLoadingIndicator ();

				_loadingTimer = new Timer ();
				_loadingTimer.Interval = 1000;
				_loadingTimer.Elapsed += OnTimerCallback;
				_loadingTimer.AutoReset = true;
				_loadingTimer.Start ();
			} else {
				SetupUI ();
			}
		}

		#endregion

		#region Private methods

		private void OnTimerCallback (object sender, ElapsedEventArgs e)
		{
			if (!TournamentManager.IsLoadingTournaments) {
				_loadingTimer.Stop ();
				_loadingTimer.Close ();
				_loadingTimer.Dispose ();
				_loadingTimer = null;

				//base.HideLoadingIndicator ();
				SetupUI ();
			}
		}

		private void SetupUI ()
		{
			if (TournamentManager.Tournaments.Count () > 0) {
				TournamentsScrollView = new CardScrollView (this);
				TournamentsScrollView.Adapter = new TournamentCardScrollAdapter (SetupTournamentCards ());
				TournamentsScrollView.Activate ();

				TournamentsScrollView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) => {
					var scoreIntent = new Intent (this, typeof(TournamentScores));

					if (TournamentManager.Tournaments.Count () >= TournamentsScrollView.SelectedItemPosition) {
						var tournament = TournamentManager.Tournaments.ElementAtOrDefault (TournamentsScrollView.SelectedItemPosition);
						if (tournament != null) {
							var tournamentId = tournament.Id.ToString ();
							scoreIntent.PutExtra ("Tournament Id", tournamentId);
						}
					}

					StartActivity (scoreIntent);
				};

				SetContentView (TournamentsScrollView);
			} else {
				var card = new Card (this);
				card.SetText ("No tournmanets found...");
				card.SetFootnote ("There was an error downloading the tournament data. Please try again later.");

				SetContentView (card.View);
			}
		}

		private IEnumerable<Card> SetupTournamentCards ()
		{
			List<Card> tournamentCards = new List<Card> ();

			if (TournamentManager.Tournaments.Count () > 0) {
				foreach (Tournament t in TournamentManager.Tournaments) {
					Card tournament = new Card (this);
					tournament.SetText (t.Name);
					tournament.SetFootnote (string.Format ("{0} - {1}", t.StartDate.ToShortDateString (), t.EndDate.ToShortDateString ()));
					tournamentCards.Add (tournament);
				}
			}

			return tournamentCards;
		}

		#endregion
	}
}