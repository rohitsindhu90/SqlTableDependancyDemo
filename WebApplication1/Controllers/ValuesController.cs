using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using WebApplication1.Hubs;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ConfigurationSetting _connectionStrings;


        public ValuesController(IOptionsSnapshot<ConfigurationSetting> connectionStrings)
        {
            _connectionStrings = connectionStrings?.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            List<Employee> lstCustomer = new List<Employee>();
            //var connectionString = _connectionStrings.DefaultConnection;
            //using (var sqlConnection = new SqlConnection(connectionString))
            //{
            //    sqlConnection.Open();
            //    using (var sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "SELECT EmployeeId,FirstName FROM EmployeeDB";

            //        using (var sqlDataReader = sqlCommand.ExecuteReader())
            //        {
            //            while (sqlDataReader.Read())
            //            {
            //                var Id = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("EmployeeId"));
            //                var name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName"));


            //                lstCustomer.Add(new Employee { EmployeeId = Id, FirstName = name });
            //            }
            //        }
            //    }
            //}
            if (true) {
                throw new ArgumentOutOfRangeException("", "volume cannot be more than 100");

            }

            return lstCustomer;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
