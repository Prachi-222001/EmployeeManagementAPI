using Emp_Management_SF.Common;
using Emp_Management_SF.ModelDTO;
using Newtonsoft.Json;
using static Emp_Management_SF.Entity.EmployeeBasicDetails;

namespace Emp_Management_SF.Entity
{
    public class EmployeeAdditionalDetails : BaseEntity
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

    public class EmployeeAdditionalFilterCriteria
    {
        public EmployeeAdditionalFilterCriteria()
        {

            Filters = new List<FilterCriteria>();
            Employees = new List<Model_Emp_AdditionalDetails>();
        }
        public int Page { get; set; } //page number

        public int PageSize { get; set; }//num of record in 1 page
        public int TotalCount { get; set; }// total record present in the data
        public List<FilterCriteria> Filters { get; set; }//Pass Filter

        public List<Model_Emp_AdditionalDetails> Employees { get; set; }


    }


    public class FilterCriteriaAdditional
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }

}


