using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PGATourLeaderboard.Glass
{
	public class BaseActivity : Activity
	{
		#region Overrides

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.main_layout);
		}

		#endregion

		#region Public Methods

		public void ShowLoadingIndicator ()
		{
			TextView title = this.FindViewById<TextView> (Resource.Id.title);
			title.Text = "Loading...";

			TextView footer = this.FindViewById<TextView> (Resource.Id.footer);
			footer.Text = string.Empty;

			var loadingView = this.FindViewById<GlassProgressBar.SliderView> (Resource.Id.loading_slider);
			loadingView.Visibility = ViewStates.Visible;
			loadingView.StartIndeterminate ();
		}

		public void HideLoadingIndicator ()
		{
			TextView title = this.FindViewById<TextView> (Resource.Id.title);
			title.Text = string.Empty;

			TextView footer = this.FindViewById<TextView> (Resource.Id.footer);
			footer.Text = string.Empty;

			var loadingView = this.FindViewById<GlassProgressBar.SliderView> (Resource.Id.loading_slider);
			loadingView.Visibility = ViewStates.Gone;
			loadingView.StopIndeterminate ();
		}

		#endregion
	}
}