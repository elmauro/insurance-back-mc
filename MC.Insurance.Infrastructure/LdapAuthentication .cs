using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MC.Insurance.Infrastructure
{
    public class LdapAuthentication : IAuthenticationService
    {
        private readonly LdapConfig config;

        public LdapAuthentication(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }
        public Task<User> Login(string userName, string password)
        {
            string userDn = $"uid={userName},{config.UserDomainName}";
            try
            {
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
            }
            catch (LdapException ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
