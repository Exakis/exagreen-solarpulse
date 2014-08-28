using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BarChart;
using Cirrious.CrossCore.Converters;

namespace Exagreen.SolarPulse.Droid.Converters
{
    public class BarValueConverter : MvxValueConverter<List<double>, BarModel[]>
    {
        protected override BarModel[] Convert(List<double> value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = value.Select(s => new BarModel {Value = (float)s}).ToArray();
            return res;
        }
    }
}