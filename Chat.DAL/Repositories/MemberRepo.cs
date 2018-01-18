using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Shared.CustomModels;
using Chat.DAL.Interfaces;
using System.Net.Mail;

namespace Chat.DAL.Repositories
{
    public class MemberRepo : IMemberRepo
    {
        ChatApplicationEntities dbcontext = null;
        Response response;

        public OperationStatus ForgotPassword(ForgotPasswordCustomModel model)
        {
            OperationStatus status = OperationStatus.Error;
            try
            {
                using (dbcontext = new ChatApplicationEntities())
                {
                    var rs = dbcontext.tblApplicationUsers.FirstOrDefault(x => x.UserName == model.UserName);
                    if (rs != null)
                    {
                        string from = "madaan.harish08@gmail.com"; //any valid GMail ID  
                        string to = Convert.ToString(rs.EmailId); //any valid GMail ID  
                        using (MailMessage mail = new MailMessage(from, to))
                        {
                            mail.Subject = "SFS Management Forgot Password";
                            mail.Body = "Dear " + rs.Name + " <br><br>Please use this password to login: " + rs.Password + "<br><br>Thanks,<br>Team";

                            mail.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential
                            ("madaan.harish08@gmail.com", "shally123");// Enter seders User name and password 
                            smtp.EnableSsl = true;
                            smtp.Send(mail);

                            status = OperationStatus.Success;
                        }
                    }
                    else
                    {
                        status = OperationStatus.Duplicate;
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

        public void Dispose()
        {
            dbcontext.Dispose();
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }


    }
}
