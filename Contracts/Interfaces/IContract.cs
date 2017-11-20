﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IContract
    {
        IPort Port { get; }
        ITariffPlan Tariff { get; }
        double Balance { get; }
    }
}
