using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;

using Gadgeteer.Networking;

namespace Exagreen.SolarPulse {
    using Gadgeteer.Modules.Velloso;

    using Microsoft.SPOT.Input;

    public partial class Program {
        private Gadgeteer.Timer lightTimer;
        private Gadgeteer.Timer wifiInitializationTimer;

        private Gadgeteer.Modules.Velloso.Bluetooth.Client bluetoothClient;

        private Window mainWindow;

        private Image splashScreenImage = new Image(new Bitmap(Resources.GetBytes(Resources.BinaryResources.solarpulse_splash), Bitmap.BitmapImageType.Jpeg));
        private Image mainScreenImage = new Image(new Bitmap(Resources.GetBytes(Resources.BinaryResources.solarpulse_main), Bitmap.BitmapImageType.Jpeg));
        private Image premaintenanceScreenImage = new Image(new Bitmap(Resources.GetBytes(Resources.BinaryResources.solarpulse_premaintenance), Bitmap.BitmapImageType.Jpeg));
        private Image maintenanceScreenImage = new Image(new Bitmap(Resources.GetBytes(Resources.BinaryResources.solarpulse_maintenance), Bitmap.BitmapImageType.Jpeg));

        private Bluetooth.BluetoothState previousBluetoothState;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted() {

            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/

            this.InitializeDisplay();
            this.ShowSplashScreen();

            this.wifiInitializationTimer = new Gadgeteer.Timer(1000, Gadgeteer.Timer.BehaviorType.RunOnce);
            wifiInitializationTimer.Tick += timer => {
                    this.InitializeWifi();
                };
            wifiInitializationTimer.Start();

            var bluetoothInitializationTimer = new Gadgeteer.Timer(1000, Gadgeteer.Timer.BehaviorType.RunOnce);
            bluetoothInitializationTimer.Tick += timer => {
                this.InitializeBluetooth();
            };
            bluetoothInitializationTimer.Start();

            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
        }

        private void InitializeWifi() {
            Debug.Print("SolarPulse - Initialize Wifi.");

            var connected = ConnectWifi();
            if (connected) {
                lightTimer = new Gadgeteer.Timer(1000, Gadgeteer.Timer.BehaviorType.RunOnce); // every 1 second (1000ms)
                lightTimer.Tick += timer_Tick;
                lightTimer.Start();

                this.ShowMainScreen();
            }
            else {
                this.wifiInitializationTimer.Start();
            }
        }

        private bool ConnectWifi() {
            Debug.Print("SolarPulse - Connect Wifi.");

            wifiRS21.UseDHCP();

            var info = wifiRS21.Interface.Scan("NOKIA Lumia 920");
            if (info != null) {
                wifiRS21.Interface.Join(info[0], "1234");
                Debug.Print("SolarPulse - Network joined.");

                // Code: http://msdn.microsoft.com/fr-fr/Library/jj677200.aspx

                return true;
            }
            Debug.Print("Network not joined");
            return false;
        }

        private void SendHttpData(int luminecence) {
            // MSDN: http://msdn.microsoft.com/fr-fr/Library/jj677200.aspx

            string json = @"
                            {
                                ""DeviceId"":""a2fbb122-0060-4f79-aafd-766c71febd97"",
                                ""Value"":" + luminecence + @"
                            }";

            POSTContent post = POSTContent.CreateTextBasedContent(json);

            var req = HttpHelper.CreateHttpPostRequest("http://exagreen.azure-mobile.net/tables/solarpulseentry", post, "application/json");

            req.AddHeaderField("X-ZUMO-APPLICATION", "MY_ZUMO");
            req.AddHeaderField("Content-Type", "application/json");

            req.ResponseReceived += req_ResponseReceived;


            req.SendRequest();
            
        }

        void req_ResponseReceived(HttpRequest sender, HttpResponse response) {

            Debug.Print("SolarPulse - Data sent");

            lightTimer.Start();
        }

