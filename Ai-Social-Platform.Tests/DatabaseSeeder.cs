using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Publication;
using Microsoft.AspNetCore.Identity;

namespace Ai_Social_Platform.Tests
{
    internal static class DatabaseSeeder
    {
        internal static List<Publication> Publications;
        internal static List<Comment> Comments;
        internal static List<ApplicationUser> ApplicationUsers;
        internal static List<Media> MediaFiles;

        internal static void SeedDatabase(ASPDbContext dbContext)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUsers = new List<ApplicationUser>()
            { 
                new ApplicationUser()
                {
                Id = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                FirstName = "Georgi",
                LastName = "Georgiev",
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.com",
                Email = "user@user.com",
                NormalizedEmail = "user@user.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.com",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "123456")
                }
            };

            Publications = new List<Publication>()
            {
                new Publication()
                {
                    Content = "This is the first seeded publication Content from Ivan",
                    AuthorId = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                },
                new Publication()
                {
                    Content = "This is the second seeded publication Content from Georgi",
                    AuthorId = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                }
            };

            Comments = new List<Comment>()
            {
                new Comment()
                {
                    Content = "This is the first seeded comment Content from Ivan",
                    AuthorId = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Publications.First().Id
                },
                new Comment()
                {
                    Content = "This is the second seeded comment Content from Ivan",
                    AuthorId = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Publications.Skip(1).First().Id
                },
                new Comment()
                {
                    Content = "This is the first seeded comment Content from Georgi",
                    AuthorId = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Publications.First().Id
                },
                new Comment()
                {
                    Content = "This is the second seeded comment Content from Georgi",
                    AuthorId = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Publications.Skip(1).First().Id
                }
              
            };
         
            MediaFiles = new List<Media>();

        }
    }
}
