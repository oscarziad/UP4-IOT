using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using SharedLibariesUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace SharedLibariesUWP.Service
{
    public static class DeviceServiceUWP
    {
        //randomfunktionen
        private static readonly Random rnd = new Random();
        //denna skickar funktion är till för att skicka -> vi kallar på den i main
        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {

            while (true)
            {
                var data = new TemperatureModel
                {

                    Temperature = rnd.Next(20, 30),
                    Humidity = rnd.Next(40, 50),
                };

                // JSON = {"temperature": 20, "humidity": 44}
                var json = JsonConvert.SerializeObject(data);

                //meddelandet vi vill skicka kallas payload där vi formatterar meddelandet i azure devices client med en encoding.utf8
                var payload = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(json));
                await deviceClient.SendEventAsync(payload);


                Console.WriteLine($"Message sent: {json}");
                await Task.Delay(60 * 1000);
            }
        }
        public static async Task RecieveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;
                //hämtar 1 o 0 från byte o formaterar till string som vi skriver ut i konsollen.
                Console.WriteLine($"Message Recieved: { Encoding.UTF8.GetString(payload.GetBytes()) }");

                await deviceClient.CompleteAsync(payload);
            }

        }
        //Detta är en service-client = IoT Hub
        //service = något som utför något. ex en telefon
        //device något som hämtar information ex en bil
        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new Microsoft.Azure.Devices.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);


        }

    }
}
