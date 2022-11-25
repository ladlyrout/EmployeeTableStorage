using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace EmployeeTableStorage
{
    public class EmployeeModel : TableEntity
    {
        public EmployeeModel(string Department, string EmpId)
        {
            this.PartitionKey = Department; this.RowKey = EmpId;
        }
        public EmployeeModel() { }

        public string EmpId { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
