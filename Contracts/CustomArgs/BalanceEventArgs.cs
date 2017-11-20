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
        public IPort Port { get; private set; }
        public double Money { get; private set; }

        public BalanceEventArgs(IPort port, double money)
        {
            Port = port;
            Money = money;
        }
    }
}
