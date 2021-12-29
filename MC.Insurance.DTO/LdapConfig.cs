using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.DTO
{
    public class LdapConfig
    {
        public string Path { get; set; }
        public int Port { get; set; }
        public string UserDomainName { get; set; }
    }
}
