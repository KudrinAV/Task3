using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;

namespace Classes.Ports
{
    public class ConnectedPort : IConnectedPort
    {
        public string Number { get; private set; }

        public int Id { get; private set; }

        public int GetIdOfTerminal()
        {
            return 0;
        }

        public ConnectedPort(string numberOfTerminal, int id)
        {
            Number = numberOfTerminal;
            Id = id;
        }
    }
}
