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

        public List<ICallInformation> FinishedCalls { get; private set; }

        public IContract FindContract(int id)
        {
            foreach(var item in Contracts)
            {
                if(item.IdOfPort == id)
                return item;
            }
            return null;
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            e.SetHistory(_findHistory(e.IdOfPort.ToString()));
        }

        public void AddContractDataToCallInformation()
        {
            var item = Contracts.Find(x => x.IdOfPort.ToString() == FinishedCalls.Last().Caller);
            FinishedCalls.Last().SetCostOfCall(FinishedCalls.Last().GetDuretionOfCall().TotalSeconds * item.Tariff.CostOfCall());
        }

        private List<string> _findHistory(string number)
        {
            List<string> resultList = new List<string>();
            var finding = from item in FinishedCalls
                          where item.Caller == number || item.Receiver == number
                          select item;
            foreach (var item in finding)
            {
                resultList.Add(item.Caller + " " + item.Receiver + " " + item.GetDuretionOfCall().TotalSeconds + " " + item.CostOfCall );
            }
            return resultList;
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            FinishedCalls = new List<ICallInformation>();
        }

   
    }
}
