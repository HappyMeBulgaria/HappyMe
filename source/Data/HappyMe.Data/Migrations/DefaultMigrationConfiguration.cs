namespace HappyMe.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Hosting;

    using HappyMe.Common.Constants;
    using HappyMe.Common.Models;
    using HappyMe.Data;
    using HappyMe.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class DefaultMigrationConfiguration : DbMigrationsConfiguration<HappyMeDbContext>
    {
        public DefaultMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HappyMeDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUser(context);
            this.SeedOther(context);
        }

        private void SeedUser(HappyMeDbContext context)
        {
            if (!context.Users.Any())
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                var user1 = new User
                {
                    CreatedOn = DateTime.Now,
                    UserName = "superUser@fake.com",
                    Email = "superUser@fake.com",
                };

                var userCreateResult1 = userManager.Create(user1, ConfigurationManager.AppSettings["superUserPass"]);
                if (userCreateResult1.Succeeded)
                {
                    context.Users.AddOrUpdate(user1);
                }
                else
                {
                    throw new Exception(string.Join("; ", userCreateResult1.Errors));
                }

                context.SaveChanges();

                var roleId =
                    context.Roles.Where(r => r.Name == RoleConstants.Administrator).Select(r => r.Id).FirstOrDefault();
                var userInRole = new IdentityUserRole { UserId = user1.Id, RoleId = roleId };
                user1.Roles.Add(userInRole);

                context.SaveChanges();
            }
        }

        private void SeedRoles(HappyMeDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleNames = new[]
                {
                    RoleConstants.Administrator,
                    RoleConstants.Parent,
                    RoleConstants.Child
                };

                foreach (var roleName in roleNames)
                {
                    context.Roles.Add(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    });
                }

                context.SaveChanges();
            }
        }

        private void SeedOther(HappyMeDbContext context)
        {
            if (!context.Images.Any())
            {
                var user = context.Users.FirstOrDefault()?.Id;

                var imageSeedPath = HostingEnvironment.MapPath("~/ImageSeed");

                var alphabetModulePath = Path.Combine(imageSeedPath, "modules-alphabet.png");
                var colorModulePath = Path.Combine(imageSeedPath, "module-colours.png");
                var familyModulePath = Path.Combine(imageSeedPath, "module-family.png");
                var combineModulePath = Path.Combine(imageSeedPath, "modules-connect.png");
                var colorQuestionPath = Path.Combine(imageSeedPath, "cherries_colour.png");
                var alphabetAnswerPath = Path.Combine(imageSeedPath, "sheep.png");
                var familyAnswerPath = Path.Combine(imageSeedPath, "mama.png");
                var connectAnswerPath = Path.Combine(imageSeedPath, "hand_pencil.png");
                var alphabetQuestionPath = Path.Combine(imageSeedPath, "alphabet-question.png");
                var connectQuestionPath = Path.Combine(imageSeedPath, "connect-question.png");

                var alphabetModuleImage = new Image { ImageData = File.ReadAllBytes(alphabetModulePath), AuthorId = user };
                context.Images.Add(alphabetModuleImage);
                var colorModuleImage = new Image { ImageData = File.ReadAllBytes(colorModulePath), AuthorId = user };
                context.Images.Add(colorModuleImage);
                var familyModuleImage = new Image { ImageData = File.ReadAllBytes(familyModulePath), AuthorId = user };
                context.Images.Add(familyModuleImage);
                var combineModuleImage = new Image { ImageData = File.ReadAllBytes(combineModulePath), AuthorId = user };
                context.Images.Add(combineModuleImage);
                var colorQuestionImage = new Image { ImageData = File.ReadAllBytes(colorQuestionPath), AuthorId = user };
                context.Images.Add(colorQuestionImage);
                var alphabetAnswerImage = new Image { ImageData = File.ReadAllBytes(alphabetAnswerPath), AuthorId = user };
                context.Images.Add(alphabetAnswerImage);
                var familyAnswerImage = new Image { ImageData = File.ReadAllBytes(familyAnswerPath), AuthorId = user };
                context.Images.Add(familyAnswerImage);
                var connectAnswerImage = new Image { ImageData = File.ReadAllBytes(connectAnswerPath), AuthorId = user };
                context.Images.Add(connectAnswerImage);
                var alphabetQuestionImage = new Image { ImageData = File.ReadAllBytes(alphabetQuestionPath), AuthorId = user };
                context.Images.Add(alphabetQuestionImage);
                var connectQuestionImage = new Image { ImageData = File.ReadAllBytes(connectQuestionPath), AuthorId = user };
                context.Images.Add(connectQuestionImage);

                context.SaveChanges();

                var userId = context.Users.FirstOrDefault()?.Id;

                var alphabetModule = new Module
                {
                    Name = "Азбука",
                    Description = "Some description{i}",
                    IsActive = true,
                    IsPublic = true,
                    AuthorId = userId,
                    ImageId = alphabetModuleImage.Id
                };
                context.Modules.AddOrUpdate(alphabetModule);

                var colorModule = new Module
                {
                    Name = "Цветове",
                    Description = "Some description{i}",
                    IsActive = true,
                    IsPublic = true,
                    AuthorId = userId,
                    ImageId = colorModuleImage.Id
                };
                context.Modules.AddOrUpdate(colorModule);

                var familyModule = new Module
                {
                    Name = "Семейство",
                    Description = "Some description{i}",
                    IsActive = true,
                    IsPublic = true,
                    AuthorId = userId,
                    ImageId = familyModuleImage.Id
                };
                context.Modules.AddOrUpdate(familyModule);

                var connectModule = new Module
                {
                    Name = "Свържи",
                    Description = "Some description{i}",
                    IsActive = true,
                    IsPublic = true,
                    AuthorId = userId,
                    ImageId = combineModuleImage.Id
                };
                context.Modules.AddOrUpdate(connectModule);

                context.SaveChanges();

                var alphabetQuestion = new Question
                {
                    Text = "Аа",
                    Type = QuestionType.AlphabetQuestion,
                    IsPublic = true,
                    AuthorId = userId,
                    ModuleId = alphabetModule.Id,
                    ImageId = alphabetQuestionImage.Id
                };
                context.Questions.AddOrUpdate(alphabetQuestion);

                var colorQuestion = new Question
                {
                    Text = "Черешките са...",
                    Type = QuestionType.ColorQuestion,
                    IsPublic = true,
                    AuthorId = userId,
                    ImageId = colorQuestionImage.Id,
                    ModuleId = colorModule.Id
                };
                context.Questions.AddOrUpdate(colorQuestion);

                var familyQuestion = new Question
                {
                    Text = "Мама",
                    Type = QuestionType.ImageQuestion,
                    IsPublic = true,
                    AuthorId = userId,
                    ModuleId = familyModule.Id
                };
                context.Questions.AddOrUpdate(familyQuestion);

                var connectQuestion = new Question
                {
                    Text = "Свържи",
                    Type = QuestionType.DragAndDropQuestion,
                    IsPublic = true,
                    AuthorId = userId,
                    ModuleId = connectModule.Id
                };
                context.Questions.AddOrUpdate(connectQuestion);

                context.SaveChanges();

                var alphabetAnswerRed = new Answer
                {
                    Text = "red",
                    IsCorrect = true,
                    QuestionId = colorQuestion.Id,
                    AuthorId = userId,
                };
                context.Answers.AddOrUpdate(alphabetAnswerRed);

                var colorAnswerGreen = new Answer
                {
                    Text = "green",
                    QuestionId = colorQuestion.Id,
                    AuthorId = userId,
                };
                context.Answers.AddOrUpdate(colorAnswerGreen);

                var colorAnswerBlue = new Answer
                {
                    Text = "blue",
                    QuestionId = colorQuestion.Id,
                    AuthorId = userId,
                };
                context.Answers.AddOrUpdate(colorAnswerBlue);

                var colorAnswerOrange = new Answer
                {
                    Text = "orange",
                    QuestionId = colorQuestion.Id,
                    AuthorId = userId,
                };
                context.Answers.AddOrUpdate(colorAnswerOrange);

                var alphabetAnswer1 = new Answer
                {
                    Text = "Агънце",
                    QuestionId = alphabetQuestion.Id,
                    AuthorId = userId,
                    ImageId = alphabetAnswerImage.Id
                };
                context.Answers.AddOrUpdate(alphabetAnswer1);

                var alphabetAnswer2 = new Answer
                {
                    Text = "Агънце",
                    QuestionId = alphabetQuestion.Id,
                    AuthorId = userId,
                    ImageId = alphabetAnswerImage.Id
                };
                context.Answers.AddOrUpdate(alphabetAnswer2);

                var alphabetAnswer3 = new Answer
                {
                    Text = "Агънце",
                    QuestionId = alphabetQuestion.Id,
                    AuthorId = userId,
                    ImageId = alphabetAnswerImage.Id
                };
                context.Answers.AddOrUpdate(alphabetAnswer3);

                var alphabetAnswer4 = new Answer
                {
                    Text = "Агънце",
                    QuestionId = alphabetQuestion.Id,
                    AuthorId = userId,
                    ImageId = alphabetAnswerImage.Id
                };
                context.Answers.AddOrUpdate(alphabetAnswer4);

                var familyAnswer1 = new Answer
                {
                    Text = "Изображение на човек",
                    QuestionId = familyQuestion.Id,
                    AuthorId = userId,
                    ImageId = familyAnswerImage.Id
                };
                context.Answers.AddOrUpdate(familyAnswer1);

                var familyAnswer2 = new Answer
                {
                    Text = "Изображение на човек",
                    QuestionId = familyQuestion.Id,
                    AuthorId = userId,
                    ImageId = familyAnswerImage.Id
                };
                context.Answers.AddOrUpdate(familyAnswer2);

                var familyAnswer3 = new Answer
                {
                    Text = "Изображение на човек",
                    QuestionId = familyQuestion.Id,
                    AuthorId = userId,
                    ImageId = familyAnswerImage.Id
                };
                context.Answers.AddOrUpdate(familyAnswer3);

                var familyAnswer4 = new Answer
                {
                    Text = "Изображение на човек",
                    QuestionId = familyQuestion.Id,
                    AuthorId = userId,
                    ImageId = familyAnswerImage.Id
                };
                context.Answers.AddOrUpdate(familyAnswer4);

                var connectAnswer1 = new Answer
                {
                    Text = "Молив",
                    QuestionId = connectQuestion.Id,
                    AuthorId = userId,
                    ImageId = connectAnswerImage.Id
                };
                context.Answers.AddOrUpdate(connectAnswer1);

                var connectAnswer2 = new Answer
                {
                    Text = "Молив",
                    QuestionId = connectQuestion.Id,
                    AuthorId = userId,
                    ImageId = connectAnswerImage.Id
                };
                context.Answers.AddOrUpdate(connectAnswer2);

                var connectAnswer3 = new Answer
                {
                    Text = "Молив",
                    QuestionId = connectQuestion.Id,
                    AuthorId = userId,
                    ImageId = connectAnswerImage.Id
                };
                context.Answers.AddOrUpdate(connectAnswer3);

                var connectAnswer4 = new Answer
                {
                    Text = "Молив",
                    QuestionId = connectQuestion.Id,
                    AuthorId = userId,
                    ImageId = connectAnswerImage.Id
                };
                context.Answers.AddOrUpdate(connectAnswer4);

                context.SaveChanges();
            }
        }
    }
}
