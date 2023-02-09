
using BIT.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace BIT.WebApp.MVC.Services
{
    public class AutenticationService :Service, IAutenticationService
    {
        private readonly HttpClient cliente;
        private readonly string BaseURL = "https://localhost:44373/api/Auth";
        public AutenticationService(HttpClient cliente)
        {
            this.cliente = cliente;
        }
        public async Task<UsuarioRespostaLoginViewModel> Login(UsuarioLogin usuarioLogin)
        {

            var logincontent = new StringContent(
                JsonSerializer.Serialize(usuarioLogin),
                Encoding.UTF8,
                "application/json");


            var response = await this.cliente.PostAsync(BaseURL + "/autenticar", logincontent);

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!this.TratarerrosResponse(response))
            {
                return new UsuarioRespostaLoginViewModel()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), option)
                };
            }



            return JsonSerializer.Deserialize<UsuarioRespostaLoginViewModel>(await response.Content.ReadAsStringAsync(), option);
        }
        public async Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistro usuarioRegistro)
        {
            var logincontent = new StringContent(
               JsonSerializer.Serialize(usuarioRegistro),
               Encoding.UTF8,
               "application/json");

            var response = await this.cliente.PostAsync(BaseURL + "/nova-conta", logincontent);

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!this.TratarerrosResponse(response))
            {
                return new UsuarioRespostaLoginViewModel()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), option)
                };
            }

            return JsonSerializer.Deserialize<UsuarioRespostaLoginViewModel>(await response.Content.ReadAsStringAsync(), option);
        }

    }
}
