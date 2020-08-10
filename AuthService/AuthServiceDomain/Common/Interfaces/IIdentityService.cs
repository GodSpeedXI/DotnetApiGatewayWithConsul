using AuthServiceDomain.Common.Models;
using System.Threading.Tasks;

namespace AuthServiceDomain.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(int userId);

        Task<(Result Result, int UserId)> CreateUserAsync(
            string userName,
            string password);

        Task<bool> UserIsInRole(int userId, string role);

        Task<Result> DeleteUserAsync(int userId);
    }
}
