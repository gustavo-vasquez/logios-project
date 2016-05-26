using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Models;
using Logios.Entities;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Logios.Extensions;
using System.Net.Mail;
using System.Net;

namespace Logios.Services
{
    public class ExerciseServices
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private void Refresh()
        {
            this.context.Dispose();
            this.context = new ApplicationDbContext();
        }
        
        public ExerciseViewModel GetExercise(int? id)
        {
            ExerciseViewModel exercise = new ExerciseViewModel();
            exercise.Exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
                        
            return exercise;
        }

        public bool CheckAnswer(int id, string answer)
        {
            var solution = context.Exercises.FirstOrDefault(e => e.ExerciseId == id).Solution;            

            if(answer.ToLower().Replace("<mo>&#x000a0;</mo>", "") != solution.ToLower().Replace("<mo>&#x000a0;</mo>", ""))
                return false;            
            else            
                return true;                     
        }

        public ExerciseViewModel GetExerciseInformation(int? id)
        {
            this.Refresh();
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
            var allExercises = context.Exercises.ToList();
            var index = allExercises.IndexOf(exercise);

            ExerciseViewModel exerciseToShow = new ExerciseViewModel();
            exerciseToShow.Exercise = exercise;
            exerciseToShow.topicName = context.Topics.FirstOrDefault(t => t.TopicId == exercise.Topic.TopicId).Description;
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

        public IEnumerable<Exercise> GetExercisesByTopic(string topicDescription)
        {
            var topicId = context.Topics
                                    .FirstOrDefault(x => topicDescription.EqualsIgnoreCase(x.Description))
                                    .TopicId;

            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topicId)
                                   .ToList();
            return exercises;
        }

        public bool CheckUserHasRecord(string userId,int id)
        {
            var isRecorded = context.UserExercise.Any(e => e.UserId == userId && e.ExerciseId == id);
            return isRecorded;
        }

        public void UpdateUserExercise(string userId, int id, bool showed)
        {
            UserExercise userExercise = new UserExercise();

            if (CheckUserHasRecord(userId, id))
            {
                return;
            }

            userExercise.UserId = userId;
            userExercise.ExerciseId = id;
            userExercise.ShowedSolution = showed;
            userExercise.SolvedDate = DateTime.Now;

            context.UserExercise.Add(userExercise);
            context.SaveChanges();
        }

        public void SumPoints(string userId)
        {
            
            var userProfile = context.UserProfiles.Find(userId);

            userProfile.Points += 1;
            
            context.SaveChanges();
        }

        public ReportViewModel GetReportData(int id)
        {
            ReportViewModel model = new ReportViewModel();
            model.Exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);

            return model;
        }

        public void SendReport(string cause, int exerciseId, string uploadName, string userId)
        {
            try
            {
                var fromAdress = "team.logios.proyect@gmail.com";
                const string fromPassword = "logios2016";

                var toAdress = context.Users.FirstOrDefault(u => u.UserName == uploadName).Email;
                var userName = context.Users.FirstOrDefault(u => u.Id == userId).UserName;                

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromAdress, "Logios");
                mailMessage.To.Add(toAdress);
                mailMessage.Subject = "Un usuario ha reportado un ejercicio - Logios";
                mailMessage.Body = "El usuario " + userName + " ha enviado un reporte para el ejercicio Nº" + exerciseId + ".\n\nCausa del reporte: " + cause;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(fromAdress, fromPassword);
                client.Timeout = 20000;

                client.Send(mailMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}