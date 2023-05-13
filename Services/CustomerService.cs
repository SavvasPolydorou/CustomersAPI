using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Newtonsoft.Json;


namespace CustomersAPI.Services
{
    public class CustomerService : ICustomerService
    {

        private static List<Customer> customers = new List<Customer>();
        private readonly string filePath = "./Helpers/Data.json";
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
            return customers;
        }

        public Customer GetCustomerById(int Id)
        {
            /* For optimisation purposes, this may not be ideal. If the JSON file contained millions of records this would
             * slow the app down as it fetches all of the customers and then applies the filtering condition.
             * A more efficient way could be to read the contents of the JSON file, and check if the Id of the current record
             * is equal to the Id parameter specified, and then return that Customer (something similar to lazy loading). */
            return GetAllCustomers().Where(c => c.Id == Id).First();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            RefreshJSONFile();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var updatedCustomer = GetAllCustomers().ToArray().Where(c => c.Id == customer.Id).FirstOrDefault();
            if (updatedCustomer != null)
            {
                //could use an automapper library, but for the purpose of this app this is fine
                updatedCustomer.FullName = customer.FullName;
                updatedCustomer.Username = customer.Username;
                updatedCustomer.EmailAddress = customer.EmailAddress;
                updatedCustomer.Age = customer.Age;
                updatedCustomer.Gender = customer.Gender;
                updatedCustomer.IncomeGroup = customer.IncomeGroup;
                updatedCustomer.Occupation = customer.Occupation;
                updatedCustomer.Password = customer.Password;
                RefreshJSONFile();
            }
            return updatedCustomer;
        }

        public Customer DeleteCustomer(int Id)
        {
            var customerToDelete = GetAllCustomers().ToArray().Where(c => c.Id == Id).FirstOrDefault();
            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
                RefreshJSONFile();
            }

            return customerToDelete;
        }
       public IEnumerable<Customer> Search(string search)
        {
            /* 
             IMPORTANT:
            Not sure if it should be a partial search or if the value should match exactly, I'll go with partial search.
            Also not sure if I'm meant to create two different functions, one with the name parameter and one with the email parameter.
            It could also be the case where the emailAdddress is an optional parameter, but for this example I'll provide one parameter
            and use that against the name and email address properties.
             */
            
            //for case purposes
            search = search.ToLower();
            var allRecords = GetAllCustomers().ToList();
            var recordsToReturn = new List<Customer>();
            if (!string.IsNullOrEmpty(search))
                recordsToReturn = allRecords.Where(c => c.FullName.ToLower().Contains(search) || c.EmailAddress.ToLower().Contains(search)).ToList();      
            return recordsToReturn;
        }

        //private helper method so that I dont repeat myself (DRY principle)
        private void RefreshJSONFile()
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();

                //serialize object directly into file stream
                serializer.Serialize(file, customers);
            }
        }


    }
}
