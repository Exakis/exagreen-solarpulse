using System.Collections.Generic;
using Android.Content;
using Android.Util;
using BarChart;

namespace Exagreen.SolarPulse.Droid.Controls
{
    public class SolarBarChartView : BarChartView
    {

        public SolarBarChartView(Context context)
            : base(context)
        {
        }

        public SolarBarChartView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public SolarBarChartView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        public IEnumerable<BarChart.BarModel> RefreshableItemsSource
        {
            get { return base.ItemsSource; }
            set
            {
                base.ItemsSource = value;
                this.Invalidate();
            }
        }
    }
}