using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;
using Chat.DAL.Interfaces;

namespace Chat.DAL.Repositories
{
    public class ApplicationFriendAssociationRepo : IApplicationFriendAssociationRepo
    {
        ChatApplicationEntities dbcontext = null;
        Response response;

        public object GetApplicationMemberList(MemberCustomModel objMemberModel)
        {
            IList<MemberCustomModel> MemberListModel = new List<MemberCustomModel>();
            
            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        MemberListModel = dbcontext.tblMembers.Where(x => x.IsDeleted == false)
                            .Select(x => new MemberCustomModel
                            {
                                MemberId = x.MemberId,
                                Name = x.Name,
                                EmailId = x.EmailId,
                                MobileNo = x.MobileNo,
                                MotherName = x.MotherName,
                                FatherName = x.FatherName,
                                Address = x.Address,
                                DateOfBirth = x.DateOfBirth,
                                Gender = x.Gender,
                                Image=x.Image,
                                IsActive = x.IsActive,
                                IsDeleted = x.IsDeleted,
                                CreatedBy = x.CreatedBy,
                                CreatedDate = x.CreatedDate,
                                ModifyBy = x.ModifyBy,
                                ModifiedDate = x.ModifiedDate,

                            }).OrderByDescending(x => x.MemberId).ToList();
                        
                        return MemberListModel;

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

        public OperationStatus AddFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            OperationStatus status = OperationStatus.Error;
            try
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    if (objAssociation.ApplicationFriendAssociationId == 0)
                    {
                        var rs = dbcontext.tblApplicationFriendAssociations.FirstOrDefault(x => x.MemberId == objAssociation.MemberId && x.FriendId == objAssociation.FriendId && x.IsDeleted == false);
                        if (rs == null)
                        {
                            tblApplicationFriendAssociation _addAssociation = new tblApplicationFriendAssociation
                            {
                                MemberId = objAssociation.MemberId,
                                FriendId = objAssociation.FriendId,
                                RequestBy = objAssociation.RequestBy,
                                IsConfirm = 2,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                CreatedBy = objAssociation.CreatedBy,
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = objAssociation.ModifiedBy,

                            };
                            dbcontext.tblApplicationFriendAssociations.Add(_addAssociation);
                            dbcontext.SaveChanges();
                            
                            status = OperationStatus.Success;                            
                        }
                        else
                        {
                            status = OperationStatus.Duplicate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dbcontext.Dispose();
                status = OperationStatus.Exception;
                throw ex;
            }
            
            return status;
        }

        public object MyFriendRequest(int MemberId)
        {
            IList<ApplicationFriendAssociationModel> MemberListModel = new List<ApplicationFriendAssociationModel>();

            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        MemberListModel = dbcontext.tblApplicationFriendAssociations.Where(x => x.FriendId == MemberId && x.IsConfirm == 2)
                            .Select(x => new ApplicationFriendAssociationModel
                            {
                                ApplicationFriendAssociationId = x.ApplicationFriendAssociationId,
                                MemberId = x.FriendId,                                
                                MemberName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.FriendId).Name : "",                                
                                EmailId = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).EmailId : "",
                                MobileNo = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).MobileNo : "",
                                FriendId = x.MemberId,
                                FriendName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).Name : "",
                                RequestBy = x.RequestBy,
                                RequestByName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.RequestBy).Name : "",
                                IsConfirm = x.IsConfirm,
                                IsActive = x.IsActive,
                                IsDeleted = x.IsDeleted,
                                CreatedBy = x.CreatedBy,
                                CreatedDate = x.CreatedDate,
                                ModifiedBy = x.ModifiedBy, 
                                ModifiedDate = x.ModifiedDate,

                            }).OrderByDescending(x => x.MemberId).ToList();

                        return MemberListModel;

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

        public OperationStatus ActionOnFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            OperationStatus status = OperationStatus.Error;
            try
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    if (objAssociation.ApplicationFriendAssociationId != 0 && objAssociation.Status != string.Empty)
                    {
                        var rs = dbcontext.tblApplicationFriendAssociations.FirstOrDefault(x => x.ApplicationFriendAssociationId == objAssociation.ApplicationFriendAssociationId && x.IsActive == true && x.IsDeleted == false);
                        if (rs != null)
                        {
                            rs.IsConfirm = objAssociation.Status == "Accept" ? 1 : 3;
                            rs.CreatedDate = DateTime.Now;
                            rs.CreatedBy = objAssociation.CreatedBy;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objAssociation.ModifiedBy;

                            dbcontext.SaveChanges();

                            status = OperationStatus.Success;
                        }
                        else
                        {
                            status = OperationStatus.Duplicate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dbcontext.Dispose();
                status = OperationStatus.Exception;
                throw ex;
            }

            return status;
        }

        public object MyFriendList(int MemberId)
        {
            IList<ApplicationFriendAssociationModel> MemberListModel = new List<ApplicationFriendAssociationModel>();

            using (response = new Response())
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    try
                    {
                        response.success = true;
                        MemberListModel = dbcontext.tblApplicationFriendAssociations.Where(x => x.FriendId == MemberId && x.IsConfirm == 1)
                            .Select(x => new ApplicationFriendAssociationModel
                            {
                                ApplicationFriendAssociationId = x.ApplicationFriendAssociationId,
                                MemberId = x.FriendId,
                                MemberName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.FriendId).Name : "",
                                EmailId = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).EmailId : "",
                                MobileNo = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).MobileNo : "",
                                FriendId = x.MemberId,
                                FriendName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.MemberId).Name : "",
                                RequestBy = x.RequestBy,
                                RequestByName = x.tblMember != null ? dbcontext.tblMembers.FirstOrDefault(m => m.MemberId == x.RequestBy).Name : "",
                                IsConfirm = x.IsConfirm,
                                IsActive = x.IsActive,
                                IsDeleted = x.IsDeleted,
                                CreatedBy = x.CreatedBy,
                                CreatedDate = x.CreatedDate,
                                ModifiedBy = x.ModifiedBy,
                                ModifiedDate = x.ModifiedDate,

                            }).OrderByDescending(x => x.MemberId).ToList();

                        return MemberListModel;

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
