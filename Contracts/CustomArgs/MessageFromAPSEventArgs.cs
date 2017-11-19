using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class MessageFromAPSEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageFromAPSEventArgs(string message)
        {
            Message = message;
        }
    }
}
