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
    public class MessageAPIController : ApiController
    {
        #region Global Variable
        // APIResponse _response = new APIResponse();
        Response _response = new Response();
        private IMessageBusiness MessageService;
        #endregion

        public MessageAPIController()
        {

        }

        public MessageAPIController(MessageBusiness _MessageService)
        {
            this.MessageService = _MessageService;
        }

        /// <summary>
        /// This method is used to add Application Members
        /// </summary>
        /// <returns>Add of Application Members</returns>
        [HttpPost]
        [Route("API/MessageAPI/SubmitMessage")]
        public Response SubmitMessage(MessageCustomModel model)
        {
            _response = new Response();
            try
            {
                IMessageBusiness MessageService = new MessageBusiness();
                _response.responseData = MessageService.SubmitMessage(model);
                _response.message = "Message submit successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                MessageService = null;
            }
            return _response;
        }

        [HttpPost]
        [Route("API/MessageAPI/GetMyReciviedMessage")]
        public object GetMyReciviedMessage(MessageRecipientCustomModel model)
        {
            _response = new Response();
            try
            {
                IMessageBusiness MessageService = new MessageBusiness();
                _response.responseData = MessageService.GetMyReciviedMessage(model);
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
                MessageService = null;
            }
            return _response;
        }

        [HttpPost]
        [Route("API/MessageAPI/GetMySentMessage")]
        public object GetMySentMessage(MessageCustomModel model)
        {
            _response = new Response();
            try
            {
                IMessageBusiness MessageService = new MessageBusiness();
                _response.responseData = MessageService.GetMySentMessage(model);
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
                MessageService = null;
            }
            return _response;
        }

        [HttpGet]
        [Route("API/MessageAPI/GetOurOldMessage/{SenderId}/{ReceiverId}")]
        public object GetOurOldMessage(int SenderId, int ReceiverId)
        {
            _response = new Response();
            try
            {
                IMessageBusiness MessageService = new MessageBusiness();
                _response.responseData = MessageService.GetOurOldMessage(SenderId, ReceiverId);
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
                MessageService = null;
            }
            return _response;
        }

    }
}
