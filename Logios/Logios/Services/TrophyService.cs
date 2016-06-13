using Logios.Entities;
using Logios.Enums;
using Logios.Helpers.Trophy;
using Logios.Models;
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
                { Trophies.EruditoNovato, new FirstExerciseResolved() },
                { Trophies.EruditoAvanzado, new FiveExercisesResolved() },
                { Trophies.EruditoExperto, new TenExercisesResolved() }
            };
        }

        public Trophy UpdateUserTrophies(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                Trophy newTrophy = null;
                var trophies = context.Trophies;

                foreach (var trophy in trophies)
                {
                    var trophyName = trophy.Description;

                    if (!this.TrophyConditions.ContainsKey(trophyName))
                    {
                        throw new KeyNotFoundException(string.Format("No existe la condicion que verifica si el usuario debe obtener el trofeo: {0}", trophyName));
                    }

                    var condition = this.TrophyConditions[trophyName];
                    var obtainedTrophy = condition.CheckCondition(userId);

                    if (obtainedTrophy && newTrophy == null)
                    {
                        newTrophy = trophy;
                    }
                }

                return newTrophy;
            }
        }
        
        public IEnumerable<Trophy> GetAllTrophies()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Trophies.ToList();
            }
        }

        public IEnumerable<int> GetTrophiesWon(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var trophiesIds = context.UserTrophies.Where(x => x.UserId == userId).ToList();
                List<int> trophiesWonList = new List<int>();

                foreach (var t in trophiesIds)
                {
                    trophiesWonList.Add(t.TrophyId);
                }

                return trophiesWonList;
            }
        }
    }
}