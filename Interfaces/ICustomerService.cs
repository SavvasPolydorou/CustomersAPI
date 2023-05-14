using CustomersAPI.Models;

namespace CustomersAPI.Interfaces
{
    public interface ICustomerService
    {
        public IEnumerable<CustomerDTO> GetAllCustomers();

        public CustomerDTO GetCustomerById(int Id);

        public CustomerDTO AddCustomer(Customer customer);

        public CustomerDTO UpdateCustomer(Customer customer);

        public CustomerDTO DeleteCustomer(int Id);

        public IEnumerable<CustomerDTO> Search(string search);

    }
}
