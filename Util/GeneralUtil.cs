using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace press_agency_asp_webapp.Util
{
    public abstract class GeneralUtil
    {
        public static bool ISEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }


        public static bool CheckPassword(string hashedPass, string unHashedPass)
        {
            Debug.WriteLine(unHashedPass);
            return Crypto.HashPassword(unHashedPass) == hashedPass;

        }

        public static ICollection<QuestionViewModel> GetQustionViewModels(Post post)
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
            foreach (Question question in post.Questions)
            {
                questionViewModels.Add(new QuestionViewModel
                {
                    Id = question.Id,
                    PostId = question.PostId,
                    ViewerId = question.ViewerId,
                    Title = question.Title,
                    Answer = question.Answer,
                    CreateDate = question.CreateDate
                });
            }
            return questionViewModels;
        }

        public static string upload_image(string path, HttpPostedFileBase upload)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (upload != null)
            {
                DateTime time = DateTime.Now;
                string format = "HHmmss";
                string date = time.ToString(format);
                string name = Path.GetFileNameWithoutExtension(upload.FileName);
                var extension = Path.GetExtension(upload.FileName);
                string FullName = name + "_" + date + "_" + extension;
                upload.SaveAs(Path.Combine(path, FullName));
                return FullName;

            }
            return null;
        }
        public static void delete_Image(string path, string ImageName)
        {
            if (ImageName != null)
            {
                string p = Path.Combine(path, ImageName);
                System.IO.File.Delete(p);
            }

        }
    }
}