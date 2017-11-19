using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITariffPlan
    {
        string Name { get; }
        double Balance { get; }
        DataTime TimeOfSubscribing { get; }
        DataTime TimeOfChanging { get; }

        double CostOfCall();    
    }
}
