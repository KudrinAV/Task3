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
        public int IdOfPort => throw new NotImplementedException();

        public ITariffPlan Tariff => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public double Balance => throw new NotImplementedException();

        public DataTime TimeOfSubscribing => throw new NotImplementedException();

        public DataTime TimeOfChanging => throw new NotImplementedException();

        public double CostOfCall()
        {
            throw new NotImplementedException();
        }
    }
}
