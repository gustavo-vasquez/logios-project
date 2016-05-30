using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logios.Helpers.Trophy
{
    public interface ITrophyCondition
    {
        bool CheckCondition(string userId);
    }
}
