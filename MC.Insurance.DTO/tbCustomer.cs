using System.ComponentModel.DataAnnotations;

namespace MC.Insurance.DTO
{
    public class TbCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Document { get; set; }
        public string CustomerName { get; set; }
    }
}
