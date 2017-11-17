﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITerminal
    {
        int Id { get; }
        int IdOfPort { get; }

        void ConnectToPort(int id);
        bool GetStatus();
        bool Call();
        void Abort();
    }
}
