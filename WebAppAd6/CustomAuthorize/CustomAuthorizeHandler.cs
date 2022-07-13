using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WebAppAd6.CustomAuthorize {
    public class CustomAuthorizeHandler : AuthorizationHandler<CustomAuthorizeRequirement> {

        private readonly IEnumerable<string> _availableUsers;

        public CustomAuthorizeHandler(IOptions<AppSettings> options) {
            _availableUsers = options.Value.AvailableUsers;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizeRequirement requirement
        ) {
            var domainName = context.User.Identity.Name;
            var claims = context.User.Claims.ToList();

            if (!string.IsNullOrEmpty(domainName)
                && _availableUsers.Contains(domainName)
                && requirement.DomainName.Equals(domainName)
            ) {
                context.Succeed(requirement);
            } else {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
