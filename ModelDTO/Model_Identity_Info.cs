using Newtonsoft.Json;

namespace Emp_Management_SF.ModelDTO
{
    public class Model_Identity_Info
    {
        [JsonProperty(propertyName: "pan", NullValueHandling = NullValueHandling.Ignore)]
        public string PAN { get; set; }

        [JsonProperty(propertyName: "aadhar", NullValueHandling = NullValueHandling.Ignore)]
        public string Aadhar { get; set; }

        [JsonProperty(propertyName: "nationality", NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }

        [JsonProperty(propertyName: "passportNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }

        [JsonProperty(propertyName: "pfNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PFNumber { get; set; }
    }
}
