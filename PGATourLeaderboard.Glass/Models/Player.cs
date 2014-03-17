using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PGATourLeaderboard.Glass
{
	public class Player
	{
		#region Properties

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Country { get; set; }
		public int Position { get; set; }
		public int Score { get; set; }
		public int Strokes { get; set; }
		public double Money { get; set; }
		public double Points { get; set; }
		public IEnumerable<Round> Rounds { get; set; }

		public string ScoreDisplay
		{
			get {
				if (Score == 0)
					return "E";
				else if (Score > 0)
					return string.Format ("+{0}", Score);
				else
					return string.Format ("{0}", Score);
			}
		}

		#endregion

		#region Constructors

		public Player ()
		{
		}

		public Player (XElement t)
		{
			var id = t.Attributes ().Where (a => a.Name.LocalName == "id").FirstOrDefault ();
			var firstName = t.Attributes ().Where (a => a.Name.LocalName == "first_name").FirstOrDefault ();
			var lastName = t.Attributes ().Where (a => a.Name.LocalName == "last_name").FirstOrDefault ();
			var country = t.Attributes ().Where (a => a.Name.LocalName == "country").FirstOrDefault ();
			var position = t.Attributes ().Where (a => a.Name.LocalName == "position").FirstOrDefault ();
			var score = t.Attributes ().Where (a => a.Name.LocalName == "score").FirstOrDefault ();
			var strokes = t.Attributes ().Where (a => a.Name.LocalName == "strokes").FirstOrDefault ();
			var money = t.Attributes ().Where (a => a.Name.LocalName == "money").FirstOrDefault ();
			var points = t.Attributes ().Where (a => a.Name.LocalName == "points").FirstOrDefault ();

			Id = (id == null) ? string.Empty : id.Value;
			FirstName = (firstName == null) ? string.Empty : firstName.Value;
			LastName = (lastName == null) ? string.Empty : lastName.Value;
			Country = (country == null) ? string.Empty : country.Value;
			Position = (position == null) ? 0 : int.Parse (position.Value);
			Score = (score == null) ? 0 : int.Parse (score.Value);
			Strokes = (strokes == null) ? 0 : int.Parse (strokes.Value);
			Money = (money == null) ? 0 : double.Parse (money.Value);
			Points = (points == null) ? 0 : double.Parse (points.Value);

			// TODO: Fill in Rounds property...
		}

		#endregion
	}
}