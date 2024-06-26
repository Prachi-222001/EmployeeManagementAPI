using Emp_Management_SF.Common;
using Emp_Management_SF.Entity;
using Newtonsoft.Json;

namespace Emp_Management_SF.ModelDTO
{
    public class Model_Emp_AdditionalDetails : BaseEntity
    {
        [JsonProperty(propertyName: "employeeBasicDetailsUId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(propertyName: "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }

        [JsonProperty(propertyName: "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }

        [JsonProperty(propertyName: "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty(propertyName: "personalDetails", NullValueHandling = NullValueHandling.Ignore)]
        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty(propertyName: "identityInformation", NullValueHandling = NullValueHandling.Ignore)]
        public IdentityInfo_ IdentityInformation { get; set; }
        [JsonProperty(propertyName: "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

    }
}
