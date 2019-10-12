using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContestantRegister.Domain;
using ContestantRegister.Framework.Extensions;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace ContestantRegister.Infrastructure.Implementation
{
    public class CurrentUserService : ICurrentUserService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string _email;

        public string Email
        {
            get
            {
                if (_email.IsEmpty())
                {
                    _email = _httpContextAccessor.HttpContext.User.Identity.Name;
                }

                return _email;
            }
        }

        private string _id;

        public string Id
        {
            get
            {
                if (_id.IsEmpty())
                {
                    _id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                return _id;
            }
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
