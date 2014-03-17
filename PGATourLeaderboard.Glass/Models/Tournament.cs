using System;
using System.Xml.Linq;
using System.Linq;

namespace PGATourLeaderboard.Glass
{
	public class Tournament
	{
		#region Properties

		public string Id { get; set; }
		public string Name { get; set; }
		public float Purse { get; set; }
		public float WinnerShare { get; set; }
		public int FedExPoints { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion

		#region Constructors

		public Tournament ()
		{
		}

		public Tournament (XElement t)
		{
			var id = t.Attributes ().Where (a => a.Name.LocalName == "id").FirstOrDefault ();
			var name = t.Attributes ().Where (a => a.Name.LocalName == "name").FirstOrDefault ();
			var purse = t.Attributes ().Where (a => a.Name.LocalName == "purse").FirstOrDefault ();
			var winnerShare = t.Attributes ().Where (a => a.Name.LocalName == "winning_share").FirstOrDefault ();
			var fedExPoints = t.Attributes ().Where (a => a.Name.LocalName == "points").FirstOrDefault ();
			var startDate = t.Attributes ().Where (a => a.Name.LocalName == "start_date").FirstOrDefault ();
			var endDate = t.Attributes ().Where (a => a.Name.LocalName == "end_date").FirstOrDefault ();

			Id = (id == null) ? string.Empty : id.Value;
			Name = (name == null) ? string.Empty : name.Value;
			Purse = (purse == null) ? 0 : float.Parse (purse.Value);
			WinnerShare = (winnerShare == null) ? 0 : float.Parse(winnerShare.Value);
			FedExPoints = (fedExPoints == null) ? 0 : int.Parse (fedExPoints.Value);
			StartDate = (startDate == null) ? DateTime.MinValue : DateTime.Parse (startDate.Value);
			EndDate = (endDate == null) ? DateTime.MaxValue : DateTime.Parse (endDate.Value);
		}

		#endregion
	}
}