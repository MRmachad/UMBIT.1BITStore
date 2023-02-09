
using BIT.WebApp.MVC.Models;

namespace BIT.WebApp.MVC.Services
{
    public interface IAutenticationService
    {
         Task<UsuarioRespostaLoginViewModel> Login(UsuarioLogin usuarioLogin);
         Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistro usuarioRegistro);
    }
}
