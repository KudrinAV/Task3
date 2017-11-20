using Classes.BillingSystemObjects;
using Classes.Ports;
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
        public BillingSystem Abonents { get; private set; }
        private List<ICallInformation> _onGoingCalls { get; set; }
        private List<ICallInformation> _finishedCalls { get; set; }

        public void AddPort()
        {
            Ports.Add(new Port(Ports.Count + 1, _generateNumber()));
            Ports.Last().Calling += HandleCallEvent;
            Ports.Last().EndingCall += HandleEndCallEvent;
        }

        public IPort GiveANotConnectedPort()
        {
            int i = 0;
            foreach(var item in Ports)
            {
                i++;
                if (item.ContractStatus == StatusOfContract.NotContracted)
                {
                    item.ChangeStatusOfContract();
                    return item;
                }
            }
            AddPort();
            return Ports.Last();

        }

        public void SignAContract(ITariffPlan tariffPlan)
        {
            int match = 0;
            var finding = from port in Ports
                          where port.ContractStatus == StatusOfContract.NotContracted
                          select port;
            foreach(var item in finding)
            {
                match++;
                Abonents.Contracts.Add(new Contract(item.Id , tariffPlan));
                break;
            }
            if(match == 0)
            {
                Ports.Add(new Port(Ports.Count + 1, _generateNumber()));
                Ports.Last().Calling += HandleCallEvent;
                Ports.Last().EndingCall += HandleEndCallEvent;
                Abonents.Contracts.Add(new Contract(Ports.Last().Id, tariffPlan));
            }
        }

        private bool _checkNumber(string number)
        {
            var finding = from port in Ports
                          select port.Number;
            foreach(var item in finding)
            {
                if (item == number)
                    return false;
            }
            return true;
        }

        private string _randomGenerator()
        {
            string number = null;
            Random rnd = new Random();
            for(int i=0; i<7; i++)
            {
                if (i == 0)
                {
                    number += rnd.Next(1, 9).ToString();
                }
                else number += rnd.Next(0, 9).ToString();
            }
            return number;
        }

        private string _generateNumber()
        {
            string number;
            do
            {
                number = _randomGenerator();
            } while (!_checkNumber(number));

            return number;
        }

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            var finding = from call in _onGoingCalls
                          where e.InitiatorOfEnd == call.Caller || e.InitiatorOfEnd == call.Receiver
                          select call;
            foreach (var item in finding)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                item.Caller.ChangeCallStatus(StatusOfCall.Avaliable);
                item.Receiver.ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            int Match = 0;
            var finding = from port in Ports
                          where port.PortStatus == StatusOfPort.Connected && port.CallStatus == StatusOfCall.Avaliable && port.Number == e.ReceivingNumber
                          select port;
            foreach (var item in finding)
            {
                Match++;
                item.GetAnswer(e);
                if (e.AnswerStatus == StatusOfAnswer.Answer)
                {
                    item.ChangeCallStatus(StatusOfCall.OnCall);
                    e.PortOfCaller.ChangeCallStatus(StatusOfCall.OnCall);
                    _onGoingCalls.Add(new CallInformation(e.PortOfCaller, item));
                }
                else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("Answer is NO"));
            }
            if(Match == 0) e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("There is no such a number"));
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

        public APS()
        {
            Ports = new List<IPort>();
            _onGoingCalls = new List<ICallInformation>();
            _finishedCalls = new List<ICallInformation>();
        }
    }
}
