using Contracts.CustomArgs;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.BillingSystemObjects
{
    public class Contract : IContract
    {
        public int IdOfContract { get; private set; }
        public int IdOfPort { get; private set; }
        public string Number { get; private set; }
        public string Name { get; private set; }
        public ITariffPlan Tariff { get; private set; }
        public double Balance { get; private set; }
        public DateTime TimeOfSigningContract { get; private set; }
        public DateTime TimeOfChangingTariff { get; private set; }
        public List<ICallInformation> AllCalls { get; private set; }
        private int _daysInMonth = 30;

        public event EventHandler<CantChangeTariffEventArgs> CantChangeTariffEvent;
        public event EventHandler<BalanceEventArgs> DebtRepaidEvent;
        public event EventHandler<SendHistoryEventArgs> SendHistoryEvent;
        public event EventHandler<BalanceEventArgs> SendBalanceEvent;

        protected virtual void OnSendBalanceEvent(BalanceEventArgs e)
        {
            SendBalanceEvent?.Invoke(this, e);
        }

        protected virtual void OnSendHistoryEvent(SendHistoryEventArgs e)
        {
            SendHistoryEvent?.Invoke(this, e);
        }

        protected virtual void OnDebtRepaidEvent(BalanceEventArgs e)
        {
            DebtRepaidEvent?.Invoke(this, e);
        }

        protected virtual void OnCantChangeTariffEvent(CantChangeTariffEventArgs e)
        {
            CantChangeTariffEvent?.Invoke(this, e);
        }

        public void SendBalance(BalanceEventArgs e)
        {
            OnSendBalanceEvent(new BalanceEventArgs(e.IdOfPort, Balance));
        }

        public void SendHistory(GetHistoryEventArgs e)
        {
            OnSendHistoryEvent(new SendHistoryEventArgs(e.IdOfPort, e.Number, _findMonthHistory()));
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            if (e.TimeOfChanging.Subtract(TimeOfChangingTariff).TotalSeconds <= _daysInMonth)
            {
                CantChangeTariffPlan(new CantChangeTariffEventArgs(e.IdOfPort, TimeOfChangingTariff));
            }
            else
            {
                Tariff = e.NewTariffPlan;
                TimeOfChangingTariff = e.TimeOfChanging;
            }
        }

        public void HandleMoneyEvent(object o, BalanceEventArgs e)
        {
            if (Balance < 0)
            {
                Balance += e.Money;
                if (Balance >= 0) OnDebtRepaidEvent(new BalanceEventArgs(e.IdOfPort));
            }
            else Balance += e.Money;
        }

        public void CantChangeTariffPlan(CantChangeTariffEventArgs e)
        {
            OnCantChangeTariffEvent(e);
        }

        public void SetBalnceAfterCall(double money)
        {
            Balance -= money;
        }

        private List<ICallInformation> _findMonthHistory()
        {
            List<ICallInformation> resultList = new List<ICallInformation>();
            var finding = AllCalls.Where(x => DateTime.Now.Subtract(x.BeginningOfCall).TotalDays <= _daysInMonth).Select(i => i);
            foreach (var item in finding)
            {
                resultList.Add(item);
            }
            return resultList;
        }

        public Contract(int id, int idOfPort, string number, string name, ITariffPlan tariffPlan)
        {
            IdOfContract = id;
            IdOfPort = idOfPort;
            Number = number;
            Tariff = tariffPlan;
            Name = name;
            TimeOfSigningContract = DateTime.Now;
            TimeOfChangingTariff = TimeOfSigningContract;
            AllCalls = new List<ICallInformation>();
            Balance = 0.0;
        }
    }
}
