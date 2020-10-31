using Domain;
using Microsoft.AspNetCore.Identity;
using System;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Create(User model);
        Task<LoginResult> SignIn(Login model);
        Task SignOut();
        Task<OperationResult<string>> ConfirmEmail(string userId, string code);
    }
}
