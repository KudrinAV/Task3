﻿using Contracts.CustomArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITerminal
    {
        int Id { get; }

        void GetHistory();
        void ChangeTariff(ITariffPlan tariffPlan);
        void EndCall();
        string GetNumber();
        void PutMoney(double money);
        void Call(string number);
        void ConnectToPort(IPort port);
        void DissconnectFromPort();
        void GetBalance();
        IPort GetPort();
    }
}
