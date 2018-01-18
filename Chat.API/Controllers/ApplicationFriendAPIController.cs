using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Shared.CustomModels;
using Chat.BDC.Interfaces;
using Chat.BDC.Classes;

namespace Chat.API.Controllers
{
    public class ApplicationFriendAPIController : ApiController
    {
        #region Global Variable
        // APIResponse _response = new APIResponse();
        Response _response = new Response();

        private IApplicationFriendAssociationBusiness applicationFriendService;

        #endregion

        public ApplicationFriendAPIController()
        {

        }

        public ApplicationFriendAPIController(ApplicationFriendAssociationBusiness _applicationFriendService)
        {
            this.applicationFriendService = _applicationFriendService;
        }


        /// <summary>
        /// This method is used to fetch Application Members
        /// </summary>
        /// <returns>List of Application Members</returns>
        [HttpGet]
        [Route("API/ApplicationFriendAPI/GetApplicationMemberList")]
        public Response GetApplicationMemberList(MemberCustomModel objMemberModel)
        {
            _response = new Response();
            try
            {
                IApplicationFriendAssociationBusiness applicationFriendService = new ApplicationFriendAssociationBusiness();
                _response.responseData = applicationFriendService.GetApplicationMemberList(objMemberModel);
                _response.message = "Records loaded successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                applicationFriendService = null;
            }
            return _response;
        }

        /// <summary>
        /// This method is used to add Application Members
        /// </summary>
        /// <returns>Add of Application Members</returns>
        [HttpPost]
        [Route("API/ApplicationFriendAPI/AddFriendRequest")]
        public Response AddFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            _response = new Response();
            try
            {
                IApplicationFriendAssociationBusiness applicationFriendService = new ApplicationFriendAssociationBusiness();
                _response.responseData = applicationFriendService.AddFriendRequest(objAssociation);
                _response.message = "Request send successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                applicationFriendService = null;
            }
            return _response;
        }
        
        /// <summary>
        /// This method is used to Get My Friend Requests
        /// </summary>
        /// <returns>list of My Friend Requests</returns>
        [HttpGet]
        [Route("API/ApplicationFriendAPI/MyFriendRequest/{MemberId}")]
        public Response MyFriendRequest(int MemberId)
        {
            _response = new Response();
            try
            {
                IApplicationFriendAssociationBusiness applicationFriendService = new ApplicationFriendAssociationBusiness();
                _response.responseData = applicationFriendService.MyFriendRequest(MemberId);
                _response.message = "Records loaded successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                applicationFriendService = null;
            }
            return _response;
        }

        /// <summary>
        /// This method is used to add Application Members
        /// </summary>
        /// <returns>Add of Application Members</returns>
        [HttpPost]
        [Route("API/ApplicationFriendAPI/ActionOnFriendRequest")]
        public Response ActionOnFriendRequest(ApplicationFriendAssociationModel objAssociation)
        {
            _response = new Response();
            try
            {
                IApplicationFriendAssociationBusiness applicationFriendService = new ApplicationFriendAssociationBusiness();
                _response.responseData = applicationFriendService.ActionOnFriendRequest(objAssociation);
                _response.message = "Request send successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                applicationFriendService = null;
            }
            return _response;
        }

        /// <summary>
        /// This method is used to Get My Friend List
        /// </summary>
        /// <returns>list of My Friends</returns>
        [HttpGet]  
        [Route("API/ApplicationFriendAPI/MyFriendList/{MemberId}")]
        public Response MyFriendList(int MemberId)
        {
            _response = new Response();
            try
            {
                IApplicationFriendAssociationBusiness applicationFriendService = new ApplicationFriendAssociationBusiness();
                _response.responseData = applicationFriendService.MyFriendList(MemberId);
                _response.message = "Records loaded successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                applicationFriendService = null;
            }
            return _response;
        }

    }
}
