using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class ChangeTariffEventArgs : EventArgs
    {
        public IPort Port { get; private set; }
        public DateTime TimeOfChanging { get; private set; }
        public ITariffPlan NewTariffPlan { get; private set; }

        public ChangeTariffEventArgs(IPort port, ITariffPlan tariffPlan)
        {
            Port = port;
            NewTariffPlan = tariffPlan;
        }

        public void SetNewTime(DateTime time)
        {
            TimeOfChanging = time;
        }
    }
}
