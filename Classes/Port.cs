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

        event EventHandler<CallToArgs>
        
    }
}
