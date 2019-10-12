using Microsoft.AspNetCore.Mvc;
using IMyUrlHelper = ContestantRegister.Services.InfrastructureServices.IUrlHelper;

namespace ContestantRegister.Infrastructure.Implementation
{
    public class MvcUrlHelper : IMyUrlHelper
    {
        private readonly IUrlHelper _urlHelper;

        public MvcUrlHelper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string Action(string action, string controller, object values, string protocol)
        {
            return _urlHelper.Action(action, controller, values, protocol);
        }
    }
}
