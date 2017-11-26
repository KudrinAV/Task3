using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class GetHistoryEventArgs : EventArgs
    {
        public int IdOfPort { get; private set; }
        public string Number { get; private set; }

        public GetHistoryEventArgs(int id, string number)
        {
            IdOfPort = id;
            Number = number;
        }
    }
}
