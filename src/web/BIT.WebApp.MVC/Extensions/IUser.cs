using System.Security.Claims;

namespace BIT.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        bool EstaAutenticado();
        string ObterUserEmail();
        string ObterUserToken();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();

    }

    public class AspNetUser : IUser
    {
        private readonly  IHttpContextAccessor ContextAccessor;

        public AspNetUser(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }

        public string Name => this.ContextAccessor.HttpContext.User.Identity.Name;

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(this.ContextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public bool EstaAutenticado()
        {
            return this.ContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? this.ContextAccessor.HttpContext.User.GetUserEmail() : "";
        }

        public string ObterUserToken()
        {

            return EstaAutenticado() ? this.ContextAccessor.HttpContext.User.GetUserToken() : "";
        }

        public bool PossuiRole(string role)
        {
            return this.ContextAccessor.HttpContext.User.IsInRole(role);    
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return this.ContextAccessor.HttpContext.User.Claims;
        }
        public HttpContext ObterHttpContext()
        {
            return this.ContextAccessor.HttpContext;
        }
    }

    public static class ClaimPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if(principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if(principal ==  null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }
        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
