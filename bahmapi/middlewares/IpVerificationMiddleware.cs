using System;
using System.Net;
using System.Threading.Tasks;
using bahmapi.Entities;
using bahmapi.Services;
using Microsoft.AspNetCore.Http;
// #nullable disable

namespace bahmapi.Middewares
{
    public class IpVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        public IpVerificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var a = context.Request.Path;
            var userId = context.User.Identity.Name; // obtém o nome de usuário autenticado


                var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = context.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                }



            var userIp = ip; // obtém o endereço IP do usuário

            // verificar se o usuário tem um IP cadastrado e se é diferente do IP atual
            if (!string.IsNullOrEmpty(userId) && userIp != null)
            {
                // substitua este exemplo com a lógica para obter o IP do usuário cadastrado
                var registeredIp = await ObterIpCadastrado(userId);

                // comparar o IP do usuário cadastrado com o IP atual
                if (!userIp.Equals(registeredIp))
                {
                    // IP inválido - negar acesso
                    // context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    // await context.Response.WriteAsync("IP inválido.");
                    // return;
                }
            }

            await _next(context);
        }

        // exemplo de método para obter o IP cadastrado do usuário
        private async Task<string> ObterIpCadastrado(string userId)
        {
            IUsuarioService usuarioService = new UsuarioService(new DatabaseContext());
            IClienteService clienteService = new ClienteService(new DatabaseContext());


            Usuario user = await usuarioService.Detalhes(Convert.ToInt32(userId));
            Cliente cliente = await clienteService.Detalhes(user.ClienteId);
            return cliente.IpCliente;

        }
    }

}

