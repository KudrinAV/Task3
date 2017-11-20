﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITariffPlan
    {
        string Name { get; }
        DateTime TimeOfSubscribing { get; }
        DateTime TimeOfChanging { get; }

        double CostOfCall();    
    }
}
