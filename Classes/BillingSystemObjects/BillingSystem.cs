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
        private const int _daysInMonth = 30;
        private const double _oneDay = 1;
        private const int _zero = 0;

        public event EventHandler<MessageFromAPSEventArgs> DailyCheckEvent;

        protected virtual void OnDailyCheckEvent(MessageFromAPSEventArgs e)
        {
            DailyCheckEvent?.Invoke(this, e);
        }

        private void _ElapsedDailyCheck()
        {
            _timer = new System.Timers.Timer(1000)
            {
                Enabled = true
            };
            _timer.Elapsed += new ElapsedEventHandler(_timerElapsed);
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void _timerElapsed(object o, ElapsedEventArgs e)
        {
            List<string> debters = _getListOfPayment();
            if (debters.Count != 0)
            {
                OnDailyCheckEvent(new MessageFromAPSEventArgs(debters));
            }
        }

        private List<string> _getListOfPayment()
        {
            List<string> result = new List<string>();
            var finding = Contracts.Where(x => DateTime.Now.Subtract(x.TimeOfSigningContract).TotalSeconds % _daysInMonth <= _oneDay && x.Balance < _zero).Select(x => x.Number);
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
            FindContract(e.IdOfPort).SendBalance(e);
        }

        public void TerminateContract(IContract contract)
        {
            _terminatedContracts.Add(contract);
            Contracts.Remove(contract);
        }

        public void HandleGetHistoryForMonthEvent(object o, GetHistoryEventArgs e)
        {
            FindContract(e.IdOfPort).SendHistory(e);
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

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            _terminatedContracts = new List<IContract>();
            _ElapsedDailyCheck();
        }
    }
}
