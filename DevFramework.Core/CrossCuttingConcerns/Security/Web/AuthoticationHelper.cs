using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class AuthoticationHelper
    {
        public static void CreateAuthCookie(Guid id, string userName, string email, DateTime expiration,
            string[] roles, bool rememberMe, string firstName, string lastName)
        {
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe,
                CreateAuthTag(email, roles,firstName,lastName,id));
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

        }

        private static string CreateAuthTag(string email, string[] roles, string firstName, string lastName, Guid id)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(email);
            stringBuilder.Append("|");
            for (int i = 0; i < roles.Length; i ++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }
                stringBuilder.Append(roles[i]);
            }
            stringBuilder.Append("|");
            stringBuilder.Append(firstName);
            stringBuilder.Append("|");
            stringBuilder.Append(lastName);
            stringBuilder.Append("|");
            stringBuilder.Append(id);
            
            return stringBuilder.ToString();


        }
    }
}
