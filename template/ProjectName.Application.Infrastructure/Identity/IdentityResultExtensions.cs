using Microsoft.AspNetCore.Identity;
using ProjectName.Application.Models;
using System.Linq;

namespace ProjectName.Application.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static ServiceResult ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? ServiceResult.Success()
                : ServiceResult.Failure(result.Errors.Select(e => e.Description));
        }
    }
}