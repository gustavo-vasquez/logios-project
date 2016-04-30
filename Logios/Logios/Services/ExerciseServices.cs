using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Models;
using Logios.Entities;

namespace Logios.Services
{
    public class ExerciseServices
    {
        private LogiosContext context = new LogiosContext();

        public Exercise GetExercise(int id)
        {
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
                        
            return exercise;
        }

        public bool CheckAnswer(int id, string answer)
        {
            var solution = context.Exercises.FirstOrDefault(e => e.ExerciseId == id).Solution;

            if(answer != solution)            
                return false;            
            else            
                return true;                     
        }

        public IEnumerable<Exercise> GetExercisesByTopic(int topicId)
        {
            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topicId)
                                   .ToList();

            if (exercises.Count == 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("No se encuentra un tema que corresponda con el TopicId {0}", topicId));
            }

            return exercises;
        }
    }
}