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
        List<ICallInformation> AllCalls { get; }
        DateTime TimeOfSigningContract { get;}
        DateTime TimeOfChangingTariff { get; }

        event EventHandler<BalanceEventArgs> DebtRepaidEvent;
        event EventHandler<CantChangeTariffEventArgs> CantChangeTariffEvent;
        event EventHandler<SendHistoryEventArgs> SendHistoryEvent;
        event EventHandler<BalanceEventArgs> SendBalanceEvent;

        void SendBalance(BalanceEventArgs e);
        void SetBalnceAfterCall(double money);
        void SendHistory(GetHistoryEventArgs e);
        void HandleMoneyEvent(object o, BalanceEventArgs e);
        void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e);
    }
}
