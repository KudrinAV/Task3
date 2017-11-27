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
                item.SignAContract(emulationEnvironment.Aps, emulationEnvironment.Cheap);
            }

            foreach (var item in emulationEnvironment.Users)
            {
                item.ConnectPortToTerminal();
            }

            Console.WriteLine("Call on unexisting number");
            emulationEnvironment.Users.ElementAt(0).Call("1231231");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Call somebody and caller finishes call");
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            Thread.Sleep(3000);
            emulationEnvironment.Users.ElementAt(0).EndCall();
            Console.WriteLine("Call somebody and caller reciever finishes call and somebody tries to call one of them");
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            emulationEnvironment.Users.ElementAt(2).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            Thread.Sleep(3000);
            emulationEnvironment.Users.ElementAt(1).EndCall();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Calling himself");
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(0).GetNumber());
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Getting balance");
            emulationEnvironment.Users.ElementAt(0).GetBalance();
            Console.WriteLine("\n");
            emulationEnvironment.Users.ElementAt(1).GetBalance();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Getting history");
            emulationEnvironment.Users.ElementAt(0).GetHistory();
            Console.WriteLine("\n");
            emulationEnvironment.Users.ElementAt(1).GetHistory();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Thread.Sleep(30000);
            Console.WriteLine("Сall with negative balance");
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Put money on balance, check balance, call somebody");
            emulationEnvironment.Users.ElementAt(0).PunOnBalance(5);
            emulationEnvironment.Users.ElementAt(0).GetBalance();
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            Thread.Sleep(3000);
            emulationEnvironment.Users.ElementAt(0).EndCall();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Changes tariff, call somebody, get history");
            emulationEnvironment.Users.ElementAt(0).ChangeTariff(emulationEnvironment.Expensive);
            emulationEnvironment.Users.ElementAt(0).Call(emulationEnvironment.Users.ElementAt(1).GetNumber());
            Thread.Sleep(3000);
            emulationEnvironment.Users.ElementAt(0).EndCall();
            Console.WriteLine("\n");
            emulationEnvironment.Users.ElementAt(0).GetHistory();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\n");
        }
    }
}
