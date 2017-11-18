using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.CustomArgs
{
    public class CallArgs : EventArgs
    {
        public string ReceivingNumber { get; private set; }
    }
}
