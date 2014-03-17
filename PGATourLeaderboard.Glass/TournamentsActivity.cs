using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Glass.App;
using Android.Glass.Widget;
using Android.OS;

namespace PGATourLeaderboard.Glass
{
	[Activity (Label = "2014 PGA Tour Tournaments")]			
	public class TournamentsActivity : Activity
	{
		#region Properties and Constants

		private readonly string TournamentsUri = "https://api.sportsdatallc.org/golf-t1/schedule/pga/2014/tournaments/schedule.xml?api_key={0}";
		private readonly XNamespace TournamentsNamespace = "http://feed.elasticstats.com/schema/golf/schedule-v1.0.xsd";

		private CardScrollView TournamentsScrollView { get; set; }

		private IList<Tournament> _tournaments;
		private IList<Tournament> Tournaments {
			get {
				if (_tournaments == null) {
					//_tournaments = XDocument.Load (Assets.Open("tournaments.xml"))
					_tournaments = XDocument.Load (string.Format(TournamentsUri, MainActivity.API_KEY))
						.Descendants (TournamentsNamespace + "tournament")
						.Select (t => new Tournament (t))
						.ToList ();
				}

				return _tournaments;
			}
		}

		#endregion

		#region Activity overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (Tournaments.Count > 0) {
				TournamentsScrollView = new CardScrollView (this);
				TournamentsScrollView.Adapter = new TournamentCardScrollAdapter (SetupTournamentCards ());
				TournamentsScrollView.Activate ();

				TournamentsScrollView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) => {
					var scoreIntent = new Intent (this, typeof(TournamentScores));

					if (Tournaments.Count >= TournamentsScrollView.SelectedItemPosition) {
						var tournamentId = Tournaments [TournamentsScrollView.SelectedItemPosition].Id.ToString ();
						scoreIntent.PutExtra ("Tournament Id", tournamentId);
					}

					StartActivity (scoreIntent);
				};

				SetContentView (TournamentsScrollView);
			} else {
				var card = new Card (this);
				card.SetText ("No tournmanets found...");
				card.SetFootnote ("There was an error downloading the tournament data. Please try again later.");

				SetContentView (card.ToView ());
			}
		}

		#endregion

		#region Private methods

		private IEnumerable<Card> SetupTournamentCards ()
		{
			List<Card> tournamentCards = new List<Card> ();

			if (Tournaments.Count > 0) {
				foreach (Tournament t in Tournaments) {
					Card tournament = new Card (this);
					tournament.SetText (t.Name);
					tournament.SetFootnote (
						string.Format ("{0} - {1}",
							t.StartDate.ToShortDateString (),
							t.EndDate.ToShortDateString ()
						)
					);

					tournamentCards.Add (tournament);
				}
			}

			return tournamentCards;
		}

		#endregion
	}
}