using System;
using Android.App;
using Android.OS;
using Android.Views;
using GlassProgressBar;

namespace PGATourLeaderboard.Glass
{
	public class LoadingFragment : Fragment
	{
		#region Overrides

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			View loadingView = inflater.Inflate (Resource.Layout.loading, container);
			var loading = loadingView.FindViewById<SliderView> (Resource.Id.loading_slider);
			loading.StartIndeterminate ();

			return loadingView;
		}

		#endregion
	}
}