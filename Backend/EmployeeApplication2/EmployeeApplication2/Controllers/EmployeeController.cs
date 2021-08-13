using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string connectionString = @"Data Source = DESKTOP-EIQF54C\SQL2014;initial catalog=EmployeeDatabase;user id=sa;password=tommya";
       SqlConnection connection = new SqlConnection();
       /* [HttpPost("save")]
        public async Task<IEnumerable<Employee>> InsertEmployeeAsync(Employee newEmployee)
         {
             using (var connection = new SqlConnection(connectionString))
             {
                 DynamicParameters para = new DynamicParameters();
                 para.Add("@ID", newEmployee.id);
                 para.Add("@CODE", newEmployee.code);
                 para.Add("@NAME", newEmployee.name);
                 para.Add("@EMPLOYEETYPE", newEmployee.employeeType);
                 para.Add("@EMAIL", newEmployee.email);
                 para.Add("@DOB", newEmployee.dob);
                 para.Add("@GENDER", newEmployee.gender);
                 para.Add("@ACTIVE", newEmployee.active);

                 var results = await connection.QueryAsync<Employee>("[dbo].[InsertEmployeeDetails]", para, commandType: CommandType.StoredProcedure);

                 return results;

             }
         }*/
        [HttpPost("save")]
        public string InsertEmployee(Employee newEmployee)
        {
            connection.ConnectionString = connectionString;

            DynamicParameters para = new DynamicParameters();
           
            para.Add("@CODE", newEmployee.code);
            para.Add("@NAME", newEmployee.name);
            para.Add("@EMPLOYEETYPE", newEmployee.employeeType);
            para.Add("@EMAIL", newEmployee.email);
            para.Add("@DOB", newEmployee.dob);
            para.Add("@GENDER", newEmployee.gender);
            para.Add("@ACTIVE", newEmployee.active);

            var result = connection.Query<Employee>("InsertEmployeeDetails", para, commandType: CommandType.StoredProcedure);
            return "A new user was saved";

        }
        [HttpGet]
        public IEnumerable<Employee> GetEmployeeList()
        {
            connection.ConnectionString = connectionString;

            var result = connection.Query<Employee>("GetEmployeeDetails", commandType: CommandType.StoredProcedure);

            return result;

        }

        [HttpGet("{id}")]
        public IEnumerable<Employee> GetEmployeeById(int id)
        {
            connection.ConnectionString = connectionString;

            var para = new DynamicParameters();
            para.Add("@ID", id);

            var result = connection.Query<Employee>("GetEmployeeById", para, commandType: CommandType.StoredProcedure);

            return result;

        }

        [HttpDelete("remove/{id}")]
        public string RemoveEmployee(int id)
        {
            connection.ConnectionString = connectionString;

            var param = new DynamicParameters();
            param.Add("@ID", id);

            var result = connection.Query<Employee>("RemoveEmployee", param, commandType: CommandType.StoredProcedure);

            return "Successfully removed the employee with id " + id;
        }

        [HttpPut("update/{id}")]
        public string UpdateEmployee(int id, Employee employee)
        {
            connection.ConnectionString = connectionString;

            var para = new DynamicParameters();
            para.Add("@ID", id);
            para.Add("@CODE", employee.code);
            para.Add("@NAME", employee.name);
            para.Add("@EMPLOYEETYPE", employee.employeeType);
            para.Add("@EMAIL", employee.email);
            para.Add("@DOB", employee.dob);
            para.Add("@GENDER", employee.gender);
            para.Add("@ACTIVE", employee.active);

            var result = connection.Query<Employee>("UpdateEmployee", para, commandType: CommandType.StoredProcedure);

            return "Successfully updated employee with id " + id;
        }


    }
}
public class Employee
{
    public int id;
    public string code;
    public string name;
    public string employeeType;
    public string email;
    public DateTime dob;
    public string gender;
    public bool active;
}