using System.Collections.Generic;

namespace MC.Insurance.DTO
{
    public class CustomerResponses
	{
		public List<Customer> Customers { get; set; }
	}

	public class Customer
	{
		public string document { get; set; }
		public string name { get; set; }
	}
}
