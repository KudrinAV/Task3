using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.BillingSystemObjects
{
    public class BillingSystem : IBillingSystem
    {
        public List<IContract> Contracts { get; private set; }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
        }
    }
}
