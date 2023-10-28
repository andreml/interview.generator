using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class JwtCollectionExtension
    {

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            var key = Encoding.ASCII.GetBytes(Configuration["Secret:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.Events = new JwtBearerEvents
                {
                    OnChallenge = c =>
                    {
                        var errors = new List<string> { "Usuário não está autenticado" };
                        c.HandleResponse();
                        var response = JsonSerializer.Serialize(new ResponseErro() { Codigo = (int)HttpStatusCode.Unauthorized, Mensagens = errors, Excecao = "Autenticação obrigatória" });
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json;charset=utf-8";
                        return c.Response.WriteAsync(response);
                    },
                    OnForbidden = c =>
                    {
                        var errors = new List<string> { "Usuário não tem permissão para acessar essa funcionalidade" };
                        var response = JsonSerializer.Serialize(new ResponseErro() { Codigo = (int)HttpStatusCode.Forbidden, Mensagens = errors, Excecao = "Permissão negada" });
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json;charset=utf-8";
                        return c.Response.WriteAsync(response);
                    }
                };

                x.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
