using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Models;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Db _db;
        private IRepository<User> _users;
        private IRepository<Topic> _topics;
        private IRepository<Message> _messages;


        public UnitOfWork(Db db, IRepository<User> users, IRepository<Topic> topics, IRepository<Message> messages)
        {
            _db = db;
            _users = users;
            _topics = topics;
            _messages = messages;
        }

        public IRepository<User> Users
        {
            get
            {
                return _users;
            }
        }

        public IRepository<Topic> Topics
        {
            get
            {
                return _topics;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return _messages;
            }
        }
    }
}
