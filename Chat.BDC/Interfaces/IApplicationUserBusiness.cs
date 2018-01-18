using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;

namespace Chat.BDC.Interfaces
{
    public interface IApplicationUserBusiness : IDisposable
    {
        object GetLoginUser(LoginUserModel Logininfo);

        OperationStatus SaveApplicationUser(ApplicationUserModel applicationUserModel);
    }
}
