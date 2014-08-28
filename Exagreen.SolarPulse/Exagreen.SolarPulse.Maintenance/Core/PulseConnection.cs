using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;

namespace Exagreen.SolarPulse.Maintenance.Core
{
    public class PulseConnection : IDisposable
    {
        private readonly StreamSocket _socket;
        private Stream _outStream;

        public async Task Write(byte[] direction)
        {
            if (_outStream == null)
                throw new Exception("Connect before");

            var b = System.Text.UTF8Encoding.UTF8.GetBytes("coucou");

            await _outStream.WriteAsync(b, 0, b.Length);
            await _outStream.FlushAsync();
        }

        public PulseConnection()
        {
            _socket = new StreamSocket();
        }

        public async Task Connect()
        {
            // Configure PeerFinder to search for all paired devices.
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            var pairedDevices = await PeerFinder.FindAllPeersAsync();

            if (pairedDevices.Count == 0)
            {
                throw new Exception("No paired devices were found.");
            }
            else
            {
                // Select a paired device. In this example, just pick the first one.
                PeerInformation selectedDevice = pairedDevices[0];
                // Attempt a connection

                try
                {
                    await _socket.ConnectAsync(selectedDevice.HostName, "1");
                    _outStream = _socket.OutputStream.AsStreamForWrite();
                }
                catch (Exception ex)
                {
                    if ((uint)ex.HResult == 0x8007048F)
                    {
                        throw new Exception("Bluetooth is turned off.");

                    }
                    if ((uint)ex.HResult == 0x8007271D)
                    {
                        //0x80070005 - previous error code that may be wrong?
                        throw new Exception("To run this app, you must have ID_CAP_PROXIMITY enabled in WMAppManifest.xaml");
                    }
                    else if ((uint)ex.HResult == 0x80072740)
                    {
                        throw new Exception("The Bluetooth port is already in use.");
                    }
                    else if ((uint)ex.HResult == 0x8007274C)
                    {
                        throw new Exception("Could not connect to the selected Bluetooth Device.\nPlease make sure it is switched on.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_outStream != null)
                _outStream.Dispose();
            if (_socket != null)
                _socket.Dispose();
        }
    }
}