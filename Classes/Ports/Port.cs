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

        private IAPS _aps {get; set;}

        public void GetAPS(IAPS aps)
        {
            _aps = aps;
        }

        public StatusOfPort PortStatus { get; private set; }

        public StatusOfCall CallStatus { get; private set; }



        public event Calling CallTheNubmer;

        public delegate StatusOfConnect Calling(IPort port, string number);

        public void ChangeCallStatus(StatusOfCall status)
        {
            CallStatus = status;
        }

        public void Call(string number)
        {
            CallTheNubmer += _aps.ConnectCall;
            CallTheNubmer(this, number);
        }

        public int GetIdOfTerminal()
        {
            return Id;
        }
        
        public void ChangeStatus()
        {
            PortStatus = StatusOfPort.NotConnected == PortStatus ? StatusOfPort.Connected : StatusOfPort.NotConnected;
        }

        public Port(string numberOfTerminal, int id)
        {
            Number = numberOfTerminal;
            Id = id;
            //CallTheNubmer += _aps.ConnectCall;
            PortStatus = StatusOfPort.NotConnected;
            CallStatus = StatusOfCall.NotAvalibale;
        }
    }
}
