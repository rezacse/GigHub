﻿using GigHub.Core;
using GigHub.Core.Models;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {

        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (!context.Users.Any())
            {

                context.Users.Add(new ApplicationUser
                {
                    UserName = "user1",
                    Name = "user1",
                    Email = "-",
                    PasswordHash = "-"
                });

                context.Users.Add(new ApplicationUser
                {
                    UserName = "user2",
                    Name = "user2",
                    Email = "--",
                    PasswordHash = "-"
                });

            }

            if (!context.Genres.Any())
            {
                context.Genres.Add(new Genre
                {
                    Id = 1,
                    Name = "Country"
                });
                context.Genres.Add(new Genre
                {
                    Id = 2,
                    Name = "Rock"
                });
                context.Genres.Add(new Genre
                {
                    Id = 3,
                    Name = "Metal"
                });
            }

            context.SaveChanges();
        }
    }
}
