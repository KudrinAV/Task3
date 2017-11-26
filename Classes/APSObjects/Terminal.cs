using Contracts.CustomArgs;
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

        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;
        public event EventHandler<BalanceEventArgs> PutOnBalanceEvent;
        public event EventHandler<ChangeTariffEventArgs> ChangeTariffEvent;
        public event EventHandler<GetHistoryEventArgs> GetHistoryEvent;
        public event EventHandler<AnswerEventArgs> ConnectEvent;
        public event EventHandler<BalanceEventArgs> GetBalanceEvent;

        protected virtual void OnGetBalanceEvent(BalanceEventArgs e)
        {
            GetBalanceEvent?.Invoke(this, e);
        }

        protected virtual void OnConnectEvent(AnswerEventArgs e)
        {
            ConnectEvent?.Invoke(this, e);
        }

        protected virtual void OnGetHistoryEvent(GetHistoryEventArgs e)
        {
            GetHistoryEvent?.Invoke(this, e);
        }

        protected virtual void OnChangeTariffEvent(ChangeTariffEventArgs e)
        {
            ChangeTariffEvent?.Invoke(this, e);
        }

        protected virtual void OnPutOnBalanceEvent(BalanceEventArgs e)
        {
            PutOnBalanceEvent?.Invoke(this, e);
        }

        protected virtual void OnEndCall(EndCallEventArgs e)
        {
            EndCallEvent?.Invoke(this, e);
        }

        protected virtual void OnCall(CallEventArgs e)
        {
            CallEvent?.Invoke(this, e);
        }

        public void HandleMessageFromAPSEvent(object o, MessageFromAPSEventArgs e)
        {
            if (e.ListOfCalls != null)
            {
                Console.WriteLine("1 - все звонки ");
                Console.WriteLine("2 - фильтрация по номеру ");
                Console.WriteLine("3 - фильтрация по стоимости ");
                Console.WriteLine("4 - фильтрация по дате ");
                Console.WriteLine("Введите пункт меню: ");
                bool result = int.TryParse(Console.ReadLine(), out int lever);
                if (result) _getFilterParametrs(e.ListOfCalls, lever);
                else Console.WriteLine("Wrong input");
            }
            else Console.WriteLine(e.Message);
        }

        private void _getFilterParametrs(List<ICallInformation> list, int switcher) 
        {
            switch(switcher)
            {
                case 1:
                    Console.WriteLine("Весь список:");
                    _filterCalls(list);
                    break;
                case 2:
                    Console.WriteLine("Введите номер: ");
                    _filterCalls(list, Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Введите диапозон сумм ");
                    _filterCalls(list, Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture), Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));
                    break;
                case 4:
                    Console.WriteLine("Введите дату: ");
                    _filterCalls(list, DateTime.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }

        private void _filterCalls(List<ICallInformation> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Caller + " " + item.Receiver + " " + item.TimeOfBeginningOfCall.ToString("dd-MM-yyyy") + " " + item.CostOfCall.ToString("F"));
            }
        }

        private void _filterCalls(List<ICallInformation> list, string number)
        {
            if (number.Length == 7)
            {
                var finding = list.Where(x => x.Caller == number || x.Receiver == number);
                foreach (var item in finding)
                {
                    Console.WriteLine(item.Caller + " " + item.Receiver + " " + item.TimeOfBeginningOfCall.ToString("dd-MM-yyyy") + " " + item.CostOfCall.ToString("F"));
                }
            }
            else Console.WriteLine("There is no such number");
        }

        private void _filterCalls(List<ICallInformation> list, double minCostOfCall, double maxCostOfCall)
        {
            if (maxCostOfCall >=minCostOfCall)
            {
                var finding = list.Where(x => x.CostOfCall >= minCostOfCall && x.CostOfCall<= maxCostOfCall);
                foreach (var item in finding)
                {
                    Console.WriteLine(item.Caller + " " + item.Receiver + " " + item.TimeOfBeginningOfCall.ToString("dd-MM-yyyy") + " " + item.CostOfCall.ToString("F"));
                }
            }
            else Console.WriteLine("There is problems with user's input");
        }

        private void _filterCalls(List<ICallInformation> list, DateTime time)
        {
            var finding = list.Where(x => x.TimeOfBeginningOfCall.Day == time.Day);
            foreach (var item in finding)
            {
                Console.WriteLine(item.Caller + " " + item.Receiver + " " + item.TimeOfBeginningOfCall.ToString("dd-MM-yyyy") + " " + item.CostOfCall.ToString("F"));
            }
        }

        public void HandleAnswerEvent(object o, CallEventArgs e)
        {
            OnConnectEvent(new AnswerEventArgs(e.PortOfCaller.Number, _port.Number, _getAbonentAnser(e.PortOfCaller.Number)));
        }

        private StatusOfAnswer _getAbonentAnser(string number)
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
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable)  OnGetHistoryEvent(new GetHistoryEventArgs(_port.Id, _port.Number));
        }

        public void ChangeTariff(ITariffPlan tariffPlan)
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable) OnChangeTariffEvent(new ChangeTariffEventArgs(_port.Id, tariffPlan));
        }

        public void PutMoney(double money)
        {
            if (_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus == StatusOfCall.Avaliable)  OnPutOnBalanceEvent(new BalanceEventArgs(_port.Id, money));
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
                CallEvent += _port.HandleCallEvent;
                _port.AnswerEvent += HandleAnswerEvent;
                EndCallEvent += _port.HandleEndCallEvent;
                _port.MessageFromAPS += HandleMessageFromAPSEvent;
                PutOnBalanceEvent += _port.HandlePutOnBalanceEvent;
                ChangeTariffEvent += _port.HandleChangeTariffEvent;
                GetHistoryEvent += _port.HandleGetHistoryEvent;
                ConnectEvent += _port.HandleConnectEvent;
                GetBalanceEvent += _port.HandleGetBalanceEvent;
            }
            else Console.WriteLine("Terminal " + Id + " already has a port");
        }

        public void DissconnectFromPort()
        {
            if (_port != null)
            {
                _port.ChangeCallStatus(StatusOfCall.NotAvalibale);
                _port.ChangeStatusOfPort();
                CallEvent -= _port.HandleCallEvent;
                _port.AnswerEvent -= HandleAnswerEvent;
                EndCallEvent -= _port.HandleEndCallEvent;
                _port.MessageFromAPS -= HandleMessageFromAPSEvent;
                PutOnBalanceEvent -= _port.HandlePutOnBalanceEvent;
                ChangeTariffEvent -= _port.HandleChangeTariffEvent;
                GetHistoryEvent -= _port.HandleGetHistoryEvent;
                ConnectEvent -= _port.HandleConnectEvent;
                GetBalanceEvent -= _port.HandleGetBalanceEvent;
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
