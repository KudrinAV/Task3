﻿using Classes.BillingSystemObjects;
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

        private IPort _giveANotConnectedPort()
        {
            foreach(var item in Ports)
            {
                if (item.ContractStatus == StatusOfContract.NotContracted)
                {
                    return item;
                }
            }
            AddPort();
            return Ports.Last();
        }

        public IPort SignAContract(ITariffPlan tariffPlan)
        {
            var finding = from port in Ports
                          where port.ContractStatus == StatusOfContract.NotContracted
                          select port;
            foreach(var item in finding)
            {
                Abonents.Contracts.Add(new Contract(item , tariffPlan));
                Console.WriteLine("Контракт подписан");
                return item;
            }
            Abonents.Contracts.Add(new Contract(_giveANotConnectedPort(), tariffPlan));
            Console.WriteLine("Контракт подписан");
            return Ports.Last();
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

        private bool _isNumberExist(string number)
        {
            foreach(var item in Ports)
            {
                if (item.Number == number) return true;
            }
            return false;
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            int match = 0;
            if (_isNumberExist(e.ReceivingNumber))
            {
                match++;
                var finding = from port in Ports
                              where port.PortStatus == StatusOfPort.Connected && port.CallStatus == StatusOfCall.Avaliable && e.ReceivingNumber == port.Number
                              select port;
                foreach (var item in finding)
                {
                    match++;
                    item.GetAnswer(e);
                    if (e.AnswerStatus == StatusOfAnswer.Answer)
                    {
                        Console.WriteLine("Hello");
                        item.ChangeCallStatus(StatusOfCall.OnCall);
                        e.PortOfCaller.ChangeCallStatus(StatusOfCall.OnCall);
                        _onGoingCalls.Add(new CallInformation(e.PortOfCaller, item));
                    }
                    else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("Answer is NO"));
                }
                if (match == 1) e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("Номер занят"));
            }
            if(match==0) e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("There is no such a number"));
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
            Abonents = new BillingSystem();
            _onGoingCalls = new List<ICallInformation>();
            _finishedCalls = new List<ICallInformation>();
        }
    }
}
