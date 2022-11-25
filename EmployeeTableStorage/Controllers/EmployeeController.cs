using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmployeeTableStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration config;
        public EmployeeController(IConfiguration configuration)
        {
            config = configuration;
        }
        [HttpGet("Department")]
      
        public IEnumerable<EmployeeModel> Get(string Department)
        {

            var condition = TableQuery.GenerateFilterCondition("Department", QueryComparisons.Equal, Department);
            var query = new TableQuery<EmployeeModel>().Where(condition);

            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("EmployeeTable");
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
        }
        [HttpGet("LastName")]

        public IEnumerable<EmployeeModel> GetLastNames(string LastName)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("EmployeeTable");
            
            var condition = TableQuery.GenerateFilterCondition("LastName", QueryComparisons.Equal, LastName);
            var query = new TableQuery<EmployeeModel>().Where(condition);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
        }
        [HttpGet("FirstName")]

        public IEnumerable<EmployeeModel> GetFirstNames(string FirstName)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("EmployeeTable");

            var condition = TableQuery.GenerateFilterCondition("FirstName", QueryComparisons.Equal, FirstName);
            var query = new TableQuery<EmployeeModel>().Where(condition);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
        }
        [HttpPost]
        public IEnumerable<EmployeeModel> Post([FromBody] EmployeeModel emp)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference("EmployeeTable");

            table.CreateIfNotExists();

            EmployeeModel employeeEntity = new EmployeeModel(emp.Department, emp.EmpId);
            employeeEntity.Department = emp.Department;
            employeeEntity.EmpId = emp.EmpId;
            employeeEntity.FirstName = emp.FirstName;
            employeeEntity.LastName = emp.LastName;
            employeeEntity.PhoneNumber = emp.PhoneNumber;
            employeeEntity.Email = emp.Email;
            var query = new TableQuery<EmployeeModel>();
            TableOperation insertOperation = TableOperation.Insert(employeeEntity);


            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }
        [HttpDelete]
        public IEnumerable<EmployeeModel> Delete([FromBody] EmployeeModel emp)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("EmployeeTable");
            table.CreateIfNotExists();
            EmployeeModel employeeEntity = new EmployeeModel(emp.Department, emp.EmpId);
            employeeEntity.Department = emp.Department;
            employeeEntity.EmpId = emp.EmpId;
            employeeEntity.FirstName = emp.FirstName;
            employeeEntity.LastName = emp.LastName;
            employeeEntity.PhoneNumber = emp.PhoneNumber;
            employeeEntity.Email = emp.Email;
            employeeEntity.ETag = "*"; // wildcard 
            var query = new TableQuery<EmployeeModel>();
            TableOperation insertOperation = TableOperation.Delete(employeeEntity);
            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }
        [HttpPut]
        public IEnumerable<EmployeeModel> Put([FromBody] EmployeeModel emp)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference("EmployeeTable");

            table.CreateIfNotExists();

            EmployeeModel employeeEntity = new EmployeeModel(emp.Department, emp.EmpId);
            employeeEntity.Department = emp.Department;
            employeeEntity.EmpId = emp.EmpId;
            employeeEntity.LastName = emp.LastName;
            employeeEntity.PhoneNumber = emp.PhoneNumber;
            employeeEntity.Email = emp.Email;
            var query = new TableQuery<EmployeeModel>();
            TableOperation insertOperation = TableOperation.InsertOrMerge(employeeEntity);
            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }
    }
}
