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

        private List<ICallInformation> _finishedCalls { get; set; }
        private List<IContract> _terminatedContracts { get; set; }

        public IContract FindContract(int id)
        {
            foreach(var item in Contracts)
            {
                if(item.IdOfPort == id)
                return item;
            }
            return null;
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
            var item = Contracts.Find(x => x.Number == call.Caller);
            call.SetCostOfCall(item.Tariff.CostOfCall * call.GetDuretionOfCall().TotalSeconds); 
            _finishedCalls.Add(call);
        }

        private List<string> _findHistory(string number)
        {
            List<string> resultList = new List<string>();
            var finding = from item in _finishedCalls
                          where item.Caller == number || item.Receiver == number
                          select item;
            foreach (var item in finding)
            {
                resultList.Add(item.Caller + " " + item.Receiver + " " + item.GetDuretionOfCall().TotalSeconds + " " + item.CostOfCall + " " + item.TimeOfBeginningOfCall );
            }
            return resultList;
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            _finishedCalls = new List<ICallInformation>();
        }

   
    }
}
