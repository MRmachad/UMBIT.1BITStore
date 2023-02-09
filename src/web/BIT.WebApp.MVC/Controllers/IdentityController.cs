
using BIT.WebApp.MVC.Models;
using BIT.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BIT.WebApp.MVC.Controllers
{
    [Route("[controller]")]
    public class IdentidadeController : MainController
    {
        private readonly IAutenticationService _authenticationService;

        public IdentidadeController(IAutenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [Route("nova-conta")]
        public async Task<ActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if(!ModelState.IsValid) return View(usuarioRegistro);

            //api - registro
            var resposta = await _authenticationService.Registro(usuarioRegistro);

            if (this.ResponsePosuiErros(resposta.ResponseResult)) 
                return View(usuarioRegistro);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Login")]
        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return View(usuarioLogin);

            //api - login
            var resposta = await _authenticationService.Login(usuarioLogin);

            if (this.ResponsePosuiErros(resposta.ResponseResult))
                return View(usuarioLogin);

            await RealizaLogin(resposta);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        private async Task RealizaLogin(UsuarioRespostaLoginViewModel resposta)
        {
            var token = obterTokenFormatado(resposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken obterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken; 
        }

    }
}
