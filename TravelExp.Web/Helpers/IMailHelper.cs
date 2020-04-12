using TravelExp.Common.Models;

namespace TravelExp.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}
