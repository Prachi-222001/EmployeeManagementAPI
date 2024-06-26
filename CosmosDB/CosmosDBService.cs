using Azure;
using Emp_Management_SF.Common;
using Emp_Management_SF.Entity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Net;

namespace Emp_Management_SF.CosmosDB
{
    //implementation of interface providing methods to interact with cosmos
    public class CosmosDBService : ICosmosDB
    {
        //instance of cosmosclient to interact with cosmosdb
        public CosmosClient _cosmosClient;
        //instance of container to hold reference of specific container
        private readonly Container _container;

        //constructor to intialize the cosmosclient and container with credentials
        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);

        }
        //asynchronously adds a new employee to cosmosDB container
        public async Task<EmployeeBasicDetails> AddEmployee(EmployeeBasicDetails emp)
        {
            var data = await _container.CreateItemAsync(emp);
            return data;
        }
        public async Task<EmployeeBasicDetails> GetEmployeeById(string Id)
        {
         // Creates a LINQ query to filter the items in the Cosmos DB container
         // The query filters items to match the provided ID, ensure they are not archived, are active,
         // and have the document type specified for employees
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true)
                .Where(x => x.Id == Id && !x.Archived && x.Active && x.DocumentType == Credentials.EmployeeDocumentType).FirstOrDefault();
            return query;
        }
        public async Task<List<EmployeeBasicDetails>> GetAllBasicDetails()
        {
            // Retrieve IQueryable interface for EmployeeBasicDetails from Cosmos DB container
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true)
                // Apply filtering conditions using LINQ Where clause
                .Where(x => x.Active && !x.Archived && x.DocumentType == Credentials.EmployeeDocumentType).ToList();
            // Return the List<EmployeeBasicDetails> containing filtered results
            return response;
        }
        // Method to replace (update/delete) an employee record in Cosmos DB
        //public async Task ReplaceDelAsync(dynamic employee)
        //{
        //    await _container.ReplaceItemAsync(employee, employee.Id);
        //}
        public async Task<EmployeeAdditionalDetails> AddAdditionalDetails(EmployeeAdditionalDetails details)
        {
            //calls cosmosDB container to create new item with given empdetails
            var response = await _container.CreateItemAsync(details);
            //returns response which contains the added empdetails
            return response;
        }
        // Method to retrieve additional details for an employee by ID from Cosmos DB
        public async Task<EmployeeAdditionalDetails> AdditionalDetailsGetById(string Id)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true)
                                    .Where(x => x.Id == Id && !x.Archived && x.Active
                                                && x.DocumentType == Credentials.EmployeeDocumentType)
                                    .FirstOrDefault();
            return response;
        }

        // Method to retrieve all employees with additional details from Cosmos DB
        public async Task<List<EmployeeAdditionalDetails>> GetAllEmployees()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true)
                                    .Where(x => x.Active && !x.Archived
                                                && x.DocumentType == Credentials.EmployeeDocumentType)
                                    .ToList();
            return response;
        }
        public async Task<EmployeeAdditionalDetails> AdditionalDetailsById(string Id)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true)
                .Where(x => x.Id == Id && !x.Archived && x.Active && x.DocumentType == Credentials.EmployeeDocumentType)
                .FirstOrDefault();
            return response;

        }
        public async Task<List<EmployeeAdditionalDetails>> GetAllAdditional()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true)
                .Where(x => x.Active && !x.Archived && x.DocumentType == Credentials.EmployeeDocumentType)
                .ToList();
            return response;

        }
        public async Task ReplaceDelAsync(dynamic employee)
        {
            await _container.ReplaceItemAsync(employee, employee.Id);
        }

    }
}