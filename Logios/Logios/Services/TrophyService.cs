using Logios.Entities;
using Logios.Helpers.Trophy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class TrophyService
    {
        private IDictionary<string, ITrophyCondition> TrophyConditions;

        public TrophyService()
        {
            this.TrophyConditions = new Dictionary<string, ITrophyCondition>()
            {
                { "Primer Trofeo", new FirstExerciseResolved() }
            };
        }

        public void UpdateUserTrophies(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var trophies = context.Trophies;

                foreach (var trophy in trophies)
                {
                    var trophyName = trophy.Description;

                    if (!this.TrophyConditions.ContainsKey(trophyName))
                    {
                        throw new KeyNotFoundException(string.Format("No existe la condicion que verifica si el usuario debe obtener el trofeo: {0}", trophyName));
                    }

                    var condition = this.TrophyConditions[trophyName];
                    condition.CheckCondition(userId);
                }
            }
        }
    }
}