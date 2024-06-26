using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Emp_Management_SF.Common;
using Emp_Management_SF.CosmosDB;
using Emp_Management_SF.Entity;
using Emp_Management_SF.Interface;
using Emp_Management_SF.ModelDTO;
using static Emp_Management_SF.Entity.EmployeeBasicDetails;
using static Emp_Management_SF.Entity.EmployeeAdditionalDetails;
using EmployeeManagementSystem.Common;
using Newtonsoft.Json;



namespace Emp_Management_SF.Services
{
    //implementation of interface providing methods to handle employee operations
    public class EmployeeService : IEmployeeInterface
    {
        //dependency injected service to interact with cosmosdb
        public readonly ICosmosDB _CosmosDB;
        //dependency injected AutoMapper to map entity and DTO obj
        public readonly IMapper _mapper;
        
        //constructor to initialize employeeservice 
        public EmployeeService(ICosmosDB cosmosDB,IMapper mapper)
        {
            _CosmosDB = cosmosDB;
            _mapper = mapper;
        }
        //asynchronously adds a new employee to cosmosDB and returns added employee
        public async Task<Model_Emp_BasicDetails> AddEmployee(Model_Emp_BasicDetails details)
        {
            var emp = _mapper.Map<EmployeeBasicDetails>(details);
            emp.Initialize(true, Credentials.EmployeeDocumentType, "Prachi", "Prachi");
            var response = await _CosmosDB.AddEmployee(emp);
            var model = _mapper.Map<Model_Emp_BasicDetails>(response);
            return model;
        }
        // Asynchronously retrieves an employee's basic details by their ID and maps it to a DTO model
        public async Task<Model_Emp_BasicDetails> GetEmployeeById(string Id)
        {
            //calls ICosmosDBService to get employee entity from cosmosDB container by id
            var entity = await _CosmosDB.GetEmployeeById(Id);
            //maps retrieved employee details by id
            return _mapper.Map<Model_Emp_BasicDetails>(entity);

        }
        public async Task<List<Model_Emp_BasicDetails>> GetAllBasicDetails()
        {
            //Calls the Cosmos DB service asynchronously to fetch all basic details
            var entity = await _CosmosDB.GetAllBasicDetails();
            //maps and returns the mapped list of DTO containing basic details
            return _mapper.Map<List<Model_Emp_BasicDetails>>(entity);

        }
        // Delete basic details for an employee
        public async Task<string> DeleteEmployee(string id)
        {
            try
            {
                // Retrieve the employee details by id
                var emp = await _CosmosDB.GetEmployeeById(id);

                if (emp == null)
                {
                    return "Employee not found";
                }

                // Update entity fields for soft deletion
                emp.Active = false;
                emp.Archived = true;

                // Replace entity in database to mark as archived
                await _CosmosDB.ReplaceDelAsync(emp);

                return "Employee deleted successfully";
            }
            catch (Exception ex)
            {
                // Log or handle exceptions appropriately
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }
        public async Task<Model_Emp_BasicDetails> UpdateEmployee(Model_Emp_BasicDetails employee)
        {
            var emp = await _CosmosDB.GetEmployeeById(employee.Id);

            // Update entity fields
            emp.Active = false;
            emp.Archived = true;

            // Replace entity in database
            await _CosmosDB.ReplaceDelAsync(employee);

            // Initialize entity with updated values
            emp.Initialize(false, Credentials.EmployeeDocumentType, "Prachi", "Prachi");
            emp.Active = false;

            // Map entity back to DTO
            var data = _mapper.Map<EmployeeBasicDetails>(emp);
            var response = await _CosmosDB.AddEmployee(data);
            var model = _mapper.Map<Model_Emp_BasicDetails>(response);

            return model;
        }
        //CRUD for additional details
        //Add Additionaldetails for employee
        public async Task<Model_Emp_AdditionalDetails> AddAdditionalDetails(Model_Emp_AdditionalDetails details)
        {
            // Step 1: Map the input DTO to entity
            var data = _mapper.Map<EmployeeAdditionalDetails>(details);

            // Step 2: Initialize the entity with specific values
            data.Initialize(true, Credentials.EmployeeDocumentType, "Prachi", "Prachi");

            // Step 3: Save entity to database using cosmosDB service
            var save = await _CosmosDB.AddAdditionalDetails(data);

            // Step 4: Map the saved entity back to DTO
            var model = _mapper.Map<Model_Emp_AdditionalDetails>(save);

            // Step 5: Return the DTO
            return model;
        }
        //retrieve additional details by ID
        public async Task<Model_Emp_AdditionalDetails> AdditionalDetailsGetById(string Id)
        {
            var details = await _CosmosDB.AdditionalDetailsGetById(Id);
            var data = _mapper.Map<Model_Emp_AdditionalDetails>(details);
            return data;
        }

       public async Task<List<Model_Emp_AdditionalDetails>> GetAllAdditional()
        {
            var data = await _CosmosDB.GetAllAdditional();
            var empList = new List<Model_Emp_AdditionalDetails>();
            foreach (var item in data)
            {
                var emp = _mapper.Map<Model_Emp_AdditionalDetails>(item);
                empList.Add(emp);
            }
            return empList;
        }
        public async Task<Model_Emp_AdditionalDetails> DeleteAdditionalDetails(string Id)
        {
            // Fetch the entity from the database by ID
            var data = await _CosmosDB.AdditionalDetailsGetById(Id);

            // Update entity fields for deletion
            data.Active = false;
            data.Archived = true;

            // Replace the entity in the database with the updated values
            await _CosmosDB.ReplaceDelAsync(data);

            // Initialize the entity with updated values
            data.Initialize(false, Credentials.EmployeeDocumentType, "Prachi", "Prachi");
            data.Active = false;

            // Save the updated entity to the database
            var updatedData = await _CosmosDB.AddAdditionalDetails(data);

            // Map the updated entity back to the DTO
            var model = _mapper.Map<Model_Emp_AdditionalDetails>(updatedData);

            // Return the updated DTO
            return model;
        }
        public async Task<Model_Emp_AdditionalDetails> UpdateAdditionalDetails(Model_Emp_AdditionalDetails details)
        {
            var data = await _CosmosDB.AdditionalDetailsGetById(details.Id);
            data.Active = false;
            data.Archived = true;
            await _CosmosDB.ReplaceDelAsync(data);
            data.Initialize(false, Credentials.EmployeeDocumentType, "Prachi", "Prachi");
            data.Active = false;

            var dt = _mapper.Map<EmployeeAdditionalDetails>(data);
            var save = await _CosmosDB.AddAdditionalDetails(dt);
            var model = _mapper.Map<Model_Emp_AdditionalDetails>(save);
            return model;
        }
        public async Task<List<Model_Emp_BasicDetails>> GetAllEmployeeByNickName(string nickName)
        {
            var allEmployees = await GetAllBasicDetails();
            return allEmployees.FindAll(a => a.NickName == nickName);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria responseObject = new EmployeeFilterCriteria();

            //filter ==>status
            var checkFilter = employeeFilterCriteria.Filters.Any(a => a.FieldName == "status");

            //var
            var status = "";
            if (checkFilter)
            {
                status = employeeFilterCriteria.Filters.Find(a => a.FieldName == "status").FieldValue;
            }

            var employess = await GetAllBasicDetails();

            var filterRecords = employess.FindAll(a => a.Status == status);

            responseObject.TotalCount = employess.Count;
            responseObject.Page = employeeFilterCriteria.Page;
            responseObject.PageSize = employeeFilterCriteria.PageSize;

            var skip = employeeFilterCriteria.PageSize * (employeeFilterCriteria.Page - 1);

            filterRecords = filterRecords.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();
            foreach (var item in employess)
            {
                responseObject.employees.Add(item);
            }
            return responseObject;
        }
        public async Task<EmployeeAdditionalFilterCriteria> GetAllAdditionalDetailsbyPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria)
        {

            EmployeeAdditionalFilterCriteria responseObject = new EmployeeAdditionalFilterCriteria();

            //filter ==>status
            var checkFilter = employeeAdditionalFilterCriteria.Filters.Any(a => a.FieldName == "status");

            //var
            var status = "";
            if (checkFilter)
            {
                status = employeeAdditionalFilterCriteria.Filters.Find(a => a.FieldName == "status").FieldValue;
            }

            var emp = await GetAllAdditional();

            var filterRecords = emp.FindAll(a => a.Status == status);

            responseObject.TotalCount = emp.Count;
            responseObject.Page = employeeAdditionalFilterCriteria.Page;
            responseObject.PageSize = employeeAdditionalFilterCriteria.PageSize;

            var skip = employeeAdditionalFilterCriteria.PageSize * (employeeAdditionalFilterCriteria.Page - 1);

            filterRecords = filterRecords.Skip(skip).Take(employeeAdditionalFilterCriteria.PageSize).ToList();
            foreach (var item in emp)
            {
                responseObject.Employees.Add(item);
            }
            return responseObject;
        }
        public async Task<Model_Emp_BasicDetails> AddBasicDetailsByMakePostRequest(Model_Emp_BasicDetails details)
        {
            var serializable = JsonConvert.SerializeObject(details);
            var request = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddBasicDetailsEndPoint, serializable);
            var model = JsonConvert.DeserializeObject<Model_Emp_BasicDetails>(request);
            return model;
        }
        public async Task<List<Model_Emp_BasicDetails>> GetBasicDetailsByMakeGetRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetAllBasicDetailsEndPoint);
            return JsonConvert.DeserializeObject<List<Model_Emp_BasicDetails>>(request);
        }
        public async Task<Model_Emp_AdditionalDetails> AddAdditionalByMakePostRequest(Model_Emp_AdditionalDetails details)
        {
            var serializable = JsonConvert.SerializeObject(details);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddAdditionalEndPoint, serializable);
            var model = JsonConvert.DeserializeObject<Model_Emp_AdditionalDetails>(requestObj);
            return model;
        }
        public async Task<List<Model_Emp_AdditionalDetails>> GetEmployeeAdditionalByMakeRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetEmployeeEndPoint);
            return JsonConvert.DeserializeObject<List<Model_Emp_AdditionalDetails>>(request);
        }


    }

}

