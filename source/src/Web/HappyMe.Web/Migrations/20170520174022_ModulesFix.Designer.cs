using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HappyMe.Data;
using HappyMe.Common.Models;

namespace HappyMe.Web.Migrations
{
    [DbContext(typeof(HappyMeDbContext))]
    [Migration("20170520174022_ModulesFix")]
    partial class ModulesFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HappyMe.Data.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int?>("ImageId");

                    b.Property<bool>("IsCorrect");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsHidden");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("OrderBy");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(5000);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<byte[]>("ImageData");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Path")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(5000);

                    b.Property<int?>("ImageId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPublic");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("HappyMe.Data.Models.ModuleSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<bool>("IsFinised");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("ModuleId");

                    b.Property<DateTime>("StartedDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("ModuleSessions");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int?>("ImageId");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPublic");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("HappyMe.Data.Models.QuestionInModule", b =>
                {
                    b.Property<int>("QuestionId");

                    b.Property<int>("ModuleId");

                    b.HasKey("QuestionId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("QuestionsInModules");
                });

            modelBuilder.Entity("HappyMe.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("ParentId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("ParentId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HappyMe.Data.Models.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("ModuleSessionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("ModuleSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersAnswers");
                });

            modelBuilder.Entity("HappyMe.Data.Models.UserInModule", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ModuleId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("UserId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("UsersInModules");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Answer", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.Image", "Image")
                        .WithMany("Answers")
                        .HasForeignKey("ImageId");

                    b.HasOne("HappyMe.Data.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Image", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Module", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("HappyMe.Data.Models.Image", "Image")
                        .WithMany("Modules")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.ModuleSession", b =>
                {
                    b.HasOne("HappyMe.Data.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.Question", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User", "Author")
                        .WithMany("Questions")
                        .HasForeignKey("AuthorId");

                    b.HasOne("HappyMe.Data.Models.Image", "Image")
                        .WithMany("Questions")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.QuestionInModule", b =>
                {
                    b.HasOne("HappyMe.Data.Models.Module", "Module")
                        .WithMany("QuestionsInModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.Question", "Question")
                        .WithMany("QuestionsInModules")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HappyMe.Data.Models.User", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.UserAnswer", b =>
                {
                    b.HasOne("HappyMe.Data.Models.Answer", "Answer")
                        .WithMany("AnswersByUsers")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.ModuleSession", "ModuleSession")
                        .WithMany("UsersAnswers")
                        .HasForeignKey("ModuleSessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HappyMe.Data.Models.UserInModule", b =>
                {
                    b.HasOne("HappyMe.Data.Models.Module", "Module")
                        .WithMany("UsersInModule")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.User", "User")
                        .WithMany("UserInModules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HappyMe.Data.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HappyMe.Data.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
