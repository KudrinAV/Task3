using Contracts.Interfaces;
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
        public List<string> ListOfDebters { get; private set; }
        public List<ICallInformation> ListOfCalls { get; private set; }

        public MessageFromAPSEventArgs(string message)
        {
            Message = message;
        }

        public MessageFromAPSEventArgs(List<string> debters)
        {
            ListOfDebters = debters;
        }

        public MessageFromAPSEventArgs(List<ICallInformation> list)
        {
            ListOfCalls = list;
        }
    }
}
