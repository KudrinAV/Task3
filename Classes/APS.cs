using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class APS : IAPS
    {
        public List<IPort> Ports { get; private set; }

        //public StatusOfConnect TryToConnect(string number)
        //{

        //}

        public StatusOfConnect ConnectCall(IPort port, string number)
        {
            var finding = from port1 in Ports
                          where port1.CallStatus == StatusOfCall.Avaliable
                          select port1.Number;
            foreach (var item in finding)
            {
                if (number == item)
                {
                    Console.WriteLine("WeCanConnect");
                    return StatusOfConnect.Connect;
                }
            }
            Console.WriteLine("WeCannot");
            return StatusOfConnect.NotAvaliable;
        }



        public APS(List<IPort> ports)
        {
            Ports = ports;
            foreach (var item in ports)
            {
                item.GetAPS(this);
            }
        }
    }
}