        void timer_Tick(Gadgeteer.Timer timer) {
            double luminecence = lightSense.GetIlluminance();

            lightTimer.Stop();


            Debug.Print("SolarPulse - Light: " + luminecence);

            //lightTimer.Start();

            SendHttpData((int)luminecence);
        }

        private void InitializeDisplay() {
            Debug.Print("SolarPulse - Initialize display.");
            this.mainWindow = this.displayTE35.WPFWindow;
            this.mainWindow.TouchDown += OnDisplayTouched;
        }

        private void OnDisplayTouched(object sender, TouchEventArgs touchEventArgs) {
            Debug.Print("SolarPulse - Display touched.");
            Debug.Print("SolarPulse - Entering bluetooth pairing mode.");
            this.ShowPremaintenanceScreen();
            this.bluetoothClient.EnterPairingMode();
        }

        private void ShowSplashScreen() {
            Debug.Print("SolarPulse - Show splash screen.");

            this.displayTE35.SetBacklight(true);

            this.mainWindow.Child = this.splashScreenImage;

            /* Backlight off.
            var backlightOffTimer = new GT.Timer(5000, GT.Timer.BehaviorType.RunOnce);
            backlightOffTimer.Tick += timer => this.displayTE35.SetBacklight(false);
            backlightOffTimer.Start();
            */
        }

        private void ShowMainScreen() {
            Debug.Print("SolarPulse - Show main screen.");

            this.displayTE35.SetBacklight(true);

            this.mainWindow.Child = this.mainScreenImage;

            /* Backlight off.
            var backlightOffTimer = new GT.Timer(5000, GT.Timer.BehaviorType.RunOnce);
            backlightOffTimer.Tick += timer => this.displayTE35.SetBacklight(false);
            backlightOffTimer.Start();
            */
        }

        private void ShowPremaintenanceScreen() {
            Debug.Print("SolarPulse - Show premaintenance screen.");

            this.displayTE35.SetBacklight(true);

            this.mainWindow.Child = this.premaintenanceScreenImage;

            /* Backlight off.
            var backlightOffTimer = new GT.Timer(5000, GT.Timer.BehaviorType.RunOnce);
            backlightOffTimer.Tick += timer => this.displayTE35.SetBacklight(false);
            backlightOffTimer.Start();
            */
        }

        private void ShowMaintenanceScreen() {
            Debug.Print("SolarPulse - Show maintenance screen.");

            this.displayTE35.SetBacklight(true);

            this.mainWindow.Child = this.maintenanceScreenImage;

            /* Backlight off.
            var backlightOffTimer = new GT.Timer(5000, GT.Timer.BehaviorType.RunOnce);
            backlightOffTimer.Tick += timer => this.displayTE35.SetBacklight(false);
            backlightOffTimer.Start();
            */
        }

        private void InitializeBluetooth() {
            Debug.Print("SolarPulse - Initialize Bluetooth.");

            this.bluetooth.Reset();
            this.bluetooth.SetDeviceName("SolarPulse");
            this.bluetoothClient = this.bluetooth.ClientMode;
            this.bluetooth.BluetoothStateChanged += OnBluetoothStateChanged;
            this.bluetooth.DataReceived += OnBluetoothDataReceived;
        }

        private void OnBluetoothDataReceived(Bluetooth sender, string data)
        {
            Debug.Print("SolarPulse - Bluetooth data received - Data: \"" + data + "\".");
        }

        private void OnBluetoothStateChanged(Bluetooth sender, Bluetooth.BluetoothState btState)
        {
            Debug.Print("SolarPulse - Bluetooth state changed - State: \"" + btState + "\".");

            switch (btState) {
                case Bluetooth.BluetoothState.Connected:
                    this.ShowMaintenanceScreen();
                    break;
                default:
                    if (previousBluetoothState == Bluetooth.BluetoothState.Connected) {
                        this.ShowMainScreen();
                    }
                    break;
            }
            this.previousBluetoothState = btState;
        }
    }
}