using Classes;
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
            Terminal id1 = new Terminal(1);
            Terminal id2 = new Terminal(2);

            IPort port1 = new Port("123", 1);
            IPort port2 = new Port("234" , 2);

            id1.ConnectToPort(port1);
            id2.ConnectToPort(port2);

            id2.SeeTheNumber();
            id1.SeeTheNumber();
        }
    }
}
