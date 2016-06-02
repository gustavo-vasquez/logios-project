using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Helpers.Trophy
{
    public class FirstExerciseResolved : ITrophyCondition
    {
        private const string TrophyDescription = "Primer Trofeo";

        public bool CheckCondition(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var hasResolvedAnExercise = context.UserExercise.Any(x => x.UserId == userId);
                var doesntHaveTheTrophy = !context.UserTrophies
                                                      .Any(x => x.Trophy.Description == TrophyDescription
                                                             && x.UserId == userId);

                if (hasResolvedAnExercise && doesntHaveTheTrophy)
                {
                    var trophy = context.Trophies.FirstOrDefault(x => x.Description == TrophyDescription);

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