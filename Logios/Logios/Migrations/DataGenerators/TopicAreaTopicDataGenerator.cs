using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Migrations.DataGenerators
{
    public class TopicAreaTopicDataGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
        {            
            var topic1 = context.Topics.FirstOrDefault(x => x.Description == "Polinomios");            
            var topicArea1 = context.TopicAreas.FirstOrDefault(x => x.Description == "Algebra");            

            var newTopicAreaTopics = new List<TopicAreaTopic>()
            {
                new TopicAreaTopic()
                {
                    TopicId = topic1.TopicId,
                    Topic = topic1,
                    TopicArea = topicArea1,
                    TopicAreaId = topicArea1.TopicAreaId
                }                
            };

            newTopicAreaTopics.ForEach(t => context.TopicAreaTopics.Add(t));
            context.SaveChanges();
        }
    }
}