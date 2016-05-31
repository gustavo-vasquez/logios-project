using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Models;
using Logios.Entities;
using Logios.DTOs;

namespace Logios.Services
{
    public class UserBadgesAndPointsServices
    {
        public IEnumerable<UserBadgesDTO> GetAllBadgesByUser(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var userTrophies = context.UserTrophies
                                                       .Where(x => x.UserId == userId)
                                                       .Select(x => x.Trophy)
                                                       .ToList();
                var result = new List<UserBadgesDTO>();

                userTrophies.ForEach(ut => result.Add(new UserBadgesDTO { IdTrophy = ut.TrophyId, DescriptionTrophy = ut.Description, ImageTrophy = ut.Image, TrophyPoints = ut.Points }));

                return result;
            }
        }

        public int GetPointsByUser(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.UserProfiles.Find(userId).Points;
                return result;
            }
        }
    }
}