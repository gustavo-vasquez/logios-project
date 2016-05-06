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
        private ApplicationDbContext context = new ApplicationDbContext();

        public Exercise GetExercise(int? id)
        {
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
                        
            return exercise;
        }

        public bool CheckAnswer(int id, string answer)
        {
            var solution = context.Exercises.FirstOrDefault(e => e.ExerciseId == id).Solution;

            if(answer.Replace('"', '\'') != solution)            
                return false;            
            else            
                return true;                     
        }

        public IEnumerable<Exercise> GetExercisesByTopic(int topicId)
        {
            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topicId)
                                   .ToList();
            return exercises;
        }

        public bool CheckUserAlreadyResolved(string UserId, int id)
        {
            var isDeveloped = context.UserExercise.Any(e => e.UserId == UserId && e.ExerciseId == id && e.ShowedSolution == true);

            return isDeveloped;
        }

        public void UpdateUserExercise(string UserId, int id)
        {
            UserExercise UserExercise = new UserExercise();

            UserExercise.UserId = UserId;
            UserExercise.ExerciseId = id;
            UserExercise.ShowedSolution = true;
            UserExercise.SolvedDate = DateTime.Now;

            context.SaveChanges();
        }
    }
}