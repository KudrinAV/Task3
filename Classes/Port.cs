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

        public StatusOfPort PortStatus => throw new NotImplementedException();

        public StatusOfCall CallStatus { get; private set; }

        public event EventHandler<CallEventArgs> Answer;
        public event EventHandler<CallEventArgs> Calling;
        public event EventHandler<EndCallEventArgs> EndingCall;

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            OnEndingCall(e);
        }

        public void GetAnswer(CallEventArgs e)
        {
            Console.WriteLine("Hello dear" + e.ReceivingNumber);
            OnAnswer(e);
        }

        protected virtual void OnEndingCall(EndCallEventArgs e)
        {
            EndingCall?.Invoke(this, e);
        }

        protected virtual void OnAnswer(CallEventArgs e)
        {
            Answer?.Invoke(this, e);
        }
        
        public void HandleCallEvent(object o,CallEventArgs e)
        {
            OnCalling(e);
        }

        protected virtual void OnCalling(CallEventArgs e)
        {
            Calling?.Invoke(this, e);
        }

        public void ChangeCallStatus(StatusOfCall status)
        {
            CallStatus = status;
        }

        public int GetIdOfTerminal()
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus()
        {
            throw new NotImplementedException();
        }

        public StatusOfConnect GetAnswer(string number)
        {
            throw new NotImplementedException();
        }

        public Port(string number)
        {
            Number = number;
        }
        
    }
}
