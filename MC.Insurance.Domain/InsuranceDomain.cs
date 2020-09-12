using MC.Insurance.Interfaces.Domain;
using MC.Insurance.DTO;
using System;

namespace MC.Insurance.Domain
{
	public class InsuranceDomain: IInsuranceDomain
	{
		public InsuranceDomain() { }

		public DTO.Insurance AsignCoverage(DTO.Insurance add)
		{
			if (add.risk == 4) {
				add.coverage = "50%";
			}

			return add;
		}
	}
}
