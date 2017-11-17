using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class APS : IAPS
    {
        public List<IConnectedPort> ConnectedPorts { get; private set; }

        public List<IFreePort> FreePorts { get; private set; }
    }
}
