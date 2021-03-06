﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.Interfaces;
using Contracts.CustomArgs;

namespace Classes.Ports
{
    public class Port : IPort
    {
        public string Number { get; private set; }
        public int Id { get; private set; }
        public StatusOfPort PortStatus { get; private set; }
        public StatusOfCall CallStatus { get; private set; }
        public StatusOfContract ContractStatus { get; private set; }

        public event EventHandler<CallEventArgs> AnswerEvent;
        public event EventHandler<AnswerEventArgs> Connecting;
        public event EventHandler<CallEventArgs> Calling;
        public event EventHandler<EndCallEventArgs> EndingCall;
        public event EventHandler<MessageFromAPSEventArgs> MessageFromAPS;
        public event EventHandler<BalanceEventArgs> PuttingOnBalance;
        public event EventHandler<ChangeTariffEventArgs> ChangingTariff;
        public event EventHandler<GetHistoryEventArgs> GettingHistory;
        public event EventHandler<BalanceEventArgs> GettingBalance;

        protected virtual void OnGettingBalance(BalanceEventArgs e)
        {
            GettingBalance?.Invoke(this, e);
        }

        protected virtual void OnCalling(CallEventArgs e)
        {
            Calling?.Invoke(this, e);
        }

        protected virtual void OnConnecting(AnswerEventArgs e)
        {
            Connecting?.Invoke(this, e);
        }

        protected virtual void OnGettingHistory(GetHistoryEventArgs e)
        {
            GettingHistory?.Invoke(this, e);
        }

        protected virtual void OnPuttingOnBalance(BalanceEventArgs e)
        {
            PuttingOnBalance?.Invoke(this, e);
        }

        protected virtual void OnChangingTariff(ChangeTariffEventArgs e)
        {
            ChangingTariff?.Invoke(this, e);
        }

        protected virtual void OnMessageFromAPS(MessageFromAPSEventArgs e)
        {
            MessageFromAPS?.Invoke(this, e);
        }

        protected virtual void OnEndingCall(EndCallEventArgs e)
        {
            EndingCall?.Invoke(this, e);
        }

        protected virtual void OnAnswerEvent(CallEventArgs e)
        {
            AnswerEvent?.Invoke(this, e);
        }

        public void HandleGetBalanceEvent(object o, BalanceEventArgs e)
        {
            OnGettingBalance(e);
        }

        public void HandlePutOnBalanceEvent(object o, BalanceEventArgs e)
        {
            OnPuttingOnBalance(e);
        }

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            OnEndingCall(e);
        }

        public void HandleConnectEvent(object o, AnswerEventArgs e)
        {
            OnConnecting(e);
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            OnGettingHistory(e);
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            OnChangingTariff(e);
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            OnCalling(e);
        }

        public void APSMessageShow(MessageFromAPSEventArgs e)
        {
            OnMessageFromAPS(e);
        }

        public void GetAnswer(CallEventArgs e)
        {
            OnAnswerEvent(e);
        }

        public void ChangeCallStatus(StatusOfCall status)
        {
            CallStatus = status;
        }

        public void ChangeStatusOfPort()
        {
            PortStatus = PortStatus != StatusOfPort.NotConnected ? StatusOfPort.NotConnected : StatusOfPort.Connected;
        }

        public void ChangeStatusOfContract()
        {
            ContractStatus = ContractStatus != StatusOfContract.NotContracted ? StatusOfContract.NotContracted : StatusOfContract.Contracted;
        }

        public Port(int id, string number)
        {
            Number = number;
            Id = id;
            PortStatus = StatusOfPort.NotConnected;
            ContractStatus = StatusOfContract.NotContracted;
        }
    }
}
