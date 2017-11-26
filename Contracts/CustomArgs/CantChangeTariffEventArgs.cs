using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class CantChangeTariffEventArgs : EventArgs
    {
        public int IdOfPort { get; private set; }
        public DateTime TimeOfChanging { get; private set; }

        public CantChangeTariffEventArgs(int id, DateTime time)
        {
            IdOfPort = id;
            TimeOfChanging = time;
        }
    }
}
