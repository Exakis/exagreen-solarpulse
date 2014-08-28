using Cirrious.CrossCore;
using Microsoft.WindowsAzure.MobileServices;

namespace Exagreen.SolarPulse.Core
{
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.ViewModels;
    using Exagreen.SolarPulse.Core.ViewModels;

    /// <summary>
    /// Define the App type.
    /// </summary>
    public class App : MvxApplication
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton(typeof(IMobileServiceClient), ()=> new MobileServiceClient("https://exagreen.azure-mobile.net/", "MY_ZUMO"));


            //// Start the app with the First View Model.
            this.RegisterAppStart<MainViewModel>();
        }
    }
}