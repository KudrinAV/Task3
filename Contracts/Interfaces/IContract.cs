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
        string Number { get; }
        ITariffPlan Tariff { get; }
        double Balance { get; }

        event EventHandler<ChangeTariffEventArgs> CantChangeTariffEvent;
        void HandleMoney(object o, BalanceEventArgs e);
        void HandleCostOfCall(object o, EndCallEventArgs e);
        void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e);
    }
}
