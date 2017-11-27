using Classes.BillingSystemObjects;
using Classes.Ports;
using Contracts.CustomArgs;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class APS : IAPS
    {
        private List<IPort> _ports { get; set; }
        private IBillingSystem _abonents { get; set; }
        private List<ICallInformation> _onGoingCalls { get; set; }
        private int _daysInMonth = 30;

        private void _handleDailyCheckEvent(object o, MessageFromAPSEventArgs e)
        {
            foreach (var item in e.ListOfDebters)
            {
                var port = _ports.Find(x => x.Number == item);
                port.APSMessageShow(new MessageFromAPSEventArgs("You need to pay " + item));
                port.ChangeCallStatus(StatusOfCall.NegativeBalance);
            }
        }

        private void _handleDebtRepaidEvent(object o, BalanceEventArgs e)
        {
            _ports.Find(x => x.Id == e.IdOfPort).ChangeCallStatus(StatusOfCall.Avaliable);
        }

        private void _handleSendBalanceEvent(object o, BalanceEventArgs e)
        {
            var item = _ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(String.Format("Balance is {0:0.00} ", e.Money)));
        }

        private void _handleCantChangeEvent(object o, CantChangeTariffEventArgs e)
        {
            var item = _ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(String.Format("U can't change tariff atleast {0:0.00} days", _daysInMonth - DateTime.Now.Subtract(e.TimeOfChanging).TotalDays)));
        }

        private void _handleSendHistoryEvent(object o, SendHistoryEventArgs e)
        {
            var item = _ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(e.CallList));
        }

        private void _handleEndCallEvent(object o, EndCallEventArgs e)
        {
            var item = _onGoingCalls.Find(x => e.InitiatorOfEnd.Number == x.Caller || e.InitiatorOfEnd.Number == x.Receiver);
            if (item != null)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                _abonents.AddToHistory(item);
                _onGoingCalls.Remove(item);
                _ports.Find(x => x.Number == item.Caller).ChangeCallStatus(StatusOfCall.Avaliable);
                _ports.Find(x => x.Number == item.Receiver).ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        private void _handleConnectingEvent(object o, AnswerEventArgs e)
        {
            var caller = _ports.Find(x => x.Number == e.CallingNumber);
            var reciver = _ports.Find(x => x.Number == e.RecieverNumber);
            if (e.Answer == StatusOfAnswer.Answer)
            {
                reciver.ChangeCallStatus(StatusOfCall.OnCall);
                caller.ChangeCallStatus(StatusOfCall.OnCall);
                _onGoingCalls.Add(new CallInformation(caller.Number, e.RecieverNumber));
            }
            else caller.APSMessageShow(new MessageFromAPSEventArgs("Answer is NO"));
        }

        private void _handleCallEvent(object o, CallEventArgs e)
        {
            if (_isNumberExist(e.ReceivingNumber) && e.ReceivingNumber != e.PortOfCaller.Number)
            {
                var item = _ports.Find(x => x.PortStatus == StatusOfPort.Connected && x.CallStatus == StatusOfCall.Avaliable && e.ReceivingNumber == x.Number);
                if (item != null)
                {
                    _getAnswerFromPort(item, e);
                }
                else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("Nomer is busy"));
            }
            else e.PortOfCaller.APSMessageShow(new MessageFromAPSEventArgs("There is no such a number"));
        }

        public void AddPort()
        {
            if (_ports.Count == 0)
            {
                _ports.Add(new Port(1, _generateNumber()));
                _ports.Last().Calling += _handleCallEvent;
                _ports.Last().EndingCall += _handleEndCallEvent;
                _ports.Last().Connecting += _handleConnectingEvent;
            }
            else
            {
                _ports.Add(new Port(_ports.Last().Id + 1, _generateNumber()));
                _ports.Last().Calling += _handleCallEvent;
                _ports.Last().EndingCall += _handleEndCallEvent;
                _ports.Last().Connecting += _handleConnectingEvent;
            }
        }

        public void DeletePort(int indexOfPort)
        {
            if (indexOfPort != 0)
            {
                if (indexOfPort <= _ports.Count)
                {
                    _ports.ElementAt(indexOfPort - 1).Calling -= _handleCallEvent;
                    _ports.ElementAt(indexOfPort - 1).EndingCall -= _handleEndCallEvent;
                    _ports.ElementAt(indexOfPort - 1).Connecting -= _handleConnectingEvent;
                    _ports.Remove(_ports.ElementAt(indexOfPort - 1));
                }
            }
        }

        public IPort SignAContract(ITariffPlan tariffPlan, string name)
        {
            var item = _ports.Find(x => x.ContractStatus == StatusOfContract.NotContracted);
            if (item != null)
            {
                _abonents.Contracts.Add(new Contract(_abonents.Contracts.Count, item.Id, item.Number, name, tariffPlan));
                item.PuttingOnBalance += _abonents.FindContract(item.Id).HandleMoneyEvent;
                item.ChangingTariff += _abonents.FindContract(item.Id).HandleChangeTariffEvent;
                item.GettingHistory += _abonents.HandleGetHistoryForMonthEvent;
                item.GettingBalance += _abonents.HandleGetBalanceEvent;
                _abonents.FindContract(item.Id).CantChangeTariffEvent += _handleCantChangeEvent;
                _abonents.FindContract(item.Id).DebtRepaidEvent += _handleDebtRepaidEvent;
                _abonents.FindContract(item.Id).SendHistoryEvent += _handleSendHistoryEvent;
                _abonents.FindContract(item.Id).SendBalanceEvent += _handleSendBalanceEvent;
                item.ChangeStatusOfContract();
                return item;
            }
            else
            {
                AddPort();
                _abonents.Contracts.Add(new Contract(_abonents.Contracts.Count, _ports.Last().Id, _ports.Last().Number, name, tariffPlan));
                _ports.Last().ChangeStatusOfContract();
                _ports.Last().PuttingOnBalance += _abonents.FindContract(_ports.Last().Id).HandleMoneyEvent;
                _ports.Last().GettingHistory += _abonents.HandleGetHistoryForMonthEvent;
                _ports.Last().GettingBalance += _abonents.HandleGetBalanceEvent;
                _ports.Last().ChangingTariff += _abonents.FindContract(_ports.Last().Id).HandleChangeTariffEvent;
                _abonents.FindContract(_ports.Last().Id).CantChangeTariffEvent += _handleCantChangeEvent;
                _abonents.FindContract(_ports.Last().Id).DebtRepaidEvent += _handleDebtRepaidEvent;
                _abonents.FindContract(_ports.Last().Id).SendHistoryEvent += _handleSendHistoryEvent;
                _abonents.FindContract(_ports.Last().Id).SendBalanceEvent += _handleSendBalanceEvent;
                return _ports.Last();
            }
        }

        public void TerminateContract(IPort port)
        {
            var item = _abonents.FindContract(port.Id);
            if (item != null && port.PortStatus == StatusOfPort.NotConnected)
            {
                port.PuttingOnBalance -= _abonents.FindContract(port.Id).HandleMoneyEvent;
                port.ChangingTariff -= _abonents.FindContract(port.Id).HandleChangeTariffEvent;
                port.GettingHistory -= _abonents.HandleGetHistoryForMonthEvent;
                port.GettingBalance -= _abonents.HandleGetBalanceEvent;
                _abonents.FindContract(port.Id).CantChangeTariffEvent -= _handleCantChangeEvent;
                _abonents.FindContract(port.Id).DebtRepaidEvent -= _handleDebtRepaidEvent;
                _abonents.FindContract(port.Id).SendHistoryEvent -= _handleSendHistoryEvent;
                _abonents.FindContract(port.Id).SendBalanceEvent -= _handleSendBalanceEvent;
                _abonents.TerminateContract(item);
                port.ChangeStatusOfContract();
            }
        }

        private bool _checkNumber(string number)
        {
            var finding = from port in _ports
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

        private bool _isNumberExist(string number)
        {
            foreach (var item in _ports)
            {
                if (item.Number == number) return true;
            }
            return false;
        }

        private void _getAnswerFromPort(IPort port, CallEventArgs e)
        {
            port.GetAnswer(e);
        }
        public APS()
        {
            _ports = new List<IPort>();
            _abonents = new BillingSystem();
            _abonents.DailyCheckEvent += _handleDailyCheckEvent;
            _onGoingCalls = new List<ICallInformation>();
        }
    }
}
