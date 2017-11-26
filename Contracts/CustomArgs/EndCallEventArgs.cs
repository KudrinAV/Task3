using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class EndCallEventArgs : EventArgs
    {
        public IPort InitiatorOfEnd { get; private set; }
        public DateTime TimeOfEndingOfCall { get; private set; }

        public EndCallEventArgs(IPort port)
        {
            InitiatorOfEnd = port;
            TimeOfEndingOfCall = DateTime.Now;
        }
    }
}
