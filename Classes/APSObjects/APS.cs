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
        public IBillingSystem Abonents { get; private set; }
        private List<ICallInformation> _onGoingCalls { get; set; }

        public void HandleGetBalanceEvent(object o, BalanceEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(e.Money.ToString()));
        }

        public void HandleCantChangeEvent(object o, ChangeTariffEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs("U can't change tariff atleast" + (30 - DateTime.Now.Subtract(e.TimeOfChanging).TotalDays)));
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdOfPort);
            item.APSMessageShow(new MessageFromAPSEventArgs(e.History));
        }

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            var item = _onGoingCalls.Find(x => e.InitiatorOfEnd.Number == x.Caller || e.InitiatorOfEnd.Number== x.Receiver);
            if (item != null)
            {
                item.SetTimeOfEnding(e.TimeOfEndingOfCall);
                e.SetDurationOfCall(item.GetDuretionOfCall());
                Abonents.AddToHistory(item);
                _onGoingCalls.Remove(item);
                Ports.Find(x=>x.Number==item.Caller).ChangeCallStatus(StatusOfCall.Avaliable);
                Ports.Find(x=>x.Number ==item.Receiver).ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        public void HandleConnectingEvent(object o, AnswerEventArgs e)
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

        public void HandleCallEvent(object o, CallEventArgs e)
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
            Ports.Last().Calling += HandleCallEvent;
            Ports.Last().EndingCall += HandleEndCallEvent;
            Ports.Last().Connecting += HandleConnectingEvent;
        }

        public IPort SignAContract(ITariffPlan tariffPlan)
        {
            var item = Ports.Find(x => x.ContractStatus == StatusOfContract.NotContracted);
            if (item != null)
            {
                Abonents.Contracts.Add(new Contract(Abonents.Contracts.Count, item.Id, item.Number, tariffPlan));
                item.PuttingOnBalance += Abonents.FindContract(item.Id).HandleMoney;
                item.EndingCall += Abonents.FindContract(item.Id).HandleCostOfCall;
                item.ChangingTariff += Abonents.FindContract(item.Id).HandleChangeTariffEvent;
                item.GettingHistory += Abonents.HandleGetHistoryEvent;
                item.GettingHistory += HandleGetHistoryEvent;
                item.GettingBalance += Abonents.HandleGetBalanceEvent;
                item.GettingBalance += HandleGetBalanceEvent;
                Abonents.FindContract(item.Id).CantChangeTariffEvent += HandleCantChangeEvent;
                item.ChangeStatusOfContract();
                return item;
            }
            else {
                AddPort();
                Abonents.Contracts.Add(new Contract(Abonents.Contracts.Count, Ports.Last().Id, Ports.Last().Number, tariffPlan));
                Ports.Last().ChangeStatusOfContract();
                Ports.Last().PuttingOnBalance += Abonents.FindContract(Ports.Last().Id).HandleMoney;
                Ports.Last().EndingCall += Abonents.FindContract(Ports.Last().Id).HandleCostOfCall;
                Ports.Last().GettingHistory += Abonents.HandleGetHistoryEvent;
                Ports.Last().GettingHistory += HandleGetHistoryEvent;
                Ports.Last().GettingBalance += Abonents.HandleGetBalanceEvent;
                Ports.Last().GettingBalance += HandleGetBalanceEvent;
                Ports.Last().ChangingTariff += Abonents.FindContract(Ports.Last().Id).HandleChangeTariffEvent;
                Abonents.FindContract(Ports.Last().Id).CantChangeTariffEvent += HandleCantChangeEvent;
                return Ports.Last();
            }
        }

        public void TerminateContract(IPort port)
        {
            var item = Abonents.FindContract(port.Id);
            if (item != null && port.PortStatus == StatusOfPort.NotConnected)
            {
                port.PuttingOnBalance -= Abonents.FindContract(port.Id).HandleMoney;
                port.EndingCall -= Abonents.FindContract(port.Id).HandleCostOfCall;
                port.ChangingTariff -= Abonents.FindContract(port.Id).HandleChangeTariffEvent;
                port.GettingHistory -= Abonents.HandleGetHistoryEvent;
                port.GettingHistory -= HandleGetHistoryEvent;
                Abonents.FindContract(port.Id).CantChangeTariffEvent -= HandleCantChangeEvent;
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
            _onGoingCalls = new List<ICallInformation>();
        }
    }
}
