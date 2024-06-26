namespace Emp_Management_SF.Common
{
    public class Credentials
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");

        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName");

        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");

        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");

        //Document Type
        public static readonly string EmployeeDocumentType = "Employee";
        public static readonly string EmployeeUrl = Environment.GetEnvironmentVariable("employeeUrl");

       
        //self api call in additionaldetails
        internal static readonly string AddBasicDetailsEndPoint = "/api/Employee/AddEmployee";

        internal static readonly string GetAllBasicDetailsEndPoint = "/api/Employee/GetAllBasicDetails";
        public static readonly string AddAdditionalEndPoint = "/api/Employee/AddAdditionalDetails";
        public static readonly string GetEmployeeEndPoint = "/api/Employee/GetAllAdditional";
    }
}
