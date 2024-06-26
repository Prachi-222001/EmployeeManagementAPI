using Emp_Management_SF.Entity;
using Emp_Management_SF.Interface;
using Emp_Management_SF.ModelDTO;
using Emp_Management_SF.ServiceFilter;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using static Emp_Management_SF.Entity.EmployeeBasicDetails;

namespace Emp_Management_SF.Controllers
{
    //specifies that this class is controller
    [ApiController]
    //defines route for controller actions
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        //dependency injected to handle employee operations
        private readonly IEmployeeInterface _employeeService;

        //constructor to initialize employeecontroller
        public EmployeeController(IEmployeeInterface employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        public async Task<Model_Emp_BasicDetails> AddEmployee(Model_Emp_BasicDetails details)
        {
            //calls service layer to add employee and awaits result
            var response = await _employeeService.AddEmployee(details);
            //returns details of added employee
            return response;

        }
        [HttpGet]
        //asynchronously retrieves employeebasicdetails from service by id
        public async Task<Model_Emp_BasicDetails> GetEmployeeById(string Id)
        {
            //calls employee service to get details by id
            var response = await _employeeService.GetEmployeeById(Id);
            //returns the response
            return response;
        }
        // HTTP GET method to retrieve all basic details of employees asynchronously
        [HttpGet]
        public async Task<List<Model_Emp_BasicDetails>> GetAllBasicDetails()
        {
            //calls employeeService asyncs to fetch all basic details
            var response = await _employeeService.GetAllBasicDetails();
            //returns the fetched response containing a list of Model_Emp_BasicDetails
            return response;
        }
        [HttpPost]
        // Endpoint to soft-delete an employee by ID

        public async Task<string> DeleteEmployee(string Id)
        {
            var response = await _employeeService.DeleteEmployee(Id);
            return response;
        }
        [HttpPost]
        public async Task<Model_Emp_BasicDetails> UpdateEmployee(Model_Emp_BasicDetails employee)
        {
            var response = await _employeeService.UpdateEmployee(employee);
            return response;
        }
        //crud for additional details
        [HttpPost]
        public async Task<Model_Emp_AdditionalDetails> AddAdditionalDetails(Model_Emp_AdditionalDetails details)
        {
            var response = await _employeeService.AddAdditionalDetails(details);
            return response;
        }

        [HttpGet]
        public async Task<Model_Emp_AdditionalDetails> AdditionalDetailsGetById(string Id)
        {
            var response = await _employeeService.AdditionalDetailsGetById(Id);
            return response;
        }
        [HttpGet]
        public async Task<List<Model_Emp_AdditionalDetails>> GetAllAdditional()
        {
            var response = await _employeeService.GetAllAdditional();
            return response;
        }
        [HttpPost]
        public async Task<Model_Emp_AdditionalDetails> DeleteAdditionalDetails(string Id)
        {
            var response = await _employeeService.DeleteAdditionalDetails(Id);
            return response;
        }
        [HttpPost]
        public async Task<Model_Emp_AdditionalDetails> UpdateAdditionalDetails(Model_Emp_AdditionalDetails details)
        {
            var response = await _employeeService.UpdateAdditionalDetails(details);
            return response;

        }
        //for Excel

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            // Check if the uploaded file is null or empty
            if (file == null || file.Length == 0)
                return BadRequest("file is empty or null");

            // Initialize a list to hold employee details
            var employees = new List<Model_Emp_BasicDetails>();

            // Set the license context for EPPlus library to non-commercial
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Use a memory stream to process the uploaded file
            using (var stream = new MemoryStream())
            {
                // Copy the uploaded file to the memory stream
                file.CopyTo(stream);

                // Load the memory stream into an Excel package
                using (var package = new ExcelPackage(stream))
                {
                    // Get the first worksheet in the Excel workbook
                    var worksheet = package.Workbook.Worksheets[0];

                    // Get the total number of rows in the worksheet
                    var rowcount = worksheet.Dimension.Rows;

                    // Iterate through each row in the worksheet
                    for (int row = 1; row <= rowcount; row++)
                    {
                        // Create a new employee object and populate its properties from the worksheet
                        var emp = new Model_Emp_BasicDetails
                        {
                            FirstName = GetStringFroceCell(worksheet, row, 1),
                            LastName = GetStringFroceCell(worksheet, row, 2),
                            Email = GetStringFroceCell(worksheet, row, 3),
                            Mobile = GetStringFroceCell(worksheet, row, 4),
                            ReportingManagerName = GetStringFroceCell(worksheet, row, 5)
                        };

                        // Add the employee to the database (asynchronously)
                        await AddEmployee(emp);

                        // Add the employee to the list
                        employees.Add(emp);
                    }
                }
            }

            // Return the list of imported employees
            return Ok(employees);
        }

        // Helper method to get a trimmed string value from a specific cell in the worksheet
        private string? GetStringFroceCell(ExcelWorksheet worksheet, int row, int column)
        {
            // Get the value from the specified cell
            var cellValue = worksheet.Cells[row, column].Value;

            // Return the cell value as a trimmed string, or null if the cell is empty
            return cellValue?.ToString()?.Trim();
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            // Retrieve all basic employee details from the service
            var employees = await _employeeService.GetAllBasicDetails();

            // Set the license context for EPPlus library to non-commercial
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet named "employees" to the workbook
                var worksheet = package.Workbook.Worksheets.Add("employees");

                // Add headers to the worksheet
                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Last Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone Number";
                worksheet.Cells[1, 5].Value = "Reporting Manager Name";

                // Apply style to the header row
                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                }

                // Add employee details to the worksheet
                for (int i = 0; i < employees.Count; i++)
                {
                    var emp = employees[i];
                    worksheet.Cells[i + 2, 1].Value = emp.FirstName;
                    worksheet.Cells[i + 2, 2].Value = emp.LastName;
                    worksheet.Cells[i + 2, 3].Value = emp.Email;
                    worksheet.Cells[i + 2, 4].Value = emp.Mobile;
                    worksheet.Cells[i + 2, 5].Value = emp.ReportingManagerName;
                }

                // Save the Excel package to a memory stream
                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);

                // Reset the position of the memory stream to the beginning
                stream.Position = 0;

                // Define the file name for the Excel file
                var fileName = "EmployeeDetails.xlsx";

                // Return the Excel file as a downloadable file
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ExportAll()
        {
            // Retrieve all basic and additional employee details from the service
            var basicDetails = await _employeeService.GetAllBasicDetails();
            var additionalDetails = await _employeeService.GetAllAdditional();

            // Set the license context for EPPlus library to non-commercial
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Add a new worksheet named "employees" to the workbook
                var worksheet = package.Workbook.Worksheets.Add("employees");

                // Add headers to the worksheet for basic and additional details
                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Last Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone Number";
                worksheet.Cells[1, 5].Value = "Reporting Manager Name";
                worksheet.Cells[1, 6].Value = "Alternate Email";
                worksheet.Cells[1, 7].Value = "Alternate Mobile";
                worksheet.Cells[1, 8].Value = "Status";
                worksheet.Cells[1, 9].Value = "Address"; // Assuming basic detail includes address

                // Apply style to the header row
                using (var range = worksheet.Cells[1, 1, 1, 9])
                {
                    range.Style.Font.Bold = true;
                }

                // Add employee details to the worksheet
                for (int i = 0; i < basicDetails.Count; i++)
                {
                    var basic = basicDetails[i];
                    var additional = additionalDetails.FirstOrDefault(a => a.EmployeeBasicDetailsUId == basic.EmployeeID); // Assuming EmployeeID is the common field

                    worksheet.Cells[i + 2, 1].Value = basic.FirstName;
                    worksheet.Cells[i + 2, 2].Value = basic.LastName;
                    worksheet.Cells[i + 2, 3].Value = basic.Email;
                    worksheet.Cells[i + 2, 4].Value = basic.Mobile;
                    worksheet.Cells[i + 2, 5].Value = basic.ReportingManagerName;
                    worksheet.Cells[i + 2, 6].Value = additional?.AlternateEmail; // Additional detail
                    worksheet.Cells[i + 2, 7].Value = additional?.AlternateMobile; // Additional detail
                    worksheet.Cells[i + 2, 8].Value = additional?.Status; // Additional detail
                    worksheet.Cells[i + 2, 9].Value = basic.Address; // Basic detail includes address
                }

                // Save the Excel package to a memory stream
                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);

                // Reset the position of the memory stream to the beginning
                stream.Position = 0;

                // Define the file name for the Excel file
                var fileName = "EmployeeDetails.xlsx";

                // Return the Excel file as a downloadable file
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        //Search and Pagination
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeByNickname(string nickName)
        {
            var response = await _employeeService.GetAllEmployeeByNickName(nickName);
            return Ok(response);
        }
        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await _employeeService.GetAllEmployeeByPagination(employeeFilterCriteria);
            return response;
        }
        [HttpGet]
        [ServiceFilter(typeof(BuildEmployeeAdditionalFilter))]
        public async Task<EmployeeAdditionalFilterCriteria> GetAllAdditionalDetailsbyPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria)
        {
            var response = await _employeeService.GetAllAdditionalDetailsbyPagination(employeeAdditionalFilterCriteria);
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> AddBasicDetailsByMakePostRequest(Model_Emp_BasicDetails details)
        {
            var response = await _employeeService.AddBasicDetailsByMakePostRequest(details);
            return Ok(response);
        }
        [HttpGet]
        public async Task<List<Model_Emp_BasicDetails>> GetBasicDetailsByMakeGetRequest()
        {
            return await _employeeService.GetBasicDetailsByMakeGetRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdditionalByMakePostRequest(Model_Emp_AdditionalDetails details)
        {
            var response = await _employeeService.AddAdditionalByMakePostRequest(details);
            return Ok(response);
        }
        [HttpGet]
        public async Task<List<Model_Emp_AdditionalDetails>> GetEmployeeAdditionalByMakeRequest()
        {
            return await _employeeService.GetEmployeeAdditionalByMakeRequest();
        }






    }
}
