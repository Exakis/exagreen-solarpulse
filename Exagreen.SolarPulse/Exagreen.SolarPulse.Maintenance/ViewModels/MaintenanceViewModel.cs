using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Exagreen.SolarPulse.Maintenance.Core;
using Telerik.Windows.Controls;

namespace Exagreen.SolarPulse.Maintenance.ViewModels
{
    public class MaintenanceViewModel : ViewModelBase
    {
        private bool m_isLoading = false;
        private string m_name = string.Empty;
        private int m_power = 0;
        private double m_voltage = 0;
        private double m_current = 0;
        private double m_temperature = 0;


        public string Name
        {
            get { return m_name; }
            set { SetValue(value, ref m_name); }
        }

        public bool IsLoading
        {
            get { return m_isLoading; }
            set { SetValue(value, ref m_isLoading); }
        }
        public int Power
        {
            get { return m_power; }
            set { SetValue(value, ref m_power); }
        }
        public double Voltage
        {
            get { return m_voltage; }
            set { SetValue(value, ref m_voltage); }
        }
        public double Current
        {
            get { return m_current; }
            set { SetValue(value, ref m_current); }
        }

        public double Temperature
        {
            get { return m_temperature; }
            set { SetValue(value, ref m_temperature); }
        }

        private void SetValue<T>(T value, ref T oldValue, [CallerMemberName]string propertyName = "")
        {
            if (oldValue != null && oldValue.Equals(value))
                return;

            oldValue = value;
            base.OnPropertyChanged(propertyName);
        }

        public async void LoadData()
        {
            IsLoading = true;
           // await Task.Delay(1000);

            try
            {
                PulseConnection connection = new PulseConnection();
                await connection.Connect();

                await connection.Write(new byte[] { 1 });
            }
            catch (Exception)
            {
                MessageBox.Show("Ousp, bluetooth connection failed");
            }
          


            Name = "Panel 47854";
            Power = 345;
            Voltage = 57.3;
            Current = 6.02;
            Temperature = 60;

            IsLoading = false;
        }
    }
}
