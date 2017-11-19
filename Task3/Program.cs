using Classes;
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
            IPort port1 = new Port("1234");
            IPort port2 = new Port("1253");
            IPort port3 = new Port("1263");
            // Call the method that raises the event.
            ITerminal id1 = new Terminal(1);
            ITerminal id2 = new Terminal(2);
            ITerminal id3 = new Terminal(3);
            List<IPort> list = new List<IPort> { port1, port2, port3 };
            IAPS test = new APS(list);

            id1.ConnectToPort(port1);
            id2.ConnectToPort(port2);
            id3.ConnectToPort(port3);

            id2.DissconnectFromPort();
            
            id2.Call("1234");
            //Thread.Sleep(5000);
            id3.Call("1234");
            //Console.WriteLine(port1.CallStatus + " " + port2.CallStatus + " " + port3.CallStatus);
            //Thread.Sleep(5000);
            id1.EndCall();
            Console.WriteLine(port1.CallStatus + " " + port2.CallStatus + " " + port3.CallStatus);

            // Keep the console window open
            Console.WriteLine("Press Enter to close this window.");
            Console.ReadLine();
        }
    }
}
