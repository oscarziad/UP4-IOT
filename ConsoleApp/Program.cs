using System;
using Microsoft.Azure.Devices.Client;
using SharedLibraries.Models;
using SharedLibraries.Services;

namespace ConsoleApp
{
    class Program
    {
        //detta initierar DeviceClient
        private static readonly string _conn = "HostName=ec-win20-iothub-steven.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=LbS3gbEJz0lipgRWexRYLRdN0BH+RgIZ+D8R/oU6N6g=";
        
        private static readonly DeviceClient deviceClient =
            DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
        static void Main(string[] args)
        {

            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.RecieveMessageAsync(deviceClient).GetAwaiter();

            Console.ReadKey();
        }
      

        
    }
}
