using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CustomersAPI.Models
{
    public class Customer
    {
        #region Variables
        
        [Required(ErrorMessage = "Field can't be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive number!")] 
        public int Id { get; set; }

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
        [EmailAddress(ErrorMessage = "Invalid email address. Please provide an email address with the following format: user@example.com")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address. Please provide an email address with the following format: user@example.com")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [Range(0, 120, ErrorMessage = "Please enter a positive number between 0 and 120 inclusive")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender can only be a number from 0 to 2 inclusive, or Male, Female, and Other respectively")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender can only be a number from 0 to 2 inclusive, or Male, Female, and Other respectively")]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = "Income group can only be a number from 0 to 4 inclusive, or Low, LowerMiddle, UpperMiddle and High respectively")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Income group can only be a number from 0 to 4 inclusive, or Low, LowerMiddle, UpperMiddle and High respectively")]
        public IncomeGroup IncomeGroup { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(100, ErrorMessage = "An occupation can only be 100 characters long!")]
        [DefaultValue("Junior Software Engineer")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #endregion
    }
}
