using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Web;

namespace proyectoCakeAPI7
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private static readonly string Secret = "1234567890abcdef1234567890abcdef";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var path = request.RequestUri.AbsolutePath.ToLower();

            // ✅ Permitir preflight CORS (muy importante)
            if (request.Method == HttpMethod.Options)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            // ✅ Excluir login y register
            if (path.Contains("/api/usuario/login") || path.Contains("/api/usuario/register"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            // 🔐 Validar token
            if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "Bearer")
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token no proporcionado");

            var token = request.Headers.Authorization.Parameter;

            if (string.IsNullOrEmpty(token))
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token no proporcionado");

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                Thread.CurrentPrincipal = principal;

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido o expirado");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
