using System.Collections.Generic;
using System.Reflection;
using BarChart;
using Cirrious.CrossCore.Converters;
using Exagreen.SolarPulse.Droid.Converters;

namespace Exagreen.SolarPulse.Droid
{
    using Android.Content;

    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxAndroidSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of IMvxApplication.</returns>
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            registry.AddOrOverwrite("Bar", new BarValueConverter());
        }

        protected override IList<Assembly> AndroidViewAssemblies
        {
            get
            {
                var toReturn = base.AndroidViewAssemblies;
                toReturn.Add(typeof(BarChartView).Assembly);
                return toReturn;
            }
        }
    }
}