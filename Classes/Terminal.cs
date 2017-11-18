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
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;

        public int Id => throw new NotImplementedException();

        public IPort Port { get; private set; }

        protected virtual void OnCall(CallEventArgs e)
        {
            EventHandler<CallEventArgs> handler = CallEvent;
            if (handler != null)
            {
                Console.WriteLine(e.ReceivingNumber);
                handler(this, e);
            }
        }

        public void Call(string number)
        {
            OnCall(new CallEventArgs(GetNumber(), number));
        }

        public void ConnectToPort(IPort port)
        {
            Port = port;
            CallEvent += Port.HandleCallEvent;
        }
        
        public string GetNumber()
        {
            if (Port == null) return null;
            return Port.Number;
        }
    }
}
