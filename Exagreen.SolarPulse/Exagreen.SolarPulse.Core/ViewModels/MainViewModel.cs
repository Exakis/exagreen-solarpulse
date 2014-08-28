using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Cirrious.MvvmCross.FieldBinding;
using Exagreen.SolarPulse.Core.Helpers;
using Exagreen.SolarPulse.Core.Services;

namespace Exagreen.SolarPulse.Core.ViewModels
{
    /// <summary>
    /// Define the FirstViewModel type.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly IEntryService _entryService;
        private Timer _timer;

        public MainViewModel(IEntryService entryService)
        {
            _entryService = entryService;

        }

        async void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            try
            {
                // ca plante defois a cause d'un dictionnaire....
                var entry = await _entryService.GetLastEntryAsync();

                InvokeOnMainThread(() => LiveProduction.Value = new List<double> { entry.Value });
            }
            catch (Exception ex)
            {


            }
            Debug.WriteLine("Tick " + DateTime.Now);
            _timer.Start();
        }

        public override async void Start()
        {
            _timer = new Timer();
            _timer.Tick += _timer_Tick;


            LiveProduction.Value = new List<double> { 0 };
            //_timer = new Timer(async (o) => 
            //                                        {

            //                                            try
            //                                            {
            //                                                // ca plante defois a cause d'un dictionnaire....
            //                                                var entry = await _entryService.GetLastEntryAsync();

            //                                                InvokeOnMainThread(() => LiveProduction.Value = new List<double> { entry.Value });
            //                                            }
            //                                            catch (Exception)
            //                                            {


            //                                            }


            //                                        }, null, 3000);



            base.Start();
        }

        public readonly INC<List<double>> LiveProduction = new NC<List<double>>();
    }
}
