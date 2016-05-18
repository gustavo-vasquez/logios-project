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

            // Llenar con data falsa para ver como se muestran las cosas
            result.TopicsByArea["Tema 3"] = this.GenerateFakeTopics(4);
            result.TopicsByArea["Tema 4"] = this.GenerateFakeTopics(6);
            result.TopicsByArea["Tema 5"] = this.GenerateFakeTopics(7);
            result.TopicsByArea["Tema 6"] = this.GenerateFakeTopics(7);

            return result;
        }

        public ExercisesByAreaViewModel GetExercisesByTopicArea(string topicAreaDescription)
        {
            var result = new ExercisesByAreaViewModel();

            using (var context = new ApplicationDbContext())
            {
                var topicArea = context.TopicAreas.FirstOrDefault(ta => ta.Description == topicAreaDescription);

                var topicsForThisArea = context.TopicAreaTopics
                                                   .Where(tat => tat.TopicAreaId == topicArea.TopicAreaId)
                                                   .Select(tat => tat.Topic)
                                                   .ToList();

                result.Topics = topicsForThisArea;

                var exerciseResultViewModels = new List<ExerciseResultViewModel>();

                foreach (var topic in result.Topics)
                {
                    var newViewModel = new ExerciseResultViewModel();
                    newViewModel.Exercises = context.Exercises
                                                        .Where(e => e.Topic.TopicId == topic.TopicId)
                                                        .ToList();

                    newViewModel.TopicImageUrl = string.Concat("/Content/images/thumbnails/", topic.Description, ".png");

                    exerciseResultViewModels.Add(newViewModel);
                }

                result.ExerciseResultViewModels = exerciseResultViewModels;

                return result;
            }
        }

        // Generar topics de mentira para ver el Layout, sacar cuando tengamos el feedbackd e Juan
        private IEnumerable<Topic> GenerateFakeTopics(int topicCount)
        {
            var fakeTopics = new List<Topic>();

            for (int i = 0; i < topicCount; i++)
            {
                fakeTopics.Add(new Topic()
                {
                    TopicId = 1,
                    Description = string.Concat("Tema ", i)
                });
            }

            return fakeTopics;
        }
    }
}