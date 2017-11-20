using System;
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
        public event EventHandler<CallEventArgs> Calling;
        public event EventHandler<EndCallEventArgs> EndingCall;
        public event EventHandler<MessageFromAPSEventArgs> MessageFromAPS;

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            OnEndingCall(e);
        }

        public void GetAnswer(CallEventArgs e)
        {
            OnAnswerEvent(e);
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
        
        public void HandleCallEvent(object o,CallEventArgs e)
        {
            OnCalling(e);
        }

        public void APSMessageShow(MessageFromAPSEventArgs e)
        {
            OnMessageFromAPS(e);
        }

        protected virtual void OnCalling(CallEventArgs e)
        {
            Calling?.Invoke(this, e);
        }

        public void ChangeCallStatus(StatusOfCall status)
        {
            CallStatus = status;
        }

        public void ChangeStatusOfPort()
        {
            PortStatus = PortStatus != StatusOfPort.NotConnected ? StatusOfPort.NotConnected : StatusOfPort.Connected;
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
