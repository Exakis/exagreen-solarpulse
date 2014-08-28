using System;
using BarChart;
using Exagreen.SolarPulse.Droid.Controls;

namespace Exagreen.SolarPulse.Droid.Views
{
    using Android.App;
    using Android.OS;

    /// <summary>
    /// Defines the FirstView type.
    /// </summary>
    [Activity(Label = "View for FirstView", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class MainView : BaseView
    {
        System.Threading.Timer _timer;

        public MainView()
        {

        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.MainView);

            var chart = FindViewById<SolarBarChartView>(Resource.Id.barChart);

            //var data = new[] { 1f, 2f, 4f, 8f, 16f, 32f };


            //_timer = new System.Threading.Timer((o) =>
            //                                    {
            //                                        data[0] = data[0] + 1;
            //                                        RunOnUiThread(() =>
            //                                                      {


            //                                                          chart.ItemsSource = Array.ConvertAll(data, v => new BarModel { Value = v });
            //                                                          chart.Invalidate();
            //                                                      });

            //                                    }, null, 0, 4000);

            //var chart = new BarChartView(this)
            //{
            //    ItemsSource = Array.ConvertAll(data, v => new BarModel { Value = v })
            //};


        }
    }
}