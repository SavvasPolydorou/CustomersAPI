using CustomersAPI.Models.Enums;

namespace CustomersAPI.Models
{
    public class CustomerDTO
    {
        #region Constructors
        public CustomerDTO() { }
        public CustomerDTO(Customer customer)
        {
            Id = customer.Id;
            FullName = customer.FullName;
            Username = customer.Username;
            EmailAddress = customer.EmailAddress;
            Age = customer.Age;
            Gender = customer.Gender;
            IncomeGroup = customer.IncomeGroup;
            Occupation = customer.Occupation;
            Password = customer.Password;
            CompanyTickerSymbol = customer.CompanyTickerSymbol;
        }
        #endregion

        #region Variables

        public int Id { get; set; }
       
        public string FullName { get; set; }
    
        public string Username { get; set; }
    
        public string EmailAddress { get; set; }
      
        public int? Age { get; set; }
     
        public Gender? Gender { get; set; }

        public IncomeGroup? IncomeGroup { get; set; }

        public string Occupation { get; set; }

        public string Password { get; set; }

        public string CompanyTickerSymbol { get; set; }

        public CompanyTickerSymbolModel? CompanyInformation { get;  set; }
        #endregion
    }
}
