using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.BindingEx.WindowsShared;

namespace Exagreen.SolarPulse.WindowsPhone
{
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.WindowsPhone.Platform;
    using Microsoft.Phone.Controls;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxPhoneSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of IMvxApplication.</returns>
        protected override IMvxApplication CreateApp()
        {
            var x = new Core.App();

            //FastApplicationSwitchingService.Initialize();

            return x;
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var builder = new MvxWindowsBindingBuilder();
            builder.DoRegistration();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new MvxDebugTrace();
        }
    }
}