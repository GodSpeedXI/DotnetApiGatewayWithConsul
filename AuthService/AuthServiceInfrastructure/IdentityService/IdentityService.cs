using System.Linq;
using AuthServiceDomain.Common.Interfaces;
using AuthServiceDomain.Common.Models;
using System.Threading.Tasks;
using AuthServiceDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceInfrastructure.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserModel> _userManager;

        public IdentityService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }
        public async Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new UserModel()
            {
                UserName = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(int userId)
        {
            var user = _userManager.Users.SingleOrDefault(u =>
                u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        private async Task<Result> DeleteUserAsync(UserModel user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<string> GetUserNameAsync(int userId)
        {
            var user = await _userManager.Users.FirstAsync(u =>
                u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> UserIsInRole(int userId, string roleId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, roleId);
        }
    }

    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
