using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Openfin.Desktop;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace OpenFinHelloDotNet
{
 
  class Program
  {
        static void Main(string[] args)
        {

            Console.WriteLine("Inter Application Bus Example");
           

            var runtimeOptions = new RuntimeOptions
            {
                Version = "9.61.36.36"
            };

            var runtime = Runtime.GetRuntimeInstance(runtimeOptions);

            runtime.Connect(() =>
            {

                Console.WriteLine("The runtime has now connected.");


                //Test Application that will recieve IAB Message

                var appOptions = new ApplicationOptions("Test-Application-Recieving-IAB", "application-uuid", "http://localhost:8080/index.html");

                appOptions.MainWindowOptions.AutoShow = true; 
                appOptions.MainWindowOptions.Frame = true;
                appOptions.MainWindowOptions.Resizable = true;
                appOptions.MainWindowOptions.Name = "Test-Application-Recieving-IAB";

                var application = runtime.CreateApplication(appOptions);


                application.Started += (s, e) =>
                {
                    Console.WriteLine("app started.");
                    Thread.Sleep(5000);

                    // Test message sent to topic
                    InterApplicationBus.Publish(runtime, "OpenFinTopic", "Incoming Message from .NET Hello");

                };


                application.run((a) =>
                {
                   
      
                },
                (n) =>
                {
                });
            });

            Console.Read();

  




        }

  }
}
