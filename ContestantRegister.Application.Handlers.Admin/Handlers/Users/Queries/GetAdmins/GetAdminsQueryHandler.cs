using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Admin.Users.Queries;
using ContestantRegister.Cqrs.Features.Admin.Users.ViewModels;
using ContestantRegister.Domain;
using ContestantRegister.Domain.Repository;
using ContestantRegister.DomainServices.Interfaces.Helpers;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices;
using ContestantRegister.Services.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Admin.Users.QueryHandlers
{
    internal class GetAdminsQueryHandler : ReadRepositoryQueryHandler<GetAdminsQuery, List<UserAdminViewModel>>
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAdminsQueryHandler(IReadRepository repository, IUserService userService, UserManager<ApplicationUser> userManager) : base(repository)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public override async Task<List<UserAdminViewModel>> HandleAsync(GetAdminsQuery query)
        {
            var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin);

            var adminIds = admins.Select(u => u.Id).ToList();
            var notAdmins = await ReadRepository.Set<ApplicationUser>().Where(u => !adminIds.Contains(u.Id)).ToListAsync();

            var defaultAdmin = admins.First(a => a.Email == UserHelper.DefaultAdminEmail);
            admins.Remove(defaultAdmin);
            var viewModels = admins.Select(u => new UserAdminViewModel { IsAdmin = true, User = u }).ToList();
            var notAdminViewModels = notAdmins.Select(u => new UserAdminViewModel { User = u }).ToList();

            viewModels.AddRange(notAdminViewModels);

            return viewModels.OrderBy(x => x.User.Id).ToList();
        }
    }
}
