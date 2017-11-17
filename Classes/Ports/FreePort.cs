using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Ports
{
    public class FreePort : IFreePort
    {
        public string Number { get; private set; }
        public int Id { get; private set; }
    }
}
