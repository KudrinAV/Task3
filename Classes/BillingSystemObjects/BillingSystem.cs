using Contracts.CustomArgs;
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
        private List<IContract> _terminatedContracts { get; set; }

        public IContract FindContract(int id)
        {
            var item = Contracts.Find(x => x.IdOfPort == id);
            return item;
        }

        public void TerminateContract(IContract contract)
        {
            _terminatedContracts.Add(contract);
            Contracts.Remove(contract);
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            e.SetHistory(_findHistory(e.Number));
        }

        public void AddToHistory(ICallInformation call)
        {
            var caller = Contracts.Find(x => x.Number == call.Caller);
            call.SetCostOfCall(caller.Tariff.CostOfCall * call.GetDuretionOfCall().TotalSeconds);
            var finding = Contracts.Where(c=>c.Number == call.Caller || c.Number == call.Receiver).Select(x => x);
            foreach(var item in finding)
            {
                item.AllCalls.Add(call);
            }
        }

        private List<string> _findHistory(string number)
        {
            List<string> resultList = new List<string>();
            var finding = Contracts.Find(x => x.Number == number);
            foreach (var item in finding.AllCalls)
            {
                resultList.Add(item.Caller + " " + item.Receiver + " " + item.GetDuretionOfCall().TotalSeconds + " " + item.CostOfCall + " " + item.TimeOfBeginningOfCall );
            }
            return resultList;
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            _terminatedContracts = new List<IContract>();
        }

   
    }
}
