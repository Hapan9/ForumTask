using System;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(MessageDTO messageDTO);
        Task DeleteMessage(Guid messageId);
        Task<Message> GetMessage(Guid id);
        Task<IEnumerable<Message>> GetMessages();
        Task UpdateMessage(Guid messageId, MessageDTO messageDTO);
    }
}