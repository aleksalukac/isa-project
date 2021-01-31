using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements);

        Task<bool> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName);
    }
    
}
