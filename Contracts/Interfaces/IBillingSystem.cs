using Contracts.CustomArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IBillingSystem
    {
        List<IContract> Contracts { get; }
        IContract FindContract(int id);
        
        void HandleGetBalanceEvent(object o, BalanceEventArgs e);
        void TerminateContract(IContract contract);
        void HandleGetHistoryForMonthEvent(object o, GetHistoryEventArgs e);
        void HandleGetHistoryEvent(object o, GetHistoryEventArgs e);
        void AddToHistory(ICallInformation call);
    }
}
