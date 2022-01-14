using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.DTO
{
    public class JwtConfig
    {
        public string ClaveSecreta { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
