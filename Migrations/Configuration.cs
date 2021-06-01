namespace press_agency_asp_webapp.Migrations
{
    using press_agency_asp_webapp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Data.SqlTypes;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<press_agency_asp_webapp.Models.CodeFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(press_agency_asp_webapp.Models.CodeFContext context)
        //{
        //    try
        //    {
        //        //PostType postType = new Models.PostType { Name = "admin" };
        //        //context.PostTypes.Add(postType);

        //        ////Editor editor = new Editor
        //        ////{
        //        ////    FirstName = "Mahmoud",
        //        ////    LastName = "Basiony",
        //        ////    Password = "1234    ",
        //        ////    Email = "Basiony@gmail.com",
        //        ////    Phone = "11111111111",
        //        ////    UserTypeId = 2,
        //        ////    Username = "Basiony",
        //        ////    CreateDate = DateTime.Now
        //        ////};
        //        ////context.Actors.Add(editor);

        //        //context.Posts.Add(new Post
        //        //{
        //        //    Title = "title",
        //        //    Body = "Body",
        //        //    No_views = 0,
        //        //    No_dislikes = 0,
        //        //    No_likes = 0,
        //        //    CreateDate = DateTime.Now,
        //        //    State = true,
        //        //    PostType = postType,
        //        //    EditorId = 6
        //        //});

        //        context.Questions.Add(new Question
        //        {
        //            Title = "how the fuck?",
        //            Answer = "i dont know the fuck",
        //            CreateDate = DateTime.Now,
        //            PostId = 2,
        //            ViewerId = 4,
        //        });
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }
        //}
    }
}
