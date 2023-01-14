using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataAccessLayer.EntityFrameworkSQL
{
    public class MyDbInitializer : CreateDatabaseIfNotExists<BlogContext>


    {
        protected override void Seed(BlogContext context)
        {

            BlogUser admin = new BlogUser()
            {
                Name = "Admin",
                Surname = "Admin",
                UserProfileImage = "User-Profile.jfif",
                Email = "admin@mail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "Admin",
                Password = "123456",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUserName = "Admin"

            };

            BlogUser standartUser = new BlogUser()
            {
                Name = "Kaan",
                Surname = "Mol",
                UserProfileImage = "User-Profile.jfif",
                Email = "kaan@mail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "kaan",
                Password = "123456",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "kaan"
            };
            context.BlogUsers.Add(admin);
            context.BlogUsers.Add(standartUser);

            for (int i = 0; i < 10; i++)
            {
                BlogUser user = new BlogUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    UserProfileImage = "User-Profile.jfif",
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user-{i}",
                    Password = "123",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = $"user-{i}"
                };
                context.BlogUsers.Add(user);
            }
            context.SaveChanges();


            List<BlogUser> userList = context.BlogUsers.ToList();


            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetCountry(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "kaan"
                };
                context.Categories.Add(category);

                for (int j = 0; j < FakeData.NumberData.GetNumber(3, 15); j++)
                {
                    BlogUser user_note = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.PlaceData.GetCity(),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 4)),
                        Category = category,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 12),
                        CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedUserName = user_note.Username,
                        Owner = user_note
                    };

                    category.Notes.Add(note);

                    for (int k = 0; k < FakeData.NumberData.GetNumber(5, 15); k++)
                    {
                        BlogUser commentuser = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = commentuser,
                            CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedUserName = commentuser.Username

                        };
                        note.Comments.Add(comment);

                    }
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = user_note,

                        };
                        note.Likes.Add(liked);
                    }

                }

            }
            context.SaveChanges();
        }


    }
}
