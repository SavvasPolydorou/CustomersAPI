using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CustomersAPI.Models.Enums;

namespace CustomersAPI.Models
{
    public class Customer
    {
        #region Variables
        
        [Required(ErrorMessage = "Field can't be empty")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number!")]
        // you wouldn't normally specify a max value for an Id since this will limit the number of records, but for this instace it's ok
        public int Id { get;  set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(50, ErrorMessage = "A full name can only be 50 characters long!")]
        [DefaultValue("Savvas Polydorou")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DefaultValue("savvaspol")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(255)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address. Please provide an email address with the following format: user@example.com")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [Range(1, 120, ErrorMessage = "Please enter a positive number between 1 and 120 inclusive")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender can only be a number from 0 to 2 inclusive, or Male, Female, and Other respectively")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [EnumDataType(typeof(IncomeGroup), ErrorMessage = "Income group can only be a number from 0 to 3 inclusive, or Low, LowerMiddle, UpperMiddle and High respectively")]
        public IncomeGroup? IncomeGroup { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(100, ErrorMessage = "An occupation can only be 100 characters long!")]
        [DefaultValue("Junior Software Engineer")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DefaultValue("Unhashed password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(5, ErrorMessage = "A company's ticker symbol can only be 5 characters long!")]
        [DefaultValue("AAPL")]
        public string CompanyTickerSymbol { get; set; }
        #endregion
    }
}
