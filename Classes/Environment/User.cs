using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Environment
{
    public class User : IUser
    {
        public string Name { get; private set; }
        public ITerminal Telephone { get; private set; }
        public IPort Port { get; private set; }

        public User(string name, ITerminal terminal)
        {
            Name = name;
            Telephone = terminal;
        }

        public void ConnectPortToTerminal()
        {
            if(Port != null)
            {
                Telephone.ConnectToPort(Port);
            }
        }

        public void DisconnectFromPort()
        {
            if (Port != null)
            {
                Telephone.DissconnectFromPort();
            }
        }

        public void SignAContract(IAPS aps, ITariffPlan plan)
        {
            Port = aps.SignAContract(plan, Name);
        }

        public void TermintaeContract(IAPS aps)
        {
            if(Port != null)
            {
                aps.TerminateContract(Port);
                Port = null;
            }
        }

        public void Call(string number)
        {
            Telephone.Call(number);
        }

        public void GetHistory()
        {
            Telephone.GetHistory();
        }

        public void GetBalance()
        {
            Telephone.GetBalance();
        }

        public void PunOnBalance(double money)
        {
            Telephone.PutMoney(money);
        }

        public void EndCall()
        {
            Telephone.EndCall();
        }
    }
}
