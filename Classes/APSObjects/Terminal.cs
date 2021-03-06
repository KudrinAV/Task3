﻿using Contracts.CustomArgs;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Terminal : ITerminal
    {
        public int Id { get; private set; }
        private IPort _port { get; set; }

        private event EventHandler<CallEventArgs> _callEvent;
        private event EventHandler<EndCallEventArgs> _endCallEvent;
        private event EventHandler<BalanceEventArgs> _putOnBalanceEvent;
        private event EventHandler<ChangeTariffEventArgs> _changeTariffEvent;
        private event EventHandler<GetHistoryEventArgs> _getHistoryEvent;
        private event EventHandler<AnswerEventArgs> _connectEvent;
        private event EventHandler<BalanceEventArgs> _getBalanceEvent;

        protected virtual void OnGetBalanceEvent(BalanceEventArgs e)
        {
            _getBalanceEvent?.Invoke(this, e);
        }

        protected virtual void OnConnectEvent(AnswerEventArgs e)
        {
            _connectEvent?.Invoke(this, e);
        }

        protected virtual void OnGetHistoryEvent(GetHistoryEventArgs e)
        {
            _getHistoryEvent?.Invoke(this, e);
        }

        protected virtual void OnChangeTariffEvent(ChangeTariffEventArgs e)
        {
            _changeTariffEvent?.Invoke(this, e);
        }

        protected virtual void OnPutOnBalanceEvent(BalanceEventArgs e)
        {
            _putOnBalanceEvent?.Invoke(this, e);
        }

        protected virtual void OnEndCall(EndCallEventArgs e)
        {
            _endCallEvent?.Invoke(this, e);
        }

        protected virtual void OnCall(CallEventArgs e)
        {
            _callEvent?.Invoke(this, e);
        }

        private void _handleMessageFromAPSEvent(object o, MessageFromAPSEventArgs e)
        {
            if (e.ListOfCalls != null)
            {
                Console.WriteLine("Filter calls by: ");
                Console.WriteLine("1 - Show all Calls ");
                Console.WriteLine("2 - Number ");
                Console.WriteLine("3 - Diaposon of cost");
                Console.WriteLine("4 - Date");
                Console.WriteLine("Insert menu");
                bool result = int.TryParse(Console.ReadLine(), out int lever);
                if (result) getFilterParametrs(e.ListOfCalls, lever);
            }
            else Console.WriteLine(e.Message);
        }

        private void getFilterParametrs(List<ICallInformation> list, int switcher)
        {
            switch (switcher)
            {
                case 1:
                    Console.WriteLine("All list :");
                    filterCalls(list);
                    break;
                case 2:
                    Console.WriteLine("Input number : ");
                    filterCalls(list, Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Input diaposon of cost : ");
                    filterCalls(list, Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture), Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));
                    break;
                case 4:
                    Console.WriteLine("Input date : ");
                    filterCalls(list, DateTime.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }

        private void filterCalls(List<ICallInformation> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Caller + " " + item.Receiver + " " + item.BeginningOfCall.ToString("dd-MM-yyyy") + " " + item.CostOfCall.ToString("F"));
            }
        }

        private void filterCalls(List<ICallInformation> list, string number)
        {
            if (number.Length == 7)
            {
                var finding = list.Where(x => x.Caller == number || x.Receiver == number).ToList();
                filterCalls(finding);
            }
        }

        private void filterCalls(List<ICallInformation> list, double minCostOfCall, double maxCostOfCall)
        {
            if (maxCostOfCall >= minCostOfCall)
            {
                var finding = list.Where(x => x.CostOfCall >= minCostOfCall && x.CostOfCall <= maxCostOfCall).ToList();
                filterCalls(finding);
            }
        }

        private void filterCalls(List<ICallInformation> list, DateTime time)
        {
            var finding = list.Where(x => x.BeginningOfCall.Day == time.Day && x.BeginningOfCall.Month == time.Month && x.BeginningOfCall.Year == time.Year).ToList();
            filterCalls(finding);
        }

        private void handleAnswerEvent(object o, CallEventArgs e)
        {
            OnConnectEvent(new AnswerEventArgs(e.PortOfCaller.Number, _port.Number, getAbonentAnser(e.PortOfCaller.Number)));
        }

        private StatusOfAnswer getAbonentAnser(string number)
        {

            Console.WriteLine("You getting call from " + number);
            Console.WriteLine("Y or y- accept || Anything else - decline ");
            string answer = Console.ReadLine();
            if (answer == "Y" || answer == "y")
            {
                return StatusOfAnswer.Answer;
            }
            else return StatusOfAnswer.Decline;
        }

        public void GetHistory()
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable) OnGetHistoryEvent(new GetHistoryEventArgs(_port.Id, _port.Number));
        }

        public void ChangeTariff(ITariffPlan tariffPlan)
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable) OnChangeTariffEvent(new ChangeTariffEventArgs(_port.Id, tariffPlan));
        }

        public void PutMoney(double money)
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus != StatusOfCall.OnCall) OnPutOnBalanceEvent(new BalanceEventArgs(_port.Id, money));
        }

        public void EndCall()
        {
            if (_port.CallStatus == StatusOfCall.OnCall && _port.PortStatus == StatusOfPort.Connected)
            {
                OnEndCall(new EndCallEventArgs(_port));
            }
        }

        public void Call(string number)
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable) OnCall(new CallEventArgs(_port, number));
        }

        public void ConnectToPort(IPort port)
        {
            if (_port == null)
            {
                _port = port;
                _port.ChangeCallStatus(StatusOfCall.Avaliable);
                _port.ChangeStatusOfPort();
                _callEvent += _port.HandleCallEvent;
                _port.AnswerEvent += handleAnswerEvent;
                _endCallEvent += _port.HandleEndCallEvent;
                _port.MessageFromAPS += _handleMessageFromAPSEvent;
                _putOnBalanceEvent += _port.HandlePutOnBalanceEvent;
                _changeTariffEvent += _port.HandleChangeTariffEvent;
                _getHistoryEvent += _port.HandleGetHistoryEvent;
                _connectEvent += _port.HandleConnectEvent;
                _getBalanceEvent += _port.HandleGetBalanceEvent;
            }
            else Console.WriteLine("Terminal " + Id + " already has a port");
        }

        public void DissconnectFromPort()
        {
            if (_port != null)
            {
                _port.ChangeCallStatus(StatusOfCall.NotAvalibale);
                _port.ChangeStatusOfPort();
                _callEvent -= _port.HandleCallEvent;
                _port.AnswerEvent -= handleAnswerEvent;
                _endCallEvent -= _port.HandleEndCallEvent;
                _port.MessageFromAPS -= _handleMessageFromAPSEvent;
                _putOnBalanceEvent -= _port.HandlePutOnBalanceEvent;
                _changeTariffEvent -= _port.HandleChangeTariffEvent;
                _getHistoryEvent -= _port.HandleGetHistoryEvent;
                _connectEvent -= _port.HandleConnectEvent;
                _getBalanceEvent -= _port.HandleGetBalanceEvent;
                _port = null;
            }
            else Console.WriteLine("Terminal " + Id + " has nothing to disconect from");
        }

        public void GetBalance()
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable) OnGetBalanceEvent(new BalanceEventArgs(_port.Id));
        }

        public string GetNumber()
        {
            if (_port == null) return null;
            return _port.Number;
        }

        public IPort GetPort()
        {
            return _port;
        }

        public Terminal(int id)
        {
            Id = id;
        }
    }
}
