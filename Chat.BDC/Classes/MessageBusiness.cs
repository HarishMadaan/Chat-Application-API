using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;
using Chat.BDC.Interfaces;
using Chat.DAL.Interfaces;
using Chat.DAL.Repositories;

namespace Chat.BDC.Classes
{
    public class MessageBusiness : IMessageBusiness
    {
        IMessageRepo _IMessageRepo;

        /// <summary>
        /// This method is used to save new messages
        /// </summary>
        /// <returns></returns>
        public OperationStatus SubmitMessage(MessageCustomModel model)
        {
            using (_IMessageRepo = new MessageRepo())
            {
                return _IMessageRepo.SubmitMessage(model);
            }
        }

        /// <summary>
        /// This method is used to get my messages
        /// </summary>
        /// <returns></returns>
        public object GetMyReciviedMessage(MessageRecipientCustomModel model)
        {
            using (_IMessageRepo = new MessageRepo())
            {
                return _IMessageRepo.GetMyReciviedMessage(model);
            }
        }

        /// <summary>
        /// This method is used to get my sent messages
        /// </summary>
        /// <returns></returns>
        public object GetMySentMessage(MessageCustomModel model)
        {
            using (_IMessageRepo = new MessageRepo())
            {
                return _IMessageRepo.GetMySentMessage(model);
            }
        }

        public object GetOurOldMessage(int SenderId, int ReceiverId)
        {
            using (_IMessageRepo = new MessageRepo())
            {
                return _IMessageRepo.GetOurOldMessage(SenderId, ReceiverId);
            }
        }

        public void Dispose()
        {
            _IMessageRepo.Dispose();
            GC.SuppressFinalize(this);
            // throw new NotImplementedException();
        }

    }
}
