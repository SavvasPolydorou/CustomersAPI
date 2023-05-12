using System.Text.Json.Serialization;

namespace CustomersAPI.Models
{
    public class Customer
    {
        #region Variables
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public IncomeGroup IncomeGroup { get; set; }
        public string Occupation { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        #endregion
    }
}
