using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Exagreen.SolarPulse.Maintenance.ViewModels;

namespace Exagreen.SolarPulse.Maintenance.Views
{
    public partial class MaintenanceView : PhoneApplicationPage
    {
        private MaintenanceViewModel m_viewModel;

        public MaintenanceView()
        {
            InitializeComponent();
            m_viewModel = new MaintenanceViewModel();
            this.DataContext = m_viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            m_viewModel.LoadData();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            m_viewModel.Dispose();
        }
    }
}