using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class BalanceEventArgs : EventArgs
    {
        public int IdOfPort { get; private set; }
        public double Money { get; private set; }

        public void GetBalance(double money)
        {
            Money = money;
        }

        public BalanceEventArgs(int id)
        {
            IdOfPort = id;
        }

        public BalanceEventArgs(int id, double money)
        {
            IdOfPort = id;
            Money = money;
        }
    }
}
