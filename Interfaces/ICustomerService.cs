using CustomersAPI.Models;

namespace CustomersAPI.Interfaces
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetAllCustomers();
       
        public Customer GetCustomerById(int Id);

        public void AddCustomer(Customer customer);

        public Customer UpdateCustomer(Customer customer);

        public Customer DeleteCustomer(int Id);

        public IEnumerable<Customer> Search(string search);
    }
}
