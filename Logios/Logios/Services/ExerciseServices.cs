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
        
        public ExerciseViewModel GetExercise(int? id)
        {
            ExerciseViewModel exercise = new ExerciseViewModel();
            exercise.Exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
                        
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

        public ExerciseViewModel GetExerciseInformation(int? id)
        {
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
            var allExercises = context.Exercises.ToList();
            var index = allExercises.IndexOf(exercise);

            ExerciseViewModel exerciseToShow = new ExerciseViewModel();
            exerciseToShow.Exercise = exercise;
            exerciseToShow.isFirst = (index == 0);
            exerciseToShow.isLast = (allExercises.Count() > 0 && index == allExercises.Count() - 1);
            exerciseToShow.backExerciseId = (index > 0) ? allExercises[index - 1].ExerciseId : id;
            exerciseToShow.nextExerciseId = (index < allExercises.Count() - 1) ? allExercises[index + 1].ExerciseId : id;

            return exerciseToShow;
        }        

        public IEnumerable<Exercise> GetExercisesByTopic(int topicId)
        {
            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topicId)
                                   .ToList();
            return exercises;
        }

        public bool CheckUserAlreadyDeveloped(string UserId, int id)
        {
            var isDeveloped = context.UserExercise.Any(e => e.UserId == UserId && e.ExerciseId == id && e.ShowedSolution == true);

            return isDeveloped;
        }

        public bool CheckUserAlreadyResolved(string UserId, int id)
        {
            var isResolved = context.UserExercise.Any(e => e.UserId == UserId && e.ExerciseId == id && e.ShowedSolution == false);

            return isResolved;
        }

        public void UpdateUserExercise(string UserId, int id, bool showed)
        {
            UserExercise userExercise = new UserExercise();

            userExercise.UserId = UserId;
            userExercise.ExerciseId = id;
            userExercise.ShowedSolution = showed;
            userExercise.SolvedDate = DateTime.Now;

            context.UserExercise.Add(userExercise);
            context.SaveChanges();
        }

        public void SumPoints(string UserId)
        {
            ApplicationUser applicationUser = context.Users.Find(UserId);
            applicationUser.Points = +1;
            context.SaveChanges();
        }
    }
}