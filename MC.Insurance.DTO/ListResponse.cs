using System.Collections.Generic;

namespace MC.Insurance.DTO
{
	public class ListResponse<T>
	{
		public List<T> List { get; set; }
	}
}
