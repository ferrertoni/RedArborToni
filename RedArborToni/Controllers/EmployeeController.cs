using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedArborToni.Models;
using RedArborToni.Services;
using System;

namespace RedArborToni.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private EmployeeService _employeeService;        

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees items
        /// </summary>
        [Route("Get")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            try
            {
                var employees = _employeeService.Get();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception : {ex.Message}");
                return GetErrorResult(ex);
            }
        }

        /// <summary>
        /// Get an item by ID
        /// </summary>
        [Route("GetByID/{employeeID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByID(int employeeID)
        {
            try
            {
                var employee = _employeeService.GetByID(employeeID);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception : {ex.Message}");
                return GetErrorResult(ex);
            }
        }


        /// <summary>
        /// Add a new item
        /// </summary>
        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create(EmployeeModel employee)
        {
            try
            {
                var employeeResult = _employeeService.Create(employee);
                return Ok(employeeResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception : {ex.Message}");
                return GetErrorResult(ex);
            }
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        [Route("Update/{employeeID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int employeeID, EmployeeModel employee)
        {
            try
            {
                _employeeService.Update(employeeID, employee);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception : {ex.Message}");
                return GetErrorResult(ex);
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        [Route("Delete/{employeeID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int employeeID)
        {
            try
            {
                _employeeService.Delete(employeeID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception : {ex.Message}");
                return GetErrorResult(ex);
            }
        }

        private ObjectResult GetErrorResult(Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ex?.Message,
                Detail = ex?.StackTrace,
                Instance = HttpContext.Request.Path,
            };

            var errorResult = new ObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = problemDetails.Status,
            };

            return errorResult;
        }
    }
}

