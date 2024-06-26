using AutoMapper;
using Emp_Management_SF.Entity;
using Emp_Management_SF.ModelDTO;

namespace Emp_Management_SF.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<EmployeeBasicDetails, Model_Emp_BasicDetails>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails, Model_Identity_Info>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails, Model_Personal_Details>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails, Model_Work_Info>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails, Model_Emp_AdditionalDetails>().ReverseMap();
           

        }
    }
}
