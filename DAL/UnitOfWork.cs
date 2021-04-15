using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork
    {
        private UserRepository _users;
        private TopicRepository _topics;
        private MessageRepository _messages;

        public UserRepository Users { get
            {
                if (_users == null)
                    _users = new UserRepository();
                return _users;
            } 
        }

        public TopicRepository Topics {
            get
            {
                if (_topics == null)
                    _topics = new TopicRepository();
                return _topics;
            }
        
        }

        public MessageRepository Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessageRepository();
                return _messages;
            }

        }
    }
}
