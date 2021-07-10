using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(MessageDto messageDto);
        Task DeleteMessage(Guid messageId);
        Task<MessageDto> GetMessage(Guid id);
        Task<IEnumerable<MessageDto>> GetMessages();
        Task UpdateMessage(Guid messageId, MessageDto messageDto);
    }
}