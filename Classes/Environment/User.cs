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
        private IPort _port { get; set; }

        public User(string name, ITerminal terminal)
        {
            Name = name;
            Telephone = terminal;
        }

        public IPort GetPort()
        {
            if (_port != null)
                return _port;
            else
            {
                Console.WriteLine("There is no port");
                return null;
            }
        }

        public void ConnectPortToTerminal()
        {
            if (_port != null)
            {
                Telephone.ConnectToPort(_port);
            }
        }

        public void DisconnectFromPort()
        {
            if (_port != null)
            {
                Telephone.DissconnectFromPort();
            }
        }

        public void SignAContract(IAPS aps, ITariffPlan plan)
        {
            if (_port == null)
            {
                _port = aps.SignAContract(plan, Name);
            }
        }

        public void TermintaeContract(IAPS aps)
        {
            if (_port != null)
            {
                aps.TerminateContract(_port);
                _port = null;
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

        public void ChangeTariff(ITariffPlan tariffPlan)
        {
            Telephone.ChangeTariff(tariffPlan);
        }

        public string GetNumber()
        {
            return Telephone.GetNumber();
        }
    }
}
