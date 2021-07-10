using System;
using System.Collections.Generic;
using AutoFixture;
using DAL.Models;

namespace Tests
{
    internal class FakeData
    {
        public FakeData()
        {
            var rnd = new Random();

            Users = new List<User>();

            Topics = new List<Topic>();

            Messages = new List<Message>();

            var fixture = new Fixture();

            for (var i = 0; i < 5; i++)
            {
                var user = fixture.Build<User>()
                    .Without(u => u.Topics)
                    .Without(u => u.Messages)
                    .Create();

                Users.Add(user);
            }

            for (var i = 0; i < 10; i++)
            {
                var user = Users[rnd.Next(Users.Count)];

                var topic = fixture.Build<Topic>()
                    .With(t => t.UserId, user.Id)
                    .Without(t => t.User)
                    .Without(t => t.Messages)
                    .Create();

                Topics.Add(topic);
            }

            for (var i = 0; i < 40; i++)
            {
                var user = Users[rnd.Next(Users.Count)];

                var topic = Topics[rnd.Next(Topics.Count)];

                var message = fixture.Build<Message>()
                    .With(m => m.UserId, user.Id)
                    .With(m => m.TopicId, topic.Id)
                    .Without(m => m.User)
                    .Without(m => m.Topic)
                    .Create();

                Messages.Add(message);
            }
        }

        public List<User> Users { get; set; }

        public List<Topic> Topics { get; set; }

        public List<Message> Messages { get; set; }
    }
}