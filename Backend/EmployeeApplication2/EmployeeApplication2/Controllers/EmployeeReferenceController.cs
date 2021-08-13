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
    public class EmployeeReferenceController : ControllerBase
    {
        string connectionString = @"Data Source = DESKTOP-EIQF54C\SQL2014;initial catalog=EmployeeDatabase;user id=sa;password=tommya";
        SqlConnection connection = new SqlConnection();

        /*[HttpPost("save")]
        public async Task<IEnumerable<EmployeeReference>> InsertEmployeeRef(EmployeeReference newEmployeeRef)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@ID", newEmployeeRef.id);
                para.Add("@CODE", newEmployeeRef.code);
                para.Add("@DESCRIPTION", newEmployeeRef.description);

                var results = await connection.QueryAsync<EmployeeReference>("[dbo].[InsertEmployeeReference]", para, commandType: CommandType.StoredProcedure);

                return results;

            }
        }*/
        [HttpPost("save")]
        public string InsertEmployee(EmployeeReference newEmployeeRef)
        {
            connection.ConnectionString = connectionString;

            DynamicParameters para = new DynamicParameters();
            
            para.Add("@CODE", newEmployeeRef.code);
            para.Add("@DESCRIPTION", newEmployeeRef.description);

            var result = connection.Query<EmployeeReference>("InsertEmployeeReference", para, commandType: CommandType.StoredProcedure);

            return "A new employee reference was saved";

        }
        [HttpGet]
        public IEnumerable<EmployeeReference> GetEmployeeRefList()
        {
            connection.ConnectionString = connectionString;

            var result = connection.Query<EmployeeReference>("GetEmployeeRef", commandType: CommandType.StoredProcedure);

            return result;

        }
        [HttpGet("{id}")]
        public IEnumerable<EmployeeReference> GetEmployeeRefById(int id)
        {
            connection.ConnectionString = connectionString;

            var para = new DynamicParameters();
            para.Add("@ID", id);

            var result = connection.Query<EmployeeReference>("GetEmployeeRefById", para, commandType: CommandType.StoredProcedure);

            return result;

        }
        [HttpDelete("remove/{id}")]
        public string RemoveEmployeeRef(int id)
        {
            connection.ConnectionString = connectionString;

            var para= new DynamicParameters();
            para.Add("@ID", id);

            var result = connection.Query<EmployeeReference>("RemoveEmployeeRef", para, commandType: CommandType.StoredProcedure);

            return "Successfully removed the employee reference with id " + id;
        }
        [HttpPut("update/{id}")]
        public string UpdateEmployeeRef(int id, EmployeeReference employeeRef)
        {
            connection.ConnectionString = connectionString;

            var para = new DynamicParameters();
            para.Add("@ID", id);
            para.Add("@CODE", employeeRef.code);
            para.Add("@DESCRIPTION", employeeRef.description);

            var result = connection.Query<EmployeeReference>("UpdateEmpRef", para, commandType: CommandType.StoredProcedure);

            return "Successfully updated employee reference with id " + id;
        }


    }
}
public class EmployeeReference
{
    public int id;
    public string code;
    public string description;
    public DateTime systemDate;
}
