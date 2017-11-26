using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        ITerminal Telephone { get; }

        void ConnectPortToTerminal();
        void DisconnectFromPort();
        void SignAContract(IAPS aps, ITariffPlan plan);
        void TermintaeContract(IAPS aps);
        void Call(string number);
        void GetBalance();
        void PunOnBalance(double money);
        void EndCall();
    }
}
