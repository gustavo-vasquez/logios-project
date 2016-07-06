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
            var topic2 = context.Topics.FirstOrDefault(x => x.Description == "Matrices");
            var topicArea1 = context.TopicAreas.FirstOrDefault(x => x.Description == "Algebra");

            var topic3 = context.Topics.FirstOrDefault(x => x.Description == "Tópico de Prueb A-A");
            var topic4 = context.Topics.FirstOrDefault(x => x.Description == "Tópico de Prueb A-B");
            var topicArea2 = context.TopicAreas.FirstOrDefault(x => x.Description == "Temática de Prueba A");

            var topic5 = context.Topics.FirstOrDefault(x => x.Description == "Tópico de Prueb B-A");
            var topic6 = context.Topics.FirstOrDefault(x => x.Description == "Tópico de Prueb B-B");
            var topicArea3 = context.TopicAreas.FirstOrDefault(x => x.Description == "Temática de Prueba B");

            var newTopicAreaTopics = new List<TopicAreaTopic>()
            {
                new TopicAreaTopic()
                {
                    TopicId = topic1.TopicId,
                    Topic = topic1,
                    TopicArea = topicArea1,
                    TopicAreaId = topicArea1.TopicAreaId
                },

                new TopicAreaTopic()
                {
                    TopicId = topic2.TopicId,
                    Topic = topic2,
                    TopicArea = topicArea1,
                    TopicAreaId = topicArea1.TopicAreaId
                },

                new TopicAreaTopic()
                {
                    TopicId = topic3.TopicId,
                    Topic = topic3,
                    TopicArea = topicArea2,
                    TopicAreaId = topicArea2.TopicAreaId
                },

                new TopicAreaTopic()
                {
                    TopicId = topic4.TopicId,
                    Topic = topic4,
                    TopicArea = topicArea2,
                    TopicAreaId = topicArea2.TopicAreaId
                },

                new TopicAreaTopic()
                {
                    TopicId = topic5.TopicId,
                    Topic = topic5,
                    TopicArea = topicArea3,
                    TopicAreaId = topicArea3.TopicAreaId
                },

                new TopicAreaTopic()
                {
                    TopicId = topic6.TopicId,
                    Topic = topic6,
                    TopicArea = topicArea3,
                    TopicAreaId = topicArea3.TopicAreaId
                }
                
            };

            newTopicAreaTopics.ForEach(t => context.TopicAreaTopics.Add(t));
            context.SaveChanges();
        }
    }
}