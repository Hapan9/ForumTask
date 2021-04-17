using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Enums;

namespace DAL
{
    public class Db:DbContext
    {
        public Db(DbContextOptions<Db> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.User)
                .WithMany(u => u.Topics)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Message>()
                .HasOne(t => t.Topic)
                .WithMany(u => u.Messages)
                .OnDelete(DeleteBehavior.ClientCascade);

            Guid u1 = Guid.NewGuid();
            Guid u2 = Guid.NewGuid();
            Guid u3 = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = u1,
                    Name = "User",
                    Surname = "First",
                    Login = "Login1",
                    Password = Guid.NewGuid(),
                    Role = Roles.Administrator
                },
                new User()
                {
                    Id = u2,
                    Name = "User",
                    Surname = "Second",
                    Login = "Login2",
                    Password = Guid.NewGuid(),
                    Role = Roles.Moderator
                },
                new User()
                {
                    Id = u3,
                    Name = "User",
                    Surname = "Third",
                    Login = "Login2",
                    Password = Guid.NewGuid(),
                    Role = Roles.User
                }
            );

            Guid t1 = Guid.NewGuid();
            Guid t2 = Guid.NewGuid();

            modelBuilder.Entity<Topic>().HasData(
                new Topic()
                {
                    Id = t1,
                    Name = "First topic",
                    UserId = u1
                },
                new Topic()
                {
                    Id = t2,
                    Name = "Second topic",
                    UserId = u2
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message()
                {
                    Id = Guid.NewGuid(),
                    Text = "Message 0-0",
                    TopicId = t1,
                    UserId = u1
                },
                new Message()
                {
                    Id = Guid.NewGuid(),
                    Text = "Message 0-1",
                    TopicId = t1,
                    UserId = u2
                },
                new Message()
                {
                    Id = Guid.NewGuid(),
                    Text = "Message 1-0",
                    TopicId = t2,
                    UserId = u1
                },
                new Message()
                {
                    Id = Guid.NewGuid(),
                    Text = "Message 1-1",
                    TopicId = t2,
                    UserId = u2
                }
            );

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
