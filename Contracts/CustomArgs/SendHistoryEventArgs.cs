using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class SendHistoryEventArgs : GetHistoryEventArgs
    {
        public List<ICallInformation> CallList { get; private set; }

        public SendHistoryEventArgs(int id, string number, List<ICallInformation> list) : base(id, number)
        {
            CallList = list;
        }
    }
}
