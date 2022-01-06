using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.DTO
{
    public class JWTConfig
    {
        public string ClaveSecreta { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
