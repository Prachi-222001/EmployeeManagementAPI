using DocumentFormat.OpenXml.Math;
using Emp_Management_SF.Entity;

namespace Emp_Management_SF.CosmosDB
{
    public interface ICosmosDB
    {
        Task<EmployeeBasicDetails> AddEmployee(EmployeeBasicDetails details);
        Task<EmployeeBasicDetails> GetEmployeeById(string Id);
        Task<List<EmployeeBasicDetails>> GetAllBasicDetails();
        Task ReplaceDelAsync(dynamic employee);
        Task<EmployeeAdditionalDetails> AddAdditionalDetails(EmployeeAdditionalDetails details);
        Task<EmployeeAdditionalDetails> AdditionalDetailsGetById(string Id);
        Task<List<EmployeeAdditionalDetails>> GetAllAdditional();
        

       
    }
}
