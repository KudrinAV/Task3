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

        public StatusOfCall CallStatus => throw new NotImplementedException();

        public event EventHandler<CallEventArgs> Calling;
        
        public void HandleCallEvent(object o,CallEventArgs e)
        {
            OnCalling(e);
        }

        protected virtual void OnCalling(CallEventArgs e)
        {
            EventHandler<CallEventArgs> handler = Calling;
            if (handler != null)
            {
                Console.WriteLine("hello" + e.ReceivingNumber);
                handler(this, e);
            }
        }

        public void ChangeCallStatus(StatusOfCall status)
        {
            throw new NotImplementedException();
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
