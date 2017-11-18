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
            TestEvents pub = new TestEvents();
            Subscriber sub1 = new Subscriber("sub1", pub);
            Subscriber sub2 = new Subscriber("sub2", pub);

            // Call the method that raises the event.
            pub.ChangeS();

            // Keep the console window open
            Console.WriteLine("Press Enter to close this window.");
            Console.ReadLine();
        }
    }
}
