using Logios.DTOs;
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

        public IEnumerable<TopicDTO> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var topics = context.Topics.ToList();
                var result = new List<TopicDTO>();
                topics.ForEach(t => result.Add(new TopicDTO { TopicId = t.TopicId, Description = t.Description }));

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