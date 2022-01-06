using MC.Insurance.DTO;
using System.Collections;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Domain
{
    public interface IInsuranceDomain
    {
        Task<IEnumerable> GetInsurances();
        Task<DTO.Insurance> GetInsuranceByID(int insuranceId);
        Task<string> InsertInsurance(DTO.Insurance insurance);
        Task<string> DeleteInsurance(int insuranceId);
        Task<string> UpdateInsurance(int insuranceId, DTO.Insurance insurance);

        Task<IEnumerable> GetCustomers();
        Task<IEnumerable> GetCustomerByID(string document);
        Task<string> InsertCustomerInsurance(string document, DTO.CustomerInsurance customerInsurance);
        Task<string> DeleteCustomerInsurance(string document, int insuranceId);

        DTO.Insurance AsignCoverage(DTO.Insurance add);
        DTO.Insurance UpdateInsuraceId(int insuranceId, DTO.Insurance add);
        DTO.CustomerInsurance UpdateValues(string document, DTO.CustomerInsurance add);

        Response CreateResponse(Enumerations.StatusCode StatusCode, object Response);

        Task<User> Login(string userName, string password);
        Task<string> CreateTokenJWT(DTO.User user);
    }
}
