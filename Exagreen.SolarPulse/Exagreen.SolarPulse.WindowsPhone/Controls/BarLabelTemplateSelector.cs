using System.Collections.ObjectModel;
using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;

namespace Exagreen.SolarPulse.WindowsPhone.Controls
{
    public class BarLabelTemplateSelector : DataTemplateSelector
    {
        private ObservableCollection<DataTemplate> templates;

        public BarLabelTemplateSelector()
        {
            this.templates = new ObservableCollection<DataTemplate>();
        }

        public ObservableCollection<DataTemplate> Templates
        {
            get { return this.templates; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (this.templates.Count == 0)
            {
                return null;
            }

            return this.templates[(item as AxisLabelModel).CollectionIndex%this.templates.Count];
        }
    }
}