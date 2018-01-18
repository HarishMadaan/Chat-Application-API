using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;

namespace Chat.DAL.Interfaces
{
    public interface IMessageRepo : IDisposable
    {
        OperationStatus SubmitMessage(MessageCustomModel model);
        object GetMyReciviedMessage(MessageRecipientCustomModel model);
        object GetMySentMessage(MessageCustomModel model);
        object GetOurOldMessage(int SenderId, int ReceiverId);
    }
}
