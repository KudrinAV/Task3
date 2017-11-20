﻿using Classes;
using Classes.BillingSystemObjects;
using Classes.Ports;
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
            IAPS test = new APS();

            ITariffPlan tariffPlan = new LoneDiggerTariffPlan();


            ITerminal id1 = new Terminal(1);
            ITerminal id2 = new Terminal(2);
            ITerminal id3 = new Terminal(3);

            id1.ConnectToPort(test.SignAContract(tariffPlan));
            id2.ConnectToPort(test.SignAContract(tariffPlan));
            id3.ConnectToPort(test.SignAContract(tariffPlan));
            Console.WriteLine(id1.GetNumber());
            id1.ConnectToPort(test.SignAContract(tariffPlan));
            Console.WriteLine(id1.GetNumber());
            Console.WriteLine(id2.GetNumber());
            Console.WriteLine(id3.GetNumber());
            id1.PutMoney(123.1);



            id1.GetHistory(id1.Id);
            id2.GetHistory(id2.Id);
            Thread.Sleep(3000);
            //id1.ChangeTariff(tariffPlan);
            id1.Call(id2.GetNumber());
            id2.EndCall();

            id1.GetHistory(id1.Id);
            //foreach(var item in test.Ports)
            //{
            //    Console.WriteLine(item.Id + " " + item.PortStatus);
            //}

            ////id2.Call(id3.GetNumber());
            ////Thread.Sleep(3000);
            //id1.Call(id3.GetNumber());
            ////Console.WriteLine("\n");
            ////Console.WriteLine(id1.Port.CallStatus + " " + id2.Port.CallStatus + " " + id3.Port.CallStatus);
            //Thread.Sleep(5000);

            //id3.EndCall();
            //Console.WriteLine("\n");
            //Console.WriteLine(id1.Port.CallStatus + " " + id2.Port.CallStatus + " " + id3.Port.CallStatus);
            //id3.Call("dasdasdas");
            //id3.Call(id1.GetNumber());

            //Console.WriteLine("\n");
            //Console.WriteLine(id1.Port.CallStatus + " " + id2.Port.CallStatus + " " + id3.Port.CallStatus);

            //id1.EndCall();
            //Console.WriteLine("\n");
            Console.WriteLine(id1.Port.CallStatus + " " + id1.Port.CallStatus + " " + id1.Port.CallStatus);


















            //IAPS test = new APS();
            //test.AddPort();
            //test.AddPort();

            //foreach(var item in test.Ports)
            //{
            //    Console.WriteLine(item.Number);
            //}

            //ITerminal id1 = new Terminal(1);
            //ITerminal id2 = new Terminal(2);
            //ITerminal id3 = new Terminal(3);

            //id1.ConnectToPort(test.GiveANotConnectedPort());
            //id2.ConnectToPort(test.GiveANotConnectedPort());
            //id3.ConnectToPort(test.GiveANotConnectedPort());

            //foreach(var item in test.Ports)
            //{
            //    Console.WriteLine(item.Id + " " + item.Number);
            //}

            //Thread.Sleep(5000);
            //id1.Call(id2.GetNumber());
            ////Console.WriteLine(id1.Port.)
            //Thread.Sleep(5000);
            //id1.EndCall();
            //id2.Call("sadasd");


            // Keep the console window open
            Console.WriteLine("Press Enter to close this window.");
            Console.ReadLine();
        }
    }
}
