using Emp_Management_SF.Entity;
using Emp_Management_SF.ModelDTO;
using static Emp_Management_SF.Entity.EmployeeBasicDetails;

namespace Emp_Management_SF.Interface
{
    public interface IEmployeeInterface
    {
        Task<Model_Emp_BasicDetails> AddEmployee(Model_Emp_BasicDetails details);
        Task<Model_Emp_BasicDetails> GetEmployeeById(string Id);
        Task<List<Model_Emp_BasicDetails>> GetAllBasicDetails();
        Task<string> DeleteEmployee(string Id);
        Task<Model_Emp_BasicDetails> UpdateEmployee(Model_Emp_BasicDetails employee);
        Task<Model_Emp_AdditionalDetails> AddAdditionalDetails(Model_Emp_AdditionalDetails details);
        Task<Model_Emp_AdditionalDetails> AdditionalDetailsGetById(string Id);
        Task<List<Model_Emp_AdditionalDetails>> GetAllAdditional();
        Task<Model_Emp_AdditionalDetails> DeleteAdditionalDetails(string Id);
        Task<Model_Emp_AdditionalDetails> UpdateAdditionalDetails(Model_Emp_AdditionalDetails details);
        Task<List<Model_Emp_BasicDetails>> GetAllEmployeeByNickName(string nickName);
        Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria);
        Task<EmployeeAdditionalFilterCriteria> GetAllAdditionalDetailsbyPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria);
        Task<Model_Emp_BasicDetails> AddBasicDetailsByMakePostRequest(Model_Emp_BasicDetails details);
        Task<List<Model_Emp_BasicDetails>> GetBasicDetailsByMakeGetRequest();
        Task<Model_Emp_AdditionalDetails> AddAdditionalByMakePostRequest(Model_Emp_AdditionalDetails details);
       Task <List<Model_Emp_AdditionalDetails>> GetEmployeeAdditionalByMakeRequest();



    }
}
