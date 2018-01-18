using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Shared.CustomModels
{
    public class MessageRecipientCustomModel
    {
        public int MessageRecipientId { get; set; }
        public Nullable<int> MessageId { get; set; }
        public Nullable<int> RecipientId { get; set; }
        public Nullable<int> SenderId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
    }
}
