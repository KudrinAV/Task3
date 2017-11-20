using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.BillingSystemObjects
{
    public class LoneDiggerTariffPlan : ILoneDiggerTariffPlan
    {
        public ITariffPlan Tariff { get; private set; }

        public string Name { get; private set; }

        public DateTime TimeOfSubscribing { get; private set; }

        public DateTime TimeOfChanging { get; private set; }

        public double CostOfCall()
        {
            return 0.5;
        }
    }
}
