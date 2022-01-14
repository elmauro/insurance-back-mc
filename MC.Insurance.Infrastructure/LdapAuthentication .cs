using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MC.Insurance.DTO.Enumerations;

namespace MC.Insurance.Infrastructure
{
    public class LdapAuthentication : IAuthenticationService
    {
        private readonly LdapConfig config;
        private readonly JwtConfig jwtConfig;

        public LdapAuthentication(IOptions<LdapConfig> config, IOptions<JwtConfig> jwtConfig)
        {
            this.config = config.Value;
            this.jwtConfig = jwtConfig.Value;
        }

        public Task<User> Login(string userName, string password)
        {
            string userDn = $"uid={userName},{config.UserDomainName}";
            
            using (var connection = new LdapConnection { SecureSocketLayer = false })
            {
                connection.Connect(config.Path, config.Port);
                connection.Bind(userDn, password);

                if (connection.Bound)
                {
                    var entities =
                    connection.Search($"uid={userName},{config.UserDomainName}", LdapConnection.ScopeSub, $"uid = {userName}", // Note that more spaces can not be hit, otherwise you can't find out
                        new string[] { "sAMAccountName", "cn", "memberof" }, false);

                    var sAMAccountName = string.Empty;

                    User found = new User();

                    while (entities.HasMore())
                    {
                        var entity = entities.Next();
                        sAMAccountName = entity.GetAttribute("sAMAccountName")?.StringValue;

                        if (sAMAccountName != null && sAMAccountName == userName)
                        {
                            found.UserName = sAMAccountName;
                            found.DisplayName = entity.GetAttribute("cn")?.StringValue;
                            found.Role = entity.GetAttribute("memberof")?.StringValue;
                            break;
                        }
                    }

                    Task<User> task = new Task<User>(() =>
                    {
                        return found;
                    });
                    task.Start();
                    return task;
                }
            }
            throw new CustomException(StatusCode.NOT_FOUND, "User Not Found");
        }

        public Task<string> CreateTokenJWT(User user)
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfig.ClaveSecreta)
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha512
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("nombre", user.UserName),
                new Claim("apellidos", user.DisplayName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: jwtConfig.Issuer,
                    audience: jwtConfig.Audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(1)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            Task<string> task = new Task<string>(() =>
            {
                return new JwtSecurityTokenHandler().WriteToken(_Token);
            });
            task.Start();
            return task;
        }
    }
}
