using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.TariffPlans
{
    public class Expensive : ITariffPlan
    {
        public string Name { get; private set; }
        public double CostOfCall { get; private set; }

        public Expensive()
        {
            Name = "Second";
            CostOfCall = 0.5;
        }
    }
}
