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
            if (e.History != null)
            {
                foreach (var item in e.History)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine(e.Message + " " + _port.Number);
        }

        public void HandleAnswerEvent(object o, CallEventArgs e)
        {
            OnConnectEvent(new AnswerEventArgs(e.PortOfCaller.Number, _port.Number, _getAbonentAnser(e.PortOfCaller.Number)));
        }

        private StatusOfAnswer _getAbonentAnser(string number)
        {

            Console.WriteLine("You getting call from " + number);
            Console.WriteLine("Y- accept || N- decline");
            string answer = Console.ReadLine();
            if (answer == "Y")
            {
                return StatusOfAnswer.Answer;
            }
            else return StatusOfAnswer.Decline;
        }

        public void GetHistory()
        {
            OnGetHistoryEvent(new GetHistoryEventArgs(_port.Id, _port.Number));
        }

        public void ChangeTariff(ITariffPlan tariffPlan)
        {
            OnChangeTariffEvent(new ChangeTariffEventArgs(_port.Id, tariffPlan));
        }

        public void PutMoney(double money)
        {
            OnPutOnBalanceEvent(new BalanceEventArgs(_port.Id, money));
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
            if(_port != null && _port.PortStatus == StatusOfPort.Connected && _port.CallStatus==StatusOfCall.Avaliable) OnCall(new CallEventArgs(_port, number));
        }

        public void ConnectToPort(IPort port)
        {
            if (_port==null)
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
            if (_port!=null)
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
