using Contracts.CustomArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IContract
    {
        int IdOfPort { get; }
        ITariffPlan Tariff { get; }
        double Balance { get; }

        void HandleMoney(object o, BalanceEventArgs e);
    }
}
