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

        public StatusOfConnect TryToConnect(IPort port, string number)
        {
            if (port.CallStatus == StatusOfCall.Avaliable)
            {
                Console.WriteLine("hello");
                
                return port.GetAnswer(number) == StatusOfConnect.Connect ? StatusOfConnect.Connect : StatusOfConnect.Abort;
            }
            if (port.CallStatus == StatusOfCall.OnCall)
                return StatusOfConnect.OnCall;
            return StatusOfConnect.NotAvaliable;

               
        }

        public StatusOfConnect ConnectCall(IPort port, string number)
        {
            var finding = from port1 in Ports
                          where port1.PortStatus == StatusOfPort.Connected
                          select port1;
            foreach (var item in finding)
            {
                if (number == item.Number)
                {
                    Console.WriteLine("yes");
                    return TryToConnect(item, port.Number);
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
