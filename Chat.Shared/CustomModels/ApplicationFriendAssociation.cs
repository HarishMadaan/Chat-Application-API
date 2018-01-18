using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Shared.CustomModels
{
    public class ApplicationFriendAssociationModel
    {
        public int ApplicationFriendAssociationId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> FriendId { get; set; }
        public Nullable<int> RequestBy { get; set; }
        public Nullable<int> IsConfirm { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public string MemberName { get; set; }
        public string FriendName { get; set; }
        public string RequestByName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
    }
}
