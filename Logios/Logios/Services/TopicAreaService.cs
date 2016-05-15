using Logios.Entities;
using Logios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class TopicAreaService
    {
        public TopicAreaService()
        {
        }

        public TopicAreaViewModel GetAll()
        {
            var result = new TopicAreaViewModel();

            using (var context = new ApplicationDbContext())
            {
                var topicAreas = context.TopicAreas.ToList();
                var topicAreaTopics = context.TopicAreaTopics.ToList();
                var topics = context.Topics.ToList();

                foreach (var topicArea in topicAreas)
                {
                    var topicsForThisArea = topicAreaTopics
                                               .Where(tat => tat.TopicAreaId == topicArea.TopicAreaId)
                                               .Select(tat => tat.Topic);

                    var topicAreaName = topicArea.Description;

                    result.TopicsByArea.Add(topicAreaName, topicsForThisArea);
                }
            }

            return result;
        }
    }
}