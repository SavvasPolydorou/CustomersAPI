using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Models
{
    public class AuthenticationRequest
    {
        [Required (ErrorMessage = "Field cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        public string Password { get; set; }
    }
}
