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

        public event EventHandler<ChangeTariffEventArgs> CantChangeTariffEvent;
        public event EventHandler<BalanceEventArgs> DebtRepaidEvent;

        protected virtual void OnDebtRepaidEvent(BalanceEventArgs e)
        {
            DebtRepaidEvent?.Invoke(this, e);
        }

        protected virtual void OnCantChangeTariffEvent(ChangeTariffEventArgs e)
        {
            CantChangeTariffEvent?.Invoke(this, e);
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            if (e.TimeOfChanging.Subtract(TimeOfChangingTariff).TotalDays <= _daysInMonth)
            {
                CantChangeTariffPlan(e);
            }
            else
            {
                Tariff = e.NewTariffPlan;
                TimeOfChangingTariff = e.TimeOfChanging;
            }
        }

        public void HandleMoney(object o, BalanceEventArgs e)
        {
            if (Balance < 0)
            {
                Balance += e.Money;
                if (Balance >= 0) OnDebtRepaidEvent(new BalanceEventArgs(e.IdOfPort));
            }
            else Balance += e.Money;
        }

        public void CantChangeTariffPlan(ChangeTariffEventArgs e)
        {
            OnCantChangeTariffEvent(e);
        }

        public void SetBalnceAfterCall(double money)
        {
            Balance -= money;
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
