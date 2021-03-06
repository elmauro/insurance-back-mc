﻿using MC.Insurance.Interfaces.Domain;
using MC.Insurance.DTO;
using System;
using System.Globalization;

namespace MC.Insurance.Domain
{
	public class InsuranceDomain: IInsuranceDomain
	{
		public InsuranceDomain() { }

		public DTO.Insurance AsignCoverage(DTO.Insurance add)
		{
			if (add.risk == 4)
			{
				add.coverage = "50%";
			}
			else {
				add.coverage = "100%";
			}

			return add;
		}

		public DTO.Insurance UpdateInsuraceId(int insuranceId, DTO.Insurance add)
		{
			add.insuranceId = insuranceId;
			return add;
		}

		public CustomerInsurance UpdateValues(string document, CustomerInsurance add)
		{
			add.document = document;
			add.start = DateTime.Now;

			return add;
		}
	}
}
