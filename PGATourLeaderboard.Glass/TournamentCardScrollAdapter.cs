using System.Collections.Generic;
using System.Linq;
using Android.Glass.App;
using Android.Glass.Widget;

namespace PGATourLeaderboard.Glass
{
	public class TournamentCardScrollAdapter : CardScrollAdapter
	{
		#region properties, instance vars, etc.

		private IEnumerable<Card> TournamentCards;

		#endregion

		#region constructors

		public TournamentCardScrollAdapter (IEnumerable<Card> tournamentCards)
		{
			TournamentCards = tournamentCards;
		}

		#endregion

		#region CardScrollAdapter overrides

		public override Java.Lang.Object GetItem (int position)
		{
			return TournamentCards.ElementAt (position);
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			return TournamentCards.ElementAt (position).View;
		}

		public override int Count {
			get
			{
				return TournamentCards.Count ();
			}
		}

		public override int GetPosition (Java.Lang.Object item)
		{
			if (item.GetType () == typeof(Card)) {
				return TournamentCards.ToList ().IndexOf (item as Card);
			} else {
				return -1;
			}
		}

		#endregion
	}
}