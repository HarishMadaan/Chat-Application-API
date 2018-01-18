using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;

namespace Chat.BDC.Interfaces
{
    public interface IApplicationFriendAssociationBusiness : IDisposable
    {
        object GetApplicationMemberList(MemberCustomModel objMemberModel);
        OperationStatus AddFriendRequest(ApplicationFriendAssociationModel objAssociation);
        object MyFriendRequest(int MemberId);
        OperationStatus ActionOnFriendRequest(ApplicationFriendAssociationModel objAssociation);
        object MyFriendList(int MemberId);
    }
}
