using Newtonsoft.Json;

namespace Emp_Management_SF.ModelDTO
{
    public class Model_Work_Info
    {
        [JsonProperty(propertyName: "designationName", NullValueHandling = NullValueHandling.Ignore)]
        public string DesignationName { get; set; }

        [JsonProperty(propertyName: "departmentName", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentName { get; set; }

        [JsonProperty(propertyName: "locationName", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationName { get; set; }

        [JsonProperty(propertyName: "employeeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeStatus { get; set; } // Terminated, Active, Resigned etc

        [JsonProperty(propertyName: "sourceOfHire", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceOfHire { get; set; }

        [JsonProperty(propertyName: "dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfJoining { get; set; }
    }
}
