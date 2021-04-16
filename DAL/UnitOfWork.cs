using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork
    {
        private readonly Db _db;
        private UserRepository users;
        private TopicRepository topics;
        private MessageRepository messages;


        public UnitOfWork(Db db)
        {
            _db = db;
        }

        public UserRepository Users { get
            {
                if (users == null)
                    users = new UserRepository(_db);
                return users;
            } 
        }

        public TopicRepository Topics {
            get
            {
                if (topics == null)
                    topics = new TopicRepository(_db);
                return topics;
            }
        
        }

        public MessageRepository Messages
        {
            get
            {
                if (messages == null)
                    messages = new MessageRepository(_db);
                return messages;
            }

        }
    }
}
