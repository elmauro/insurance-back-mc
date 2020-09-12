using System;

namespace MC.Insurance.Interfaces.Domain
{
	public interface IInsuranceDomain
	{
		DTO.Insurance AsignCoverage(DTO.Insurance add);
	}
}
