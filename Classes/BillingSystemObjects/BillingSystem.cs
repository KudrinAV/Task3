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

        public IContract FindContract(int id)
        {
            foreach(var item in Contracts)
            {
                if(item.IdOfPort == id)
                return item;
            }
            return null;
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
        }
    }
}
