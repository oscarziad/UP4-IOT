using Microsoft.Azure.Devices.Client;
using SharedLibariesUWP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPApplication.Helpers
{
    class AzureHelper
    {
        private static readonly string _conn = "HostName=ec-win20-iothub-oscar.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=PhHHOn69OIQmjjmUQJyyiQEfq1teMYQBAQRBUODJZos=";

        private static readonly DeviceClient deviceClient =
        DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
        public static void SendReceiveMsgAsync()
        {

            DeviceServiceUWP.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceServiceUWP.RecieveMessageAsync(deviceClient).GetAwaiter();

        }
    }
}
