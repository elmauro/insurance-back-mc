using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Interfaces.Application
{
	public interface IInsuranceFormatInputOutput
	{
		IInsuranceDomain InsuranceDomain { get; set; }
		ISerializer Serializer { get; set; }

		ExternalResponse GetInsuranceFormatted(ExternalResponse result);
		ExternalResponse GetInsurancesFormatted(ExternalResponse result);
	}
}
