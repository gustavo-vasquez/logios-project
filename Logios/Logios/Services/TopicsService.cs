using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class TopicsService
    {
        public TopicsService()
        {
        }

        public IEnumerable<Topic> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Topics.Select(x => x);
                return result;
            }
        }

        public Topic GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Topics.Where(t => t.TopicId == id).FirstOrDefault();
                return result;
            }
        }
    }
}