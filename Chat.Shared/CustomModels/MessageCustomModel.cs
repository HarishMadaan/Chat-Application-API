using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Shared.CustomModels
{
    public class MessageCustomModel
    {
        public int Id { get; set; }
        public Nullable<int> SenderId { get; set; }
        public Nullable<int> ReceiverId { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public Nullable<int> ParentMessageId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
    }
}
