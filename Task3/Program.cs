using Classes;
using Classes.BillingSystemObjects;
using Classes.Environment;
using Classes.Ports;
using Classes.TariffPlans;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmulationEnvironment emulationEnvironment = new EmulationEnvironment();

            emulationEnvironment.CreateTerminals(5);
            emulationEnvironment.CreateUsers();

            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();

            emulationEnvironment.Aps.DeletePort(0);
            emulationEnvironment.Aps.DeletePort(1);
            emulationEnvironment.Aps.DeletePort(1);


            foreach (var item in emulationEnvironment.Users)
            {
                item.SignAContract(emulationEnvironment.Aps, emulationEnvironment.First);
            }

            foreach(var item in emulationEnvironment.Users)
            {
                item.ConnectPortToTerminal();
            }

            foreach(var item in emulationEnvironment.Users)
            {
                Console.WriteLine(item.GetNumber() + " " + item.GetPort().Id);
            }

          


        }
    }
}
