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
        event EventHandler<CallArgs> CallEvent;

        public int Id => throw new NotImplementedException();

        public IPort Port { get; private set; }

        protected virtual void OnCall(CallArgs e)
        {
            EventHandler<CallArgs> handler = CallEvent;
            if (handler != null)
            {
                Console.WriteLine(e.ReceivingNumber);
                handler(this, e);
            }
        }

        public void Call(string number)
        {
            OnCall(new CallArgs(number));
        }

        public void ConnectToPort(IPort port)
        {
            Port = port;
            CallEvent += Port.HandleCallEvent;
        }
        

        event EventHandler<CallArgs> ITerminal.CallEvent
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }
    }
}
