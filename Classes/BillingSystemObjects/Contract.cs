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

        public ITariffPlan Tariff { get; private set; }

        public double Balance { get; private set; }

        public Contract(int id , ITariffPlan tariffPlan)
        {
            IdOfPort = id;
            Tariff = tariffPlan;
            Balance = 0.0;

        }

        public void HandleCostOfCall(object o, EndCallEventArgs e)
        {
            Balance -= e.EndedCall.GetDuretionOfCall().TotalSeconds * Tariff.CostOfCall();
            Console.WriteLine(Balance);
        }

        public void HandleMoney(object o, BalanceEventArgs e)
        {
            Console.WriteLine("Money" + e.Money);
            Balance += e.Money;
        }
    }
}
