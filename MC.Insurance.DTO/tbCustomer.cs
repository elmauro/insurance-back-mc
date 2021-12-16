using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MC.Insurance.DTO
{
    public class tbCustomer
    {
        [Key]
        public int customerId { get; set; }
        public string document { get; set; }
        public string customerName { get; set; }
    }
}
