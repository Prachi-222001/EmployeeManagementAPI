using Emp_Management_SF.Common;
using Newtonsoft.Json;

namespace Emp_Management_SF.ModelDTO
{
    public class Model_Emp_BasicDetails : BaseEntity
    {
        [JsonProperty(propertyName: "salutary", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(propertyName: "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(propertyName: "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(propertyName: "nickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        [JsonProperty(propertyName: "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(propertyName: "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(propertyName: "employeeId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeID { get; set; }

        [JsonProperty(propertyName: "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(propertyName: "reportingManagerUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerUId { get; set; }

        [JsonProperty(propertyName: "reportingManagerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerName { get; set; }

        [JsonProperty(propertyName: "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(propertyName: "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get;  set; }
    }
}
