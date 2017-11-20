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
        public int IdOfPort { get; private set; }

        public ITariffPlan Tariff { get; private set; }

        public string Name { get; private set; }

        public DataTime TimeOfSubscribing { get; private set; }

        public DataTime TimeOfChanging { get; private set; }

        public double CostOfCall()
        {
            throw new NotImplementedException();
        }
    }
}
