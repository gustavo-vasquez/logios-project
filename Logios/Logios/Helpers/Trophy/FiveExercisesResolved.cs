using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Helpers.Trophy
{
    public class FiveExercisesResolved : ITrophyCondition
    {
        public bool CheckCondition(string userId)
        {
            return false;
        }
    }
}