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
            ITariffPlan First = new First();

            IEmulationEnvironment emulationEnvironment = new EmulationEnvironment();

            emulationEnvironment.CreateTerminals(5);
            emulationEnvironment.CreateUsers();

            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();
            emulationEnvironment.Aps.AddPort();
            
            
            foreach(var item in emulationEnvironment.Users)
            {
                item.Telephone.ConnectToPort(emulationEnvironment.Aps.SignAContract(First, item.Name));
            }

            foreach(var item in emulationEnvironment.Users)
            {
                Console.WriteLine(item.Telephone.GetNumber() + " " + item.Telephone.Id);
            }

            emulationEnvironment.Users.ElementAt(0).Telephone.Call(emulationEnvironment.Users.ElementAt(2).Telephone.GetNumber());
            Thread.Sleep(3000);
            emulationEnvironment.Users.ElementAt(0).Telephone.EndCall();
            emulationEnvironment.Users.ElementAt(0).Telephone.GetBalance();
            emulationEnvironment.Users.ElementAt(0).Telephone.GetHistory();
            emulationEnvironment.Users.ElementAt(0).Telephone.ChangeTariff(First);
            emulationEnvironment.Users.ElementAt(0).Telephone.PutMoney(30);
            emulationEnvironment.Users.ElementAt(0).Telephone.GetBalance();
            emulationEnvironment.Users.ElementAt(0).Telephone.DissconnectFromPort();
            emulationEnvironment.Aps.TerminateContract(emulationEnvironment.Aps.Ports.ElementAt(0));
            emulationEnvironment.Users.ElementAt(0).Telephone.GetNumber();
            emulationEnvironment.Users.ElementAt(0).Telephone.GetPort();
            emulationEnvironment.Users.ElementAt(0).Telephone.ConnectToPort(emulationEnvironment.Aps.SignAContract(First, emulationEnvironment.Users.ElementAt(0).Name));

            Console.WriteLine("kek");
            Console.WriteLine(emulationEnvironment.Users.ElementAt(0).Telephone.GetNumber());
            emulationEnvironment.Users.ElementAt(0).Telephone.GetBalance();
            emulationEnvironment.Users.ElementAt(0).Telephone.GetHistory();


        }
    }
}
