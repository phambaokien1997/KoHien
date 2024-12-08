using BookStore.IdentityServer.Data;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;

    public CustomProfileService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", user.Id) // Thêm claim sub
            };

            // Thêm các claims khác nếu cần
            claims.AddRange(await _userManager.GetClaimsAsync(user));

            context.IssuedClaims.AddRange(claims);
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        context.IsActive = user != null;
    }
}
