using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class CallEventArgs : EventArgs
    {
        public string ReceivingNumber { get; private set; }
        public IPort PortOfCaller { get; private set; }

        public CallEventArgs(IPort port, string reciver)
        {
            PortOfCaller = port;
            ReceivingNumber = reciver;
        }
    }
}
