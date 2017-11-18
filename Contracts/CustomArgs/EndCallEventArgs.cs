using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class EndCallEventArgs: EventArgs
    {
        public string ReceivingNumber { get; private set; }
        public string CallerNumber { get; private set; }

        public EndCallEventArgs(string caller, string reciver)
        {
            CallerNumber = caller;
            ReceivingNumber = reciver;
        }
    }
}
