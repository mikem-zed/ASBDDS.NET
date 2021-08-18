using System.Threading.Tasks;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;

namespace ASBDDS.Web.Shared.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RefreshToken();
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
}