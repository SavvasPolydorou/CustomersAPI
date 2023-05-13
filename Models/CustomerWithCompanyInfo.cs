namespace CustomersAPI.Models
{
    public class CustomerWithCompanyInfo
    {

        public Customer Customer { get; set; }
        public List<CompanyTickerSymbolModel> CompanyInformation { get; set; }
    }
}
