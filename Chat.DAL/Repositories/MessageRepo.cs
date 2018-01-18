using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;
using Chat.DAL.Interfaces;
using System.Transactions;

namespace Chat.DAL.Repositories
{
    public class MessageRepo : IMessageRepo
    {
        ChatApplicationEntities dbcontext = null;
        Response response;

        public OperationStatus SubmitMessage(MessageCustomModel model)
        {
            OperationStatus status = OperationStatus.Error;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (dbcontext = new ChatApplicationEntities())
                    {
                        if (model.Id == 0)
                        {
                            tblMessage _addMessage = new tblMessage
                            {
                                SenderId = model.SenderId,
                                Subject = model.Subject,
                                MessageBody = model.MessageBody,
                                ParentMessageId = model.ParentMessageId,                                
                                IsActive = true,                
                                CreatedDate = DateTime.Now                
                            };
                            dbcontext.tblMessages.Add(_addMessage);
                            dbcontext.SaveChanges();
                            int messageid = _addMessage.Id;

                            tblMessageRecipient _addMessageRecipient = new tblMessageRecipient
                            {
                                MessageId = messageid,
                                RecipientId = model.ReceiverId,                                
                                IsActive = true,
                                CreatedDate = DateTime.Now,                                
                            };

                            dbcontext.tblMessageRecipients.Add(_addMessageRecipient);
                            dbcontext.SaveChanges();

                            status = OperationStatus.Success;
                            ts.Complete();
                        }
                        else
                        {
                            status = OperationStatus.Duplicate;
                            //ts.Dispose();
                        }
                    }                                            
                }
                catch (Exception ex)
                {
                    dbcontext.Dispose();
                    status = OperationStatus.Exception;
                    ts.Dispose();
                    throw ex;
                }
            }
            return status;
        }

        /// <summary>
        /// method is used to get my recivied messages
        /// </summary>
        /// <param name="MessageModel"></param>
        /// <returns></returns>
        public object GetMyReciviedMessage(MessageRecipientCustomModel model)
        {
            List<MessageRecipientCustomModel> MessageListModel = new List<MessageRecipientCustomModel>();

            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        MessageListModel = dbcontext.tblMessageRecipients.Where(x => x.RecipientId == model.RecipientId && (model.SenderId == null || model.SenderId == 0 || x.tblMessage.SenderId == model.SenderId))
                            .Select(x => new MessageRecipientCustomModel
                            {
                                MessageId = x.MessageId,
                                MessageRecipientId = x.MessageRecipientId,
                                RecipientId = x.RecipientId,
                                SenderId = x.tblMessage != null ? x.tblMessage.SenderId : 0,
                                ReceiverName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == model.RecipientId).Name : "",
                                SenderName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == model.SenderId).Name : "",
                                Message = x.tblMessage != null ? x.tblMessage.MessageBody : "",

                                IsActive = x.IsActive,
                                CreatedDate = x.CreatedDate,

                            }).OrderBy(x => x.MessageRecipientId).ToList();

                        return MessageListModel;

                        //                     var query = dbcontext.tblMessageRecipients // your starting point - table in the "from" statement
                        //.Join(dbcontext.tblMessages, // the source table of the inner join
                        //   post => post.MessageId,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                        //   meta => meta.Id,   // Select the foreign key (the second part of the "on" clause)
                        //   (post, meta) => new { Post = post, Meta = meta }) // selection
                        //.Where(postAndMeta => postAndMeta.Post.RecipientId == model.RecipientId && postAndMeta.Meta.SenderId == model.SenderId)    // where statement
                        //.Select(m => new MessageRecipientCustomModel
                        //{
                        //    MessageId = m.Post.MessageId,
                        //    RecipientId = m.Post.RecipientId,
                        //    SenderId = m.Meta.SenderId
                        //}
                        //).ToList();

                        //                     return query;

                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public object GetMySentMessage(MessageCustomModel model)
        {
            List<MessageCustomModel> MessageListModel = new List<MessageCustomModel>();

            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        MessageListModel = dbcontext.tblMessages.Where(x => x.SenderId == model.SenderId && (model.ReceiverId == null || model.ReceiverId == 0 || x.tblMessageRecipients.FirstOrDefault(m => m.RecipientId == model.ReceiverId).RecipientId == model.ReceiverId))
                            .Select(x => new MessageCustomModel
                            {
                                Id = x.Id,                                
                                SenderId = x.SenderId,
                                ReceiverName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == model.ReceiverId).Name : "",
                                SenderName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == model.SenderId).Name : "",
                                MessageBody = x.MessageBody,
                                
                                IsActive = x.IsActive,
                                CreatedDate = x.CreatedDate,

                            }).OrderBy(x => x.Id).ToList();

                        return MessageListModel;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public object GetOurOldMessage(int SenderId, int ReceiverId)
        {
            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        var MessageList = dbcontext.USP_OurOldMessage(SenderId, ReceiverId).ToList();
                        return MessageList;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }

    }
}
