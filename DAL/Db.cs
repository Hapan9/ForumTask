using System;
using System.Collections.Generic;
using AutoFixture;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }

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

            var rnd = new Random();

            var users = new List<User>();

            var topics = new List<Topic>();

            var messages = new List<Message>();

            var fixture = new Fixture();

            for (var i = 0; i < 5; i++)
            {
                var user = fixture.Build<User>()
                    .Without(u => u.Topics)
                    .Without(u => u.Messages)
                    .Create();

                users.Add(user);
            }

            for (var i = 0; i < 10; i++)
            {
                var user = users[rnd.Next(users.Count)];

                var topic = fixture.Build<Topic>()
                    .With(t => t.UserId, user.Id)
                    .Without(t => t.User)
                    .Without(t => t.Messages)
                    .Create();

                topics.Add(topic);
            }

            for (var i = 0; i < 40; i++)
            {
                var user = users[rnd.Next(users.Count)];

                var topic = topics[rnd.Next(topics.Count)];

                var message = fixture.Build<Message>()
                    .With(m => m.UserId, user.Id)
                    .With(m => m.TopicId, topic.Id)
                    .Without(m => m.User)
                    .Without(m => m.Topic)
                    .Create();

                messages.Add(message);
            }

            modelBuilder.Entity<User>()
                .HasData(users);

            modelBuilder.Entity<Topic>()
                .HasData(topics);

            modelBuilder.Entity<Message>()
                .HasData(messages);
        }
    }
}