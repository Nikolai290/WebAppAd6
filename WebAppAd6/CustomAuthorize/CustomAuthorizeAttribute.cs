using Microsoft.AspNetCore.Authorization;

namespace WebAppAd6.CustomAuthorize {
    public class CustomAuthorizeAttribute : AuthorizeAttribute {
        public CustomAuthorizeAttribute(string domainName) {
            Policy = domainName;
        }
    }
}
