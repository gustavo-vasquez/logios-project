using Logios.Entities;
using Logios.Enums;
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
            using (var context = new ApplicationDbContext())
            {
                var hasResolvedFiveExercises = (context.UserExercise.Where(x => x.UserId == userId
                                                                       && x.ShowedSolution == false).Count() == 5);
                var doesntHaveTheTrophy = !context.UserTrophies
                                                      .Any(x => x.Trophy.Description == Trophies.EruditoAvanzado
                                                             && x.UserId == userId);

                if (hasResolvedFiveExercises && doesntHaveTheTrophy)
                {
                    var trophy = context.Trophies.FirstOrDefault(x => x.Description == Trophies.EruditoAvanzado);

                    context.UserTrophies.Add(new UserTrophy()
                    {
                        UserId = userId,
                        TrophyId = trophy.TrophyId
                    });

                    context.SaveChanges();

                    return true;
                }

                return false;
            }
        }
    }
}