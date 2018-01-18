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
    public class MemberAPIController : ApiController
    {
        #region Global Variable
        // APIResponse _response = new APIResponse();
        Response _response = new Response();
        private IMemberBusiness MemberService;
        #endregion

        public MemberAPIController()
        {

        }

        public MemberAPIController(MemberBusiness _MemberService)
        {
            this.MemberService = _MemberService;
        }

        /// <summary>
        /// This method is used for forgot password
        /// </summary>
        /// <returns>forgot password</returns>
        [HttpPost]
        [Route("API/MemberAPI/ForgotPassword")]
        public Response ForgotPassword(ForgotPasswordCustomModel model)
        {
            _response = new Response();
            try
            {
                MemberBusiness memberService = new MemberBusiness();
                _response.responseData = memberService.ForgotPassword(model);
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
                MemberService = null;
            }
            return _response;
        }


    }
}
