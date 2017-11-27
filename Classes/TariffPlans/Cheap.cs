using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.TariffPlans
{
    public class Cheap : ITariffPlan
    {
        public string Name { get; private set; }
        public double CostOfCall { get; private set; }

        public Cheap()
        {
            Name = "First";
            CostOfCall = 0.3;
        }
    }
}
