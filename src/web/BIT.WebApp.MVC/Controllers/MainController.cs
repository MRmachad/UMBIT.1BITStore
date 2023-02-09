using BIT.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BIT.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePosuiErros(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach(var mensagem in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }
                return true;
            }
            return false;
        }
    }
}
