using Contracts.CustomArgs;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Classes.BillingSystemObjects
{
    public class BillingSystem : IBillingSystem
    {
        public List<IContract> Contracts { get; private set; }
        private List<IContract> _terminatedContracts { get; set; }
        public object DataTime { get; private set; }
        private System.Timers.Timer _timer { get; set; }

        public event EventHandler<MessageFromAPSEventArgs> DailyCheckEvent;

        protected virtual void OnDailyCheckEvent(MessageFromAPSEventArgs e)
        {
            DailyCheckEvent?.Invoke(this, e);
        }

        private void _ElapsedDailyCheck()
        {
            _timer = new System.Timers.Timer(100000)
            {
                Enabled = true
            };
            _timer.Elapsed += new ElapsedEventHandler(_timerElapsed);
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void _timerElapsed(object o, ElapsedEventArgs e)
        {
            OnDailyCheckEvent(new MessageFromAPSEventArgs(_getListOfPayment()));
        }

        private List<string> _getListOfPayment()
        {
            List<string> result = new List<string>();
            var finding = Contracts.Where(x => DateTime.Now.Subtract(x.TimeOfSigningContract).TotalDays % 30 < 30 && x.Balance< 0.0).Select(x => x.Number);
            foreach (var item in finding)
            {
                result.Add(item);
            }
            return result;
        }


        public IContract FindContract(int id)
        {
            var item = Contracts.Find(x => x.IdOfPort == id);
            return item;
        }

        public void HandleGetBalanceEvent(object o, BalanceEventArgs e)
        {
            e.GetBalance(FindContract(e.IdOfPort).Balance);
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

        public void HandleGetHistoryForMonthEvent(object o, GetHistoryEventArgs e)
        {
            e.SetHistory(_findMonthHistory(e.Number));
        }

        public void AddToHistory(ICallInformation call)
        {
            var caller = Contracts.Find(x => x.Number == call.Caller);
            call.SetCostOfCall(caller.Tariff.CostOfCall * call.GetDuretionOfCall().TotalSeconds);
            caller.SetBalnceAfterCall(call.CostOfCall);
            var finding = Contracts.Where(c => c.Number == call.Caller || c.Number == call.Receiver).Select(x => x);
            foreach (var item in finding)
            {
                item.AllCalls.Add(call);
            }
        }

        private List<ICallInformation> _findHistory(string number)
        {
            var item = Contracts.Find(x => x.Number == number);
            return item.AllCalls;
        }

        private List<ICallInformation> _findMonthHistory(string number)
        {
            List<ICallInformation> resultList = new List<ICallInformation>();
            var contract = Contracts.Find(x => x.Number == number);
            var finding = contract.AllCalls.Where(x => DateTime.Now.Subtract(x.TimeOfBeginningOfCall).TotalDays <= 30).Select(i => i);
            foreach (var item in finding)
            {
                resultList.Add(item);
            }
            return resultList;
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            _terminatedContracts = new List<IContract>();
            _ElapsedDailyCheck();
        }


    }
}
