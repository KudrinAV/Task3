using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Environment
{
    public class EmulationEnvironment : IEmulationEnvironment
    {
        public IAPS Aps { get; private set; }
        public List<IUser> Users { get; private set; }
        public List<ITerminal> Telephones { get; private set; }
    }
}
