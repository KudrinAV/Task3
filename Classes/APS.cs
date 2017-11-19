using Contracts.CustomArgs;
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


        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            Console.WriteLine("I'm working, just implement me");
        }

        public void HandleCallEvent(object sender, CallEventArgs e)
        {
            var finding = from port in Ports
                          //where port1.PortStatus == StatusOfPort.Connected
                          select port;
            foreach (var item in finding)
            {
                //Console.WriteLine(item.Number);
                if (e.ReceivingNumber == item.Number)
                {
                    item.GetAnswer(e);
                    if (e.AnswerStatus == StatusOfAnswer.Answer)
                    {
                        item.ChangeCallStatus(StatusOfCall.OnCall);
                        e.PortOfCaller.ChangeCallStatus(StatusOfCall.OnCall);
                        Console.WriteLine("Hello to you");
                    }
                    else Console.WriteLine("he doesn't want to hear you anymore");
                }
            }
            //Console.WriteLine("WeCannot");
        }



        public APS(List<IPort> ports)
        {
            Ports = ports;
            foreach (var item in ports)
            {
                item.Calling += HandleCallEvent;
                item.EndingCall += HandleEndCallEvent;
            }
        }
    }
}
