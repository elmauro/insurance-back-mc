namespace MC.Insurance.Interfaces.Domain
{
    public interface IInsuranceDomain
	{
		DTO.Insurance AsignCoverage(DTO.Insurance add);

		DTO.Insurance UpdateInsuraceId(int insuranceId, DTO.Insurance add);
		DTO.CustomerInsurance UpdateValues(string document, DTO.CustomerInsurance add);
	}
}
