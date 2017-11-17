using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITerminal
    {
        int IdOfPort { get; }
        int Number { get; }
        bool OnCall { get; }
        bool NotAvalibale { get; }


        bool Call();
        void Abort();

    }
}
