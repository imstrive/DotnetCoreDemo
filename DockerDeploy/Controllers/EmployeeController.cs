using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DockerDeploy.Entities;
using DockerDeploy.Repository;

namespace DockerDeploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        /// <summary>
        /// 员工查询
        /// </summary>
        /// <param name="id">员工ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> GetByID(int id)
        {
            return await _employeeRepo.GetByID(id);
        }
        /// <summary>
        /// 员工查询
        /// </summary>
        /// <param name="dateOfBirth">员工生日</param>
        /// <returns></returns>
        [HttpGet]
        [Route("dob/{dateOfBirth}")]
        public async Task<ActionResult<List<Employee>>> GetByID(DateTime dateOfBirth)
        {
            return await _employeeRepo.GetByDateOfBirth(dateOfBirth);
        }
    }
}