﻿using Contracts.CustomArgs;
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
        public int IdOfPort { get; private set; }
        public string Number { get; private set; }
        public ITariffPlan Tariff { get; private set; }
        public double Balance { get; private set; }
        public DateTime TimeOfSigningContract { get; private set; }
        public DateTime TimeOfChangingTariff { get; private set; }

        public event EventHandler<ChangeTariffEventArgs> CantChangeTariffEvent;

        protected virtual void OnCantChangeTariffEvent(ChangeTariffEventArgs e)
        {
            CantChangeTariffEvent?.Invoke(this, e);
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            if (e.TimeOfChanging.Subtract(TimeOfChangingTariff).TotalDays <= 30)
            {
                e.SetNewTime(TimeOfChangingTariff);
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
            Balance += e.Money;
        }

        public void CantChangeTariffPlan(ChangeTariffEventArgs e)
        {
            OnCantChangeTariffEvent(e);
        }

        public void HandleCostOfCall(object o, EndCallEventArgs e)
        {
            Balance -= e.DurationOfCall.TotalSeconds * Tariff.CostOfCall;
        }

        public Contract(int id , string number, ITariffPlan tariffPlan)
        {
            IdOfPort = id;
            Number = number;
            Tariff = tariffPlan;
            TimeOfSigningContract = DateTime.Now;
            TimeOfChangingTariff = TimeOfSigningContract;
            Balance = 0.0;
        }
  
    }
}
