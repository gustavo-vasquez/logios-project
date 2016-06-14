using Logios.Entities;
using Logios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Helpers.Trophy
{
    public class TenExercisesResolved : ITrophyCondition
    {
        public bool CheckCondition(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var hasResolvedTenExercises = (context.UserExercise.Where(x => x.UserId == userId
                                                                       && x.ShowedSolution == false).Count() == 10);
                var doesntHaveTheTrophy = !context.UserTrophies
                                                      .Any(x => x.Trophy.Description == Trophies.EruditoExperto
                                                             && x.UserId == userId);

                if (hasResolvedTenExercises && doesntHaveTheTrophy)
                {
                    var trophy = context.Trophies.FirstOrDefault(x => x.Description == Trophies.EruditoExperto);

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