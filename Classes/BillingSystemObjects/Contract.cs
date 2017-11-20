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
        public IPort Port { get; private set; }

        public ITariffPlan Tariff { get; private set; }

        public double Balance => throw new NotImplementedException();

        public Contract(IPort port , ITariffPlan tariffPlan)
        {
            Port = port;
            Tariff = tariffPlan;
            port.ChangeStatusOfContract();

        }
    }
}
