using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using CustomersAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace CustomersAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService customerService;
        public CustomersController(ICustomerService _customerService)
        {
            customerService = _customerService;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all customers with their associated company information")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds a new customer")]
        public async Task<ActionResult<CustomerDTO>> AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (CustomerExists(customer.Id))
                    return BadRequest($"A Customer with an ID of {customer.Id} already exists!");


                CustomerDTO customerDTO = customerService.AddCustomer(customer);

                return CreatedAtAction(
                   actionName: nameof(GetCustomerById),
                   routeValues: new { id = customer.Id },
                   value: customerDTO
                    );
             
            }
            return BadRequest();
        }
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieves a customer by their Id")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int Id)
        {

            if (!CustomerExists(Id))
            {
                return BadRequest(new { errorMessage = $"A customer with an ID of {Id} does not exist!" });
            }

            return customerService.GetCustomerById(Id);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates an existing customer")]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(Customer customer)
        {
            var updatedCustomer = customerService.UpdateCustomer(customer);
            if (updatedCustomer == null)
            {
                return BadRequest(new { errorMessage = "The Customer to update does not exist." });
            }

            return Ok(updatedCustomer);

        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Deletes an existing customer")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int Id)
        {
            var customerToDelete = customerService.DeleteCustomer(Id);
            if (customerToDelete == null)
            {
                return BadRequest(new { errorMessage = "The Customer to delete does not exist." });
            }

            return NoContent();

        }

        [HttpGet("[action]/{search}")]
        [SwaggerOperation(Summary = "Partially searches for a customer by name or email address")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Search(string search)
        {
            var result = customerService.Search(search);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound(new { errorMessage = $"A customer whose name or email address contains the word '{search}' could not be found!" });
        }
        private bool CustomerExists(int Id)
        {
            return customerService.GetAllCustomers().Any(Customer => Customer.Id == Id);
        }
    }
}
