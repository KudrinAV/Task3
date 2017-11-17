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

        public IPort Port { get; private set; }

        public void ConnectToPort(IPort port)
        {
            Port = port;
            Port.ChangeStatus();
        }

        public void DissconnectFromPort()
        {
            Port.ChangeStatus();
            Port = null; 
        }

        public void ChangePorts(IPort port)
        {
            DissconnectFromPort();
            ConnectToPort(port);
        }

        public bool GetStatus()
        {
            return true;
        }

        public void SeeTheNumber()
        {
            Console.WriteLine(Port.Number);
        }

        public void SeeBalance()
        {

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
            Id = id;
        }
    }
}
