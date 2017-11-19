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

        private List<ICallInformation> _onGoingCalls { get; set; }
        private List<ICallInformation> _finishedCalls { get; set; }


        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            var finding = from call in _onGoingCalls
                          where e.InitiatorOfEnd == call.Caller || e.InitiatorOfEnd == call.Receiver
                          select call;
            foreach(var item in finding)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                item.Caller.ChangeCallStatus(StatusOfCall.Avaliable);
                item.Receiver.ChangeCallStatus(StatusOfCall.Avaliable);
                Console.WriteLine("Вызов закончился, время на звонок  " + item.GetDuretionOfCall());
            }
        }

        public void HandleCallEvent(object sender, CallEventArgs e)
        {
            var finding = from port in Ports
                          where port.PortStatus == StatusOfPort.Connected && port.CallStatus== StatusOfCall.Avaliable && e.PortOfCaller != port
                          select port;
            foreach (var item in finding)
            {
                if (e.ReceivingNumber == item.Number)
                {
                    item.GetAnswer(e);
                    if (e.AnswerStatus == StatusOfAnswer.Answer)
                    {
                        item.ChangeCallStatus(StatusOfCall.OnCall);
                        e.PortOfCaller.ChangeCallStatus(StatusOfCall.OnCall);
                        _onGoingCalls.Add(new CallInformation(e.PortOfCaller, item));
                    }
                    else Console.WriteLine("he doesn't want to hear you anymore");
                }
            }
            //Console.WriteLine("WeCannot");
        }



        public APS(List<IPort> ports)
        {
            Ports = ports;
            _onGoingCalls = new List<ICallInformation>();
            _finishedCalls = new List<ICallInformation>();
            foreach (var item in ports)
            {
                item.Calling += HandleCallEvent;
                item.EndingCall += HandleEndCallEvent;
            }
        }
    }
}
