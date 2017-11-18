using Classes;
using Classes.Learning_Events;
using Classes.Ports;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            IPort port1 = new Port("1234");
            IPort port2 = new Port("1253");
            // Call the method that raises the event.
            ITerminal id1 = new Terminal();
            ITerminal id2 = new Terminal();
            List<IPort> list = new List<IPort> { port1, port2 };
            IAPS test = new APS(list);

            id1.ConnectToPort(port1);
            id2.ConnectToPort(port2);

            id2.Call("1234");
            id1.EndCall();

            // Keep the console window open
            Console.WriteLine("Press Enter to close this window.");
            Console.ReadLine();
        }
    }
}
