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
using Logios.DTOs;

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

        public ResultModalViewModel CheckAnswer(int id, string answer)
        {
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);
            
            if (exercise == null)
            {
                throw new ArgumentException(string.Format("No se encontró un ejercicio con Id = {0}", id));
            }

            var userExercise = context.UserExercise.FirstOrDefault(e => e.ExerciseId == exercise.ExerciseId);
            var showed = userExercise == null ? false : userExercise.ShowedSolution;
            var alreadyDone = userExercise != null;

            var mathJaxSpace = @"<mo>&#x000A0;</mo>";
            var parsedAnswer = answer.Replace(mathJaxSpace, string.Empty).Trim();
            var parsedSolution = exercise.Solution.Replace(mathJaxSpace, string.Empty).Trim();

            var success = parsedAnswer.EqualsIgnoreCase(parsedSolution);
            
            return new ResultModalViewModel()
            {
                Success = success,
                ShowedAnswer = showed,
                AlreadyResolved = alreadyDone
            };
        }

        public ExerciseViewModel GetExerciseInformation(int? id, string userId)
        {
            this.Refresh();
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseId == id);

            if (exercise == null || exercise.IsDeleted == true)
            {
                return new ExerciseViewModel();
            }

            var allExercises = context.Exercises.Where(a => a.IsDeleted == false && a.Topic.Description == exercise.Topic.Description).ToList();
            var index = allExercises.IndexOf(exercise);

            ExerciseViewModel exerciseToShow = new ExerciseViewModel();
            exerciseToShow.Exercise = exercise;
            exerciseToShow.topicName = context.Topics.FirstOrDefault(t => t.TopicId == exercise.Topic.TopicId).Description;
            exerciseToShow.isFirst = (index == 0);
            exerciseToShow.isLast = (allExercises.Count() > 0 && index == allExercises.Count() - 1);
            exerciseToShow.backExerciseId = (index > 0) ? allExercises[index - 1].ExerciseId : id;
            exerciseToShow.nextExerciseId = (index < allExercises.Count() - 1) ? allExercises[index + 1].ExerciseId : id;
            exerciseToShow.isResolved = context.UserExercise.Any(x => x.ExerciseId == id && x.UserId == userId);
            exerciseToShow.maxExerciseId = context.Exercises.Where(e => e.ExerciseId != id && e.IsDeleted == false)
                                                            .OrderByDescending(e => e.ExerciseId)
                                                            .Select(e => e.ExerciseId)
                                                            .First();

            return exerciseToShow;
        }        

        public IEnumerable<Exercise> GetExercisesByTopic(int topicId)
        {
            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topicId && e.IsDeleted == false)
                                   .ToList();
            return exercises;
        }

        public IEnumerable<Exercise> GetExercisesByTopic(string topicDescription)
        {
            var topic = context.Topics.FirstOrDefault(x => x.Description == topicDescription);

            if (topic == null)
            {
                throw new ArgumentException(string.Format("A Topic with description '{0}' was not found", topicDescription));
            }

            var exercises = context.Exercises
                                   .Where(e => e.Topic.TopicId == topic.TopicId)
                                   .ToList();
            return exercises;
        }

        public IEnumerable<ExerciseDTO> GetExerciseDTOsByTopic(string userId, int topicId)
        {
            var exercises = this.GetExercisesByTopic(topicId);

            var exercisesDTOs = exercises
                                    .Select(x => new ExerciseDTO
                                    {
                                        ExerciseId = x.ExerciseId,
                                        Description = x.Description,
                                        Problem = x.Problem,
                                        Solution = x.Solution,
                                        Development = x.Development
                                    })
                                    .ToList();

            var userExercises = context.UserExercise.Where(x => x.UserId == userId);

            exercisesDTOs.ForEach(x => x.Resolved = userExercises.Any(y => y.ExerciseId == x.ExerciseId));

            return exercisesDTOs;
        }

        public IEnumerable<ExerciseDTO> GetExerciseDTOsCards(string userId, int topicId, bool showResolvedExercises)
        {
            var exercises = this.GetExercisesByTopic(topicId);
            var userExercises = context.UserExercise.Where(x => x.UserId == userId);
            var userFavorites = context.UserFavorites.Where(x => x.UserId == userId);

            if (!showResolvedExercises)
            {
                List<ExerciseDTO> exercisesDTOs = new List<ExerciseDTO>();

                foreach (var item in exercises)
                {
                    if (!userExercises.Any(x => x.ExerciseId == item.ExerciseId && x.UserId == userId))
                    {
                        exercisesDTOs.Add(new ExerciseDTO
                        {
                            ExerciseId = item.ExerciseId,
                            Description = item.Description,
                            Problem = item.Problem,
                            Solution = item.Solution,
                            Development = item.Development,
                            Resolved = false
                        });
                    }
                }

                exercisesDTOs.ForEach(x => x.Favorited = userFavorites.Any(y => y.ExerciseId == x.ExerciseId));
                return exercisesDTOs;
            }
            else
            {                
                var exercisesDTOs = exercises
                                        .Select(x => new ExerciseDTO
                                        {
                                            ExerciseId = x.ExerciseId,
                                            Description = x.Description,
                                            Problem = x.Problem,
                                            Solution = x.Solution,
                                            Development = x.Development
                                        })
                                        .ToList();

                exercisesDTOs.ForEach(x => x.Resolved = userExercises.Any(y => y.ExerciseId == x.ExerciseId));
                exercisesDTOs.ForEach(x => x.Favorited = userFavorites.Any(y => y.ExerciseId == x.ExerciseId));

                return exercisesDTOs;
            }
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

        public IEnumerable<int> GetExercisesResolved(string userId, string topicName)
        {
            List<int> tree = new List<int>();
            List<int> allResolvedIds = context.UserExercise.Where(x => x.UserId == userId).Select(x => x.ExerciseId).ToList();
            var topicId = context.Topics.FirstOrDefault(t => t.Description == topicName).TopicId;
            var exercises = context.Exercises;

            foreach(var exerciseId in allResolvedIds)
            {
                if (exercises.Any(e => e.ExerciseId == exerciseId && e.Topic.TopicId == topicId))
                {
                    tree.Add(exerciseId);
                }
            }

            return tree;
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
                var fromAdress = "team.logios.project@gmail.com";
                const string fromPassword = "logios2016";

                var toAdress = context.Users.FirstOrDefault(u => u.UserName == uploadName).Email;
                var userName = context.Users.FirstOrDefault(u => u.Id == userId).UserName;                

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromAdress, "Logios");
                mailMessage.To.Add(toAdress);
                mailMessage.Subject = "Un usuario ha reportado un ejercicio - Logios";
                //mailMessage.Body = "El usuario " + userName + " ha enviado un reporte para el ejercicio Nº" + exerciseId + ".\n\nCausa del reporte: " + cause;
                mailMessage.Body = "<div style=\"border:1px solid #8E8E8E;\"><div style=\"background-color:darkblue;text-align:center;font-size:30px;padding:10px;\"><label style=\"color:white; background-color: darkblue;\"><b>El usuario " + userName + " ha enviado un reporte para el ejercicio Nº " + exerciseId + "</b></label></div><div style=\"text-align: center; padding: 25px;\"><h3 style=\"color: #317eac; font-size: 26px;\"><b>Causa del reporte:</b></h3><h3 style=\"font-size: 24px;color:#4C4C4C;\"><b>" + cause + "</b></h3></div><p style=\"padding-left:20px;\">&copy; " + DateTime.Now.Year + " Logios</p></div>";
                mailMessage.IsBodyHtml = true;

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