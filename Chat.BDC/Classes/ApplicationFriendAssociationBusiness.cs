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
    public class ApplicationFriendAssociationBusiness : IApplicationFriendAssociationBusiness
    {
        IApplicationFriendAssociationRepo _IAssociationRepo;

        /// <summary>
        /// This method is used to list all members
        /// </summary>
        /// <returns></returns>
        public object GetApplicationMemberList(MemberCustomModel objMemberModel)
        {
            using (_IAssociationRepo = new ApplicationFriendAssociationRepo())
            {
                return _IAssociationRepo.GetApplicationMemberList(objMemberModel);
            }
        }

        /// <summary>
        /// This method is used to save new members
        /// </summary>
        /// <returns></returns>
        public OperationStatus AddFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            using (_IAssociationRepo = new ApplicationFriendAssociationRepo())
            {
                return _IAssociationRepo.AddFriendRequest(objAssociation);
            }
        }

        public object MyFriendRequest(int MemberId)
        {
            using (_IAssociationRepo = new ApplicationFriendAssociationRepo())
            {
                return _IAssociationRepo.MyFriendRequest(MemberId);
            }
        }

        /// <summary>
        /// This method is used to do action on friend request
        /// </summary>
        /// <returns></returns>
        public OperationStatus ActionOnFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            using (_IAssociationRepo = new ApplicationFriendAssociationRepo())
            {
                return _IAssociationRepo.ActionOnFriendRequest(objAssociation);
            }
        }

        public object MyFriendList(int MemberId)
        {
            using (_IAssociationRepo = new ApplicationFriendAssociationRepo())
            {
                return _IAssociationRepo.MyFriendList(MemberId);
            }
        }

        public void Dispose()
        {
            _IAssociationRepo.Dispose();
            GC.SuppressFinalize(this);
            // throw new NotImplementedException();
        }
    }
}
