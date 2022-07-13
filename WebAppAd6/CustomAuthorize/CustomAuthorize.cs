using Microsoft.AspNetCore.Authorization;
using System.DirectoryServices.Protocols;

namespace WebAppAd6.CustomAuthorize {
    public class CustomAuthorizeRequirement : IAuthorizationRequirement {

        protected internal string DomainName { get; set; }

        public CustomAuthorizeRequirement(string domainName) {
            DomainName = domainName;
        }

    }
}
