using System;

namespace MC.Insurance.DTO
{
	public class ExternalResponse
	{
		public bool IsSuccessStatusCode { get; set; }
		public int StatusCode { get; set; }
		public string Body { get; set; }
	}
}
