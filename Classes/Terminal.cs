using Classes.CustomArgs;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Terminal : ITerminal
    {
        event EventHandler<CallArgs> Call;

        protected virtual void OnCall(CallArgs e, string number)
        {
            EventHandler<CallArgs> handler = Call;
            if (handler != null)
            {
                e = new CallArgs(number);
            }
        }
    }
}
