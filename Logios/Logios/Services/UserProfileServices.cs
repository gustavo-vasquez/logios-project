using Logios.Entities;
using Logios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class UserProfileServices
    {
        string defaultImage = "1465327785_profle.png";

        public string GetProfilePicture(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var image = context.UserProfiles.First(x => x.UserID == userId).ImagePath;
                    if(!String.IsNullOrEmpty(image))
                    {
                        return image;
                    }

                    throw new ArgumentNullException();
                }
                catch
                {
                    return defaultImage;
                }                                
            }                
        }

        public string UploadAvatar(RegisterViewModel registerData, HttpServerUtilityBase server)
        {
            HttpPostedFileBase picture = registerData.ImageProfile;

            // Verifica que el usuario ha elegido un archivo
            if (picture != null && picture.ContentLength > 0)
            {
                // Obtiene información de la imagen
                var fileName = Path.GetFileName(picture.FileName);
                //var contentLength = picture.ContentLength;
                //var contentType = picture.ContentType;

                // Obtiene datos del archivo
                byte[] data = new byte[] { };
                using (var binaryReader = new BinaryReader(picture.InputStream))
                {
                    data = binaryReader.ReadBytes(picture.ContentLength);
                }

                // Guarda imagen en el servidor
                string pathToSave = server.MapPath("~/Content/images/avatars/") + registerData.UserName + Path.GetExtension(fileName);
                using (FileStream image = System.IO.File.Create(pathToSave, data.Length))
                {
                    image.Write(data, 0, data.Length);
                }

                return registerData.UserName + Path.GetExtension(picture.FileName);
            }

            return defaultImage;
        }

        public void EditAvatar(string userId, HttpPostedFileBase profilePicture, HttpServerUtilityBase server)
        {            
            using (var context = new ApplicationDbContext())
            {
                string userName = context.Users.FirstOrDefault(u => u.Id == userId).UserName;
                UserProfile userProfile = new UserProfile();

                // Verifica que el usuario ha elegido un archivo
                if (profilePicture != null && profilePicture.ContentLength > 0)
                {
                    // Obtiene información de la imagen
                    var fileName = Path.GetFileName(profilePicture.FileName);
                    //var contentLength = picture.ContentLength;
                    //var contentType = picture.ContentType;

                    // Obtiene datos del archivo
                    byte[] data = new byte[] { };
                    using (var binaryReader = new BinaryReader(profilePicture.InputStream))
                    {
                        data = binaryReader.ReadBytes(profilePicture.ContentLength);
                    }

                    // Guarda imagen en el servidor
                    string pathToSave = server.MapPath("~/Content/images/avatars/") + userName + Path.GetExtension(fileName);
                    using (FileStream image = System.IO.File.Create(pathToSave, data.Length))
                    {
                        image.Write(data, 0, data.Length);
                    }

                    userProfile = context.UserProfiles.FirstOrDefault(x => x.UserID == userId);
                    userProfile.ImagePath = userName + Path.GetExtension(profilePicture.FileName);
                    context.SaveChanges();
                }
                else
                {
                    userProfile = context.UserProfiles.FirstOrDefault(x => x.UserID == userId);
                    userProfile.ImagePath = defaultImage;
                    context.SaveChanges();
                }                                                
            }
        }

        public void ResetAvatar(string userId, HttpServerUtilityBase server)
        {
            using (var context = new ApplicationDbContext())
            {
                var userProfile = context.UserProfiles.First(x => x.UserID == userId);
                if(!String.IsNullOrEmpty(userProfile.ImagePath))
                {
                    File.Delete(server.MapPath("~/Content/images/avatars/") + userProfile.ImagePath);
                    userProfile.ImagePath = defaultImage;
                    context.SaveChanges();
                }                
            }
        }
    }
}