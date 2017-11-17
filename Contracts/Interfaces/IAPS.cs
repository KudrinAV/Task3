﻿using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IAPS
    {
        List<IPort> Ports { get; }

        StatusOfConnect ConnectCall(IPort port, string number);
    }
}
