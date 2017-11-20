﻿using Contracts.CustomArgs;
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
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;
        public event EventHandler<BalanceEventArgs> PutOnBalanceEvent;
        public event EventHandler<ChangeTariffEventArgs> ChangeTariffEvent;
        public event EventHandler<GetHistoryEventArgs> GetHistoryEvent;

        public int Id { get; private set; }

        public IPort Port { get; private set; }

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

        public void HandleAnswerEvent(object o, CallEventArgs e)
        {
            Console.WriteLine("You getting call from" + e.PortOfCaller.Number);
            Console.WriteLine("Y- accept || N- decline");
            string answer = Console.ReadLine();
            e.SetAnswerStatus(answer);
        }

        public void HandleMessageFromAPSEvent(object o, MessageFromAPSEventArgs e)
        {
            if (e.History != null)
            {
                foreach(var item in e.History)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine(e.Message + " " + Port.Number);
        }

        public void GetHistory(int id)
        {
            OnGetHistoryEvent(new GetHistoryEventArgs(id));
        }

        public void ChangeTariff(ITariffPlan tariffPlan)
        {
            OnChangeTariffEvent(new ChangeTariffEventArgs(Port, tariffPlan));
        }

        public void PutMoney(double money)
        {
            OnPutOnBalanceEvent(new BalanceEventArgs(Port, money));
        }

        public void EndCall()
        {
            if (Port.CallStatus == StatusOfCall.OnCall)
            {
                OnEndCall(new EndCallEventArgs(Port));
            }
        }

        public void Call(string number)
        {
            if(Port != null) OnCall(new CallEventArgs(Port, number));
        }

        public void ConnectToPort(IPort port)
        {
            if (Port==null)
            {
                Port = port;
                Port.ChangeCallStatus(StatusOfCall.Avaliable);
                Port.ChangeStatusOfPort();
                CallEvent += Port.HandleCallEvent;
                Port.AnswerEvent += HandleAnswerEvent;
                EndCallEvent += Port.HandleEndCallEvent;
                Port.MessageFromAPS += HandleMessageFromAPSEvent;
                PutOnBalanceEvent += Port.HandlePutOnBalanceEvent;
                ChangeTariffEvent += Port.HandleChangeTariffEvent;
                GetHistoryEvent += Port.HandleGetHistoryEvent;
            }
            else Console.WriteLine("Terminal " + Id + " already has a port");
        }

        public void DissconnectFromPort()
        {
            if (Port!=null)
            {
                Port.ChangeCallStatus(StatusOfCall.NotAvalibale);
                Port.ChangeStatusOfPort();
                CallEvent -= Port.HandleCallEvent;
                Port.AnswerEvent -= HandleAnswerEvent;
                EndCallEvent -= Port.HandleEndCallEvent;
                Port.MessageFromAPS -= HandleMessageFromAPSEvent;
                PutOnBalanceEvent -= Port.HandlePutOnBalanceEvent;
                ChangeTariffEvent -= Port.HandleChangeTariffEvent;
                GetHistoryEvent -= Port.HandleGetHistoryEvent;
                Port = null;
            }
            else Console.WriteLine("Terminal " + Id + " has nothing to disconect from");
        }
        
        public string GetNumber()
        {
            if (Port == null) return null;
            return Port.Number;
        }

        public Terminal(int id)
        {
            Id = id;
        }
    }
}
