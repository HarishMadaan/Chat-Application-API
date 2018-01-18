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
    public class MemberBusiness : IMemberBusiness
    {
        IMemberRepo objDAL;

        public OperationStatus ForgotPassword(ForgotPasswordCustomModel model)
        {
            using (objDAL = new MemberRepo())
            {
                return objDAL.ForgotPassword(model);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
