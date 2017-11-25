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

        public DateTime TimeOfSubscribing => throw new NotImplementedException();

        public DateTime TimeOfChanging => throw new NotImplementedException();

        public double CostOfCall()
        {
            return 0.5;
        }
    }
}
