using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmulationEnvironment
    {
        IAPS Aps { get; }
        List<IUser> Users { get; }
        List<ITerminal> Telephones { get; }
        ITariffPlan First { get;  }
        ITariffPlan Second { get; }

        void CreateTerminals(int number);
        void CreateUsers();
    }
}
