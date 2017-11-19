using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ILoneDiggerTariffPlan
    {
        int IdOfPort { get; }
        ITariffPlan Tariff { get; }
    }
}
