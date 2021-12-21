using System;

namespace MC.Insurance.DTO
{
    public class CustomerInsurance
	{
		public int customerInsuranceId { get; set; }
		public string document { get; set; }
		public string customerName { get; set; }


		public int insuranceId { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int type { get; set; }
		public string coverage { get; set; }
		public DateTime start { get; set; }
		public int period { get; set; }
		public float price { get; set; }
		public int risk { get; set; }
	}
}
