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








            //IAPS test = new APS();

            //ITariffPlan tariffPlan = new First();


            //ITerminal id1 = new Terminal(1);
            //ITerminal id2 = new Terminal(2);
            //ITerminal id3 = new Terminal(3);
            //ITerminal id4 = new Terminal(4);

            //id1.ConnectToPort(test.SignAContract(tariffPlan));
            //id2.ConnectToPort(test.SignAContract(tariffPlan));
            //id3.ConnectToPort(test.SignAContract(tariffPlan));
            //Console.WriteLine(id1.GetNumber());
            //Console.WriteLine(id2.GetNumber());
            //Console.WriteLine(id3.GetNumber());

            //id1.ChangeTariff(tariffPlan);


            //id1.Call(id2.GetNumber());
            //Thread.Sleep(3000);
            //id2.EndCall();
            //id1.Call(id3.GetNumber());
            //Thread.Sleep(3000);
            //id1.EndCall();
            //Console.WriteLine();
            //id1.GetHistory();

            //id1.GetBalance();

            //Console.WriteLine("Press Enter to close this window.");
            //Console.ReadLine();
        }
    }
}
