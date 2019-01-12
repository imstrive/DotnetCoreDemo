using DockerDeploy.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DockerDeploy.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByID(int id);
        Task<List<Employee>> GetByDateOfBirth(DateTime dateofBirth);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        #region 初始化
        private readonly IConfiguration _config;
        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
        } 
        public IDbConnection Connection
        {
            get { return new MySqlConnection/*("server=localhost;port=3307;userid=root;password=longtao;database=LocalTestDB;Charset=utf8;SslMode=None")*/(_config.GetConnectionString("connectionString"));
            }
        }
        #endregion
        public async Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, FirstName, LastName, DateOfBirth FROM Employee WHERE DateOfBirth = @DateOfBirth";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { DateOfBirth = dateOfBirth });
                return result.ToList();
            }
        }

        public async Task<Employee> GetByID(int id)
        {
            string strCmd = @"select * from Employee where id = @ID";
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Employee>(strCmd, new { ID = id });
                return result.FirstOrDefault();
            }
        }
    }
}
