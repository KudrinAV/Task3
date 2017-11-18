using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.CustomArgs
{
    class CallToArgs : EventArgs
    {
        CallArgs e;
        string CallerNumber;
        string Balance;

        public CallToArgs(CallArgs eImp, string callerNumber, string balance)
        {
            e = eImp;
            CallerNumber = callerNumber;
            Balance = balance;
        }
    }
}
