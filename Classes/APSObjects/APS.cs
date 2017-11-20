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


        public void AddPort()
        {
            Ports.Add(new Port(Ports.Count + 1, _generateNumber()));
            Ports.Last().Calling += HandleCallEvent;
            Ports.Last().EndingCall += HandleEndCallEvent;
        }

        public IPort SignAContract(ITariffPlan tariffPlan)
        {
            var item = Ports.Find(x => x.ContractStatus == StatusOfContract.NotContracted);
            if (item != null)
            {
                Abonents.Contracts.Add(new Contract(item.Id, tariffPlan));
                item.PuttingOnBalance += Abonents.FindContract(item.Id).HandleMoney;
                item.EndingCall += Abonents.FindContract(item.Id).HandleCostOfCall;
                item.ChangingTariff += Abonents.FindContract(item.Id).HandleChangeTariffEvent;
                item.GettingHistory += Abonents.HandleGetHistoryEvent;
                item.GettingHistory += HandleGetHistoryEvent;
                Abonents.FindContract(item.Id).CantChangeTariffEvent += HandleCantChangeEvent;
                item.ChangeStatusOfContract();
                Console.WriteLine("Контракт подписан");
                return item;
            }
            else {
                AddPort();
                Abonents.Contracts.Add(new Contract(Ports.Last().Id, tariffPlan));
                Ports.Last().ChangeStatusOfContract();
                Ports.Last().PuttingOnBalance += Abonents.FindContract(Ports.Last().Id).HandleMoney;
                Ports.Last().EndingCall += Abonents.FindContract(Ports.Last().Id).HandleCostOfCall;
                Ports.Last().GettingHistory += Abonents.HandleGetHistoryEvent;
                Ports.Last().GettingHistory += HandleGetHistoryEvent;
                Ports.Last().ChangingTariff += Abonents.FindContract(Ports.Last().Id).HandleChangeTariffEvent;
                Abonents.FindContract(Ports.Last().Id).CantChangeTariffEvent += HandleCantChangeEvent;
                Console.WriteLine("Контракт подписан");
                return Ports.Last();
            }
        }

        public void HandleCantChangeEvent(object o , ChangeTariffEventArgs e)
        {
            e.Port.APSMessageShow(new MessageFromAPSEventArgs("U can't change tariff atleast" + (30 - DateTime.Now.Subtract(e.TimeOfChanging).TotalDays)));
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(e.History));
        }

        private bool _checkNumber(string number)
        {
            var finding = from port in Ports
                          select port.Number;
            foreach (var item in finding)
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
            for (int i = 0; i < 7; i++)
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
            var item = _onGoingCalls.Find(x => e.InitiatorOfEnd == x.Caller || e.InitiatorOfEnd == x.Receiver);
            if (item != null)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                Abonents.FinishedCalls.Add(item);
                e.SetEndedCall(item);
                _onGoingCalls.Remove(item);
                item.Caller.ChangeCallStatus(StatusOfCall.Avaliable);
                item.Receiver.ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        private bool _isNumberExist(string number)
        {
            foreach (var item in Ports)
            {
                if (item.Number == number) return true;
            }
            return false;
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            if (_isNumberExist(e.ReceivingNumber))
            {
                var item = Ports.Find(x => x.PortStatus == StatusOfPort.Connected && x.CallStatus == StatusOfCall.Avaliable && e.ReceivingNumber == x.Number);
                if(item != null)
                {
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
                else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("Номер занят"));
            }
            else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("There is no such a number"));
        }

        public APS()
        {
            Ports = new List<IPort>();
            Abonents = new BillingSystem();
            _onGoingCalls = new List<ICallInformation>();
        }
    }
}
