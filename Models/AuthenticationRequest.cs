using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Models
{
    //this class is used for the Authenticate endpoint
    public class AuthenticationRequest
    {
        [Required (ErrorMessage = "Field cannot be empty")]
        [DefaultValue("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [DefaultValue("Password")]
        public string Password { get; set; }
    }
}
