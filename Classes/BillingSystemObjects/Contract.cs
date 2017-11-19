using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.BillingSystemObjects
{
    public class Contract : IContract
    {
        public int IdOfPort => throw new NotImplementedException();

        public ITariffPlan Tariff => throw new NotImplementedException();
    }
}
