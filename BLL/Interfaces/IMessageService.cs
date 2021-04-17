using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(MessageDto messageDto);
        Task DeleteMessage(Guid messageId);
        Task<Message> GetMessage(Guid id);
        Task<IEnumerable<Message>> GetMessages();
        Task UpdateMessage(Guid messageId, MessageDto messageDto);
    }
}