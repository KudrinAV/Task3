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
        public int Id { get; private set; }

        public int IdOfPort { get; private set; }

        public void GonnectToPort(int id)
        {
            IdOfPort = id;
        }

        public bool GetStatus()
        {
            return true;
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public bool Call()
        {
            throw new NotImplementedException();
        }

        public Terminal(int id)
        {

        }
    }
}
