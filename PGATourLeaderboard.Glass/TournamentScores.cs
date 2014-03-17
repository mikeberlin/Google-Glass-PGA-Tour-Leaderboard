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
	[Activity (Label = "Tournament Scores")]			
	public class TournamentScores : Activity
	{
		#region Properties and Constants

		private readonly string PlayerScoresBaseUri = "https://api.sportsdatallc.org/golf-t1/leaderboard/pga/2014/tournaments/{0}/leaderboard.xml?api_key={1}";
		private readonly XNamespace PlayerScoreNamespace = "http://feed.elasticstats.com/schema/golf/tournament/leaderboard-v1.0.xsd";

		private CardScrollView ScoresScrollView { get; set; }
		private string TournamentId { get; set; }

		private IList<Player> _playerScores;
		private IList<Player> PlayerScores {
			get {
				if (_playerScores == null) {
					var playerScoresUri = string.Format(PlayerScoresBaseUri, TournamentId, MainActivity.API_KEY);

					//_playerScores = XDocument.Load (Assets.Open("leaderboard_honda_classic.xml"))
					_playerScores = XDocument.Load (playerScoresUri)
						.Descendants (PlayerScoreNamespace + "leaderboard")
						.Descendants (PlayerScoreNamespace + "player")
						.Select (ps => new Player (ps))
						.ToList ();
				}

				return _playerScores;
			}
		}

		#endregion

		#region Activity overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			TournamentId = Intent.GetStringExtra ("Tournament Id") ?? string.Empty;

			if (PlayerScores.Count > 0) {
				ScoresScrollView = new CardScrollView (this);
				ScoresScrollView.Adapter = new TournamentCardScrollAdapter (SetupPlayerScoreCards ());
				ScoresScrollView.Activate ();

				// TODO: Future release to display more details on player's rounds in tournament?
				/*
			ScoresScrollView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) => {
				var scoreIntent = new Intent (this, typeof(TournamentScores));

				if (PlayerScores.Count >= ScoresScrollView.SelectedItemPosition) {
					var playerId = PlayerScores[ScoresScrollView.SelectedItemPosition].Id.ToString ();
					scoreIntent.PutExtra ("Player Id", playerId);
				}

				StartActivity (scoreIntent);
			};
			*/

				SetContentView (ScoresScrollView);
			} else {
				var card = new Card (this);
				card.SetText ("No player scores found...");
				card.SetFootnote ("There may have been an error or the tournament has not started yet.");

				SetContentView (card.ToView ());
			}
		}

		#endregion

		#region Private methods

		private IEnumerable<Card> SetupPlayerScoreCards ()
		{
			List<Card> playerScoreCards = new List<Card> ();

			if (PlayerScores.Count > 0) {
				foreach (Player p in PlayerScores) {
					Card playerScore = new Card (this);
					playerScore.SetText (string.Format ("{0} {1}\n{2}", p.FirstName, p.LastName, p.ScoreDisplay));
					playerScore.SetFootnote (string.Format ("Total strokes: {0}", p.Strokes));

					playerScoreCards.Add (playerScore);
				}
			}

			return playerScoreCards;
		}

		#endregion
	}
}