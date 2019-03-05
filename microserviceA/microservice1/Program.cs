using Microsoft.Owin.Hosting;
using System;

namespace microservice1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string servicePort = "9000";
            string baseAddress = $"http://localhost:{servicePort}/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Service started on port {servicePort}");
                Console.Read();
            }
        }
    }
}
