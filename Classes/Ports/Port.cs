using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.Interfaces;

namespace Classes.Ports
{
    public class Port : IPort
    {
        public string Number { get; private set; }

        public int Id { get; private set; }

        public StatusOfPort PortStatus { get; private set; }

        public int GetIdOfTerminal()
        {
            return 0;
        }
        
        public void ChangeStatus()
        {
            PortStatus = StatusOfPort.Free == PortStatus ? StatusOfPort.Connected : StatusOfPort.Free;
        }

        public Port(string numberOfTerminal, int id)
        {
            Number = numberOfTerminal;
            Id = id;
            PortStatus = StatusOfPort.Free;
        }
    }
}
