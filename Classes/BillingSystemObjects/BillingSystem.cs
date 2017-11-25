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

        private List<string> _findHistory(int id)
        {
            List<string> resultList = new List<string>();
            var finding = from item in FinishedCalls
                          where item.Caller.Id == id || item.Receiver.Id == id
                          select item;
            foreach(var item in finding)
            {
                resultList.Add(item.Caller.Number + " " + item.Receiver.Number + " " + item.GetDuretionOfCall().TotalSeconds + " " + item.CostOfCall + " " + item.TariffPlan.Name);
            }
            return resultList;
        }

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
            e.SetHistory(_findHistory(e.IdOfPort));
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            FinishedCalls = new List<ICallInformation>();
        }

        public void AddContractDataToCallInformation()
        {
            var item = Contracts.Find(x => x.IdOfPort == FinishedCalls.Last().Caller.Id);
            FinishedCalls.Last().SetCostOfCall(FinishedCalls.Last().GetDuretionOfCall().TotalSeconds * item.Tariff.CostOfCall());
            FinishedCalls.Last().SetTarrifPlan(item.Tariff);
        }
    }
}
