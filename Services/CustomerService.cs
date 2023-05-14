using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CustomersAPI.Services
{
    public class CustomerService : ICustomerService
    {

        private static List<CustomerDTO> customersDTO = new List<CustomerDTO>();
        private readonly string filePath = "./Helpers/Data.json";
        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                customersDTO = JsonConvert.DeserializeObject<List<CustomerDTO>>(json);
            }
            return customersDTO;
        }

        public CustomerDTO GetCustomerById(int Id)
        {
            /* For optimisation purposes, this may not be ideal. If the JSON file contained millions of records this would
             * slow the app down as it fetches all of the customers and then applies the filtering condition.
             * A more efficient way could be to read the contents of the JSON file, and check if the Id of the current record
             * is equal to the Id parameter specified, and then return that Customer (something similar to lazy loading). */
            return GetAllCustomers().Where(c => c.Id == Id).First();
        }

        public CustomerDTO AddCustomer(Customer customer)
        {
            ////this means that the user left the currentStockPrice property to the default value which is not logical (0)
            //if (customer.CurrentStockPrice == 0m)
            //{
            //    try
            //    {
            //        customer.CurrentStockPrice = CallExternalAPIForCurrentStockPrice(customer).Result;
            //    }catch(Exception ex)
            //    {
            //        customer.CurrentStockPrice = 0;

            //    }
            //}
            CustomerDTO customerDTO = new CustomerDTO(customer);
            try
            {
                customerDTO.CompanyInformation = CallExternalAPIForCurrentStockPrice(customer.CompanyTickerSymbol).Result[0];
            }
            catch (Exception ex)
            {
                customerDTO.CompanyInformation = null;
            }
            customersDTO.Add(customerDTO);
            RefreshJSONFile();
            return customerDTO;
        }

        public CustomerDTO UpdateCustomer(Customer customer)
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
                //fetch the company info if the ticker symbol has changed
                if (!updatedCustomer.CompanyTickerSymbol.Equals(customer.CompanyTickerSymbol))
                {
                    try
                    {
                        updatedCustomer.CompanyTickerSymbol = customer.CompanyTickerSymbol;
                        updatedCustomer.CompanyInformation = CallExternalAPIForCurrentStockPrice(customer.CompanyTickerSymbol).Result[0];
                    }
                    catch (Exception ex)
                    {
                        updatedCustomer.CompanyInformation = null;
                    }
                }
                RefreshJSONFile();
            }
            return updatedCustomer;
        }

        public CustomerDTO DeleteCustomer(int Id)
        {
            var customerToDelete = GetAllCustomers().ToArray().Where(c => c.Id == Id).FirstOrDefault();
            if (customerToDelete != null)
            {
                customersDTO.Remove(customerToDelete);
                RefreshJSONFile();
            }

            return customerToDelete;
        }
        public IEnumerable<CustomerDTO> Search(string search)
        {
            /* 
             IMPORTANT:
            Not sure if it should be a partial search or if the value should match exactly, I'll go with partial search.
            Also not sure if I'm meant to create two different functions, one with the name parameter and one with the email parameter.
            It could also be the case where the emailAdddress is an optional parameter, but for this example I'll provide one parameter
            and use that against the name and email address properties.
             */

            //for case sensitivity purposes
            search = search.ToLower();
            var allRecords = GetAllCustomers().ToList();
            var recordsToReturn = new List<CustomerDTO>();
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
                serializer.Serialize(file, customersDTO);
            }
        }

        //private method for better code readability
        private async Task<List<CompanyTickerSymbolModel>> CallExternalAPIForCurrentStockPrice(string companyTickerSymbol)
        {
            //Should never be stored like this for security purposes, but it's ok for this assessment
            string apiKey = "5a9fe8ae378297a5a8dcdb6a6312d4b2";
            var url = "https://financialmodelingprep.com/api/v3/quote/";
            var parameters = $"{companyTickerSymbol}?apikey={apiKey}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);
            var responseObject = new List<CompanyTickerSymbolModel>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                responseObject = JsonConvert.DeserializeObject<List<CompanyTickerSymbolModel>>(jsonString);


                //this means that the company ticker symbol that the user provided is not valid
                if (responseObject.Count != 0)
                {
                    System.Diagnostics.Debug.WriteLine("dame = " + (responseObject.FirstOrDefault().Price));
                    // fetchedPrice = responseObject.FirstOrDefault().Price;
                }
            }

            return responseObject;
        }


    }
}
