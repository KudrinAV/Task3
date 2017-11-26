using Classes.BillingSystemObjects;
using Classes.Ports;
using Contracts.CustomArgs;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Classes
{
    public class APS : IAPS
    {
        public List<IPort> Ports { get; private set; }
        public IBillingSystem Abonents { get; private set; }
        private List<ICallInformation> _onGoingCalls { get; set; }
        private int _daysInMonth = 30;

        private void _handleDailyCheckEvent(object o, MessageFromAPSEventArgs e)
        {
            foreach(var item in e.ListOfDebters)
            {
                var port = Ports.Find(x => x.Number == item);
                port.APSMessageShow(new MessageFromAPSEventArgs("You need to pay " + item));
                port.ChangeCallStatus(StatusOfCall.NegativeBalance);
            }
        }

        private void _handleDebtRepaidEvent(object o, BalanceEventArgs e)
        {
            Ports.Find(x => x.Id == e.IdOfPort).ChangeCallStatus(StatusOfCall.Avaliable);
        }

        private void _handleSendBalanceEvent(object o, BalanceEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(String.Format("Balance is {0:0.00} ", e.Money)));
        }

        private void _handleCantChangeEvent(object o, CantChangeTariffEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(String.Format("U can't change tariff atleast {0:0.00} days", _daysInMonth - DateTime.Now.Subtract(e.TimeOfChanging).TotalDays)));
        }

        private void _handleSendHistoryEvent(object o, SendHistoryEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(e.CallList));
        }

        private void _handleEndCallEvent(object o, EndCallEventArgs e)
        {
            var item = _onGoingCalls.Find(x => e.InitiatorOfEnd.Number == x.Caller || e.InitiatorOfEnd.Number == x.Receiver);
            if (item != null)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                Abonents.AddToHistory(item);
                _onGoingCalls.Remove(item);
                Ports.Find(x => x.Number == item.Caller).ChangeCallStatus(StatusOfCall.Avaliable);
                Ports.Find(x => x.Number == item.Receiver).ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        private void _handleConnectingEvent(object o, AnswerEventArgs e)
        {
            var caller = Ports.Find(x => x.Number == e.CallingNumber);
            var reciver = Ports.Find(x => x.Number == e.RecieverNumber);
            if (e.Answer == StatusOfAnswer.Answer)
            {
                Console.WriteLine("Hello");
                reciver.ChangeCallStatus(StatusOfCall.OnCall);
                caller.ChangeCallStatus(StatusOfCall.OnCall);
                _onGoingCalls.Add(new CallInformation(caller.Number, e.RecieverNumber));
            }
            else caller.APSMessageShow(new MessageFromAPSEventArgs("Answer is NO"));
        }

        private void _handleCallEvent(object o, CallEventArgs e)
        {
            if (_isNumberExist(e.ReceivingNumber))
            {
                var item = Ports.Find(x => x.PortStatus == StatusOfPort.Connected && x.CallStatus == StatusOfCall.Avaliable && e.ReceivingNumber == x.Number);
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
            Ports.Add(new Port(Ports.Count + 1, _generateNumber()));
            Ports.Last().Calling += _handleCallEvent;
            Ports.Last().EndingCall += _handleEndCallEvent;
            Ports.Last().Connecting += _handleConnectingEvent;
        }

        public IPort SignAContract(ITariffPlan tariffPlan , string name)
        {
            var item = Ports.Find(x => x.ContractStatus == StatusOfContract.NotContracted);
            if (item != null)
            {
                Abonents.Contracts.Add(new Contract(Abonents.Contracts.Count, item.Id, item.Number, name, tariffPlan));
                item.PuttingOnBalance += Abonents.FindContract(item.Id).HandleMoneyEvent;
                item.ChangingTariff += Abonents.FindContract(item.Id).HandleChangeTariffEvent;
                item.GettingHistory += Abonents.HandleGetHistoryForMonthEvent;
                item.GettingBalance += Abonents.HandleGetBalanceEvent;
                Abonents.FindContract(item.Id).CantChangeTariffEvent += _handleCantChangeEvent;
                Abonents.FindContract(item.Id).DebtRepaidEvent += _handleDebtRepaidEvent;
                Abonents.FindContract(item.Id).SendHistoryEvent += _handleSendHistoryEvent;
                Abonents.FindContract(item.Id).SendBalanceEvent += _handleSendBalanceEvent;
                item.ChangeStatusOfContract();
                return item;
            }
            else
            {
                AddPort();
                Abonents.Contracts.Add(new Contract(Abonents.Contracts.Count, Ports.Last().Id, Ports.Last().Number, name, tariffPlan));
                Ports.Last().ChangeStatusOfContract();
                Ports.Last().PuttingOnBalance += Abonents.FindContract(Ports.Last().Id).HandleMoneyEvent;
                Ports.Last().GettingHistory += Abonents.HandleGetHistoryForMonthEvent;
                Ports.Last().GettingBalance += Abonents.HandleGetBalanceEvent;
                Ports.Last().ChangingTariff += Abonents.FindContract(Ports.Last().Id).HandleChangeTariffEvent;
                Abonents.FindContract(Ports.Last().Id).CantChangeTariffEvent += _handleCantChangeEvent;
                Abonents.FindContract(Ports.Last().Id).DebtRepaidEvent += _handleDebtRepaidEvent;
                Abonents.FindContract(Ports.Last().Id).SendHistoryEvent += _handleSendHistoryEvent;
                Abonents.FindContract(Ports.Last().Id).SendBalanceEvent += _handleSendBalanceEvent;
                return Ports.Last();
            }
        }

        public void TerminateContract(IPort port)
        {
            var item = Abonents.FindContract(port.Id);
            if (item != null && port.PortStatus == StatusOfPort.NotConnected)
            {
                port.PuttingOnBalance -= Abonents.FindContract(port.Id).HandleMoneyEvent;
                port.ChangingTariff -= Abonents.FindContract(port.Id).HandleChangeTariffEvent;
                port.GettingHistory -= Abonents.HandleGetHistoryForMonthEvent;
                port.GettingBalance -= Abonents.HandleGetBalanceEvent;
                Abonents.FindContract(port.Id).CantChangeTariffEvent -= _handleCantChangeEvent;
                Abonents.FindContract(port.Id).DebtRepaidEvent -= _handleDebtRepaidEvent;
                Abonents.FindContract(port.Id).SendHistoryEvent -= _handleSendHistoryEvent;
                Abonents.FindContract(port.Id).SendBalanceEvent -= _handleSendBalanceEvent;
                Abonents.TerminateContract(item);
                port.ChangeStatusOfContract();
            }
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

        private bool _isNumberExist(string number)
        {
            foreach (var item in Ports)
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
            Ports = new List<IPort>();
            Abonents = new BillingSystem();
            Abonents.DailyCheckEvent += _handleDailyCheckEvent;
            _onGoingCalls = new List<ICallInformation>();
        }
    }
}
