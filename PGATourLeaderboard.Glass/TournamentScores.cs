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

namespace PGATourLeaderboard.Glass
{
	[Activity (Label = "Tournament Scores")]			
	public class TournamentScores : BaseActivity
	{
		#region Properties and Constants

		private CardScrollView _scoresScrollView { get; set; }
		private IList<Player> _playerScores { get; set; }

		#endregion

		#region Activity overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//var loading = FindViewById<SliderView> (Resource.Id.loading_slider);
			//loading.StartIndeterminate ();

			int tournamentId = 0;
			int.TryParse (Intent.GetStringExtra ("Tournament Id"), out tournamentId);

			LoadPlayerScores (tournamentId);
		}

		#endregion

		#region Private methods

		private async void LoadPlayerScores (int tournamentId)
		{
			_playerScores = await new PlayerManager (tournamentId).GetPlayerScores () as List<Player>;

			if (_playerScores.Count > 0) {
				_scoresScrollView = new CardScrollView (this);
				_scoresScrollView.Adapter = new TournamentCardScrollAdapter (SetupPlayerScoreCards ());
				_scoresScrollView.Activate ();

				/*
				// TODO: Future release to display more details on player's rounds in tournament?
				_scoresScrollView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) => {
					var scoreIntent = new Intent (this, typeof(TournamentScores));

					if (_playerScores.Count >= _scoresScrollView.SelectedItemPosition) {
						var playerId = _playerScores[_scoresScrollView.SelectedItemPosition].Id.ToString ();
						scoreIntent.PutExtra ("Player Id", playerId);
					}

					StartActivity (scoreIntent);
				};
				*/

				SetContentView (_scoresScrollView);
			} else {
				var card = new Card (this);
				card.SetText ("No player scores found...");
				card.SetFootnote ("There may have been an error or the tournament has not started yet.");

				SetContentView (card.View);
			}

			//var loading = FindViewById<SliderView> (Resource.Id.loading_slider);
			//loading.StopIndeterminate ();
		}

		private IEnumerable<Card> SetupPlayerScoreCards ()
		{
			List<Card> playerScoreCards = new List<Card> ();

			if (_playerScores.Count > 0) {
				foreach (Player p in _playerScores) {
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