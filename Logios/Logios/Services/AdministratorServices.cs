using Logios.Entities;
using Logios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Services
{
    public class AdministratorServices
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<SelectListItem> GetAllTopics()
        {
            var topics = context.Topics.ToList();            

            return new SelectList(topics, "TopicId", "Description");
        }

        public Boolean? CreateNewExercise(CreateExerciseViewModel model)
        {
            try
            {
                Exercise exercise = new Exercise();
                exercise.ExerciseId = context.Exercises.Count() + 1;
                exercise.Problem = model.Exercise.Problem;
                exercise.Development = model.Exercise.Development;
                exercise.Solution = model.Exercise.Solution;
                exercise.Description = model.Exercise.Description;                
                exercise.Topic = context.Topics.FirstOrDefault(t => t.TopicId == model.Topic.TopicId);
                context.Exercises.Add(exercise);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}