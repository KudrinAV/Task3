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
        public int IdOfPort { get; private set; }
        public DateTime TimeOfChanging { get; private set; }
        public ITariffPlan NewTariffPlan { get; private set; }

        public ChangeTariffEventArgs(int id, ITariffPlan tariffPlan)
        {
            IdOfPort = id;
            TimeOfChanging = DateTime.Now;
            NewTariffPlan = tariffPlan;
        }
    }
}
