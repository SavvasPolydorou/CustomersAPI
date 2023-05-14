namespace CustomersAPI.Models
{
    public sealed class CompanyTickerSymbolModel
    {
        /* the commented out code is the JSON structure of an example response from the external API
         * In order to convert a string JSON to an object a class is needed to map the values, I'll not
         * create properties for all of them, just a select few
         */
        #region Variables
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ChangesPercentage { get; set; }       
        public decimal YearHigh { get; set; }
        public decimal YearLow { get; set; }
        #endregion
        //"symbol": "AAPL",
        //"name": "Apple Inc.",
        //"price": 172.57,
        //"changesPercentage": -0.5417,
        //"change": -0.94,
        //"dayLow": 171,
        //"dayHigh": 174.06,
        //"yearHigh": 176.15,
        //"yearLow": 124.17,
        //"marketCap": 2714301830789,
        //"priceAvg50": 162.3786,
        //"priceAvg200": 151.84406,
        //"exchange": "NASDAQ",
        //"volume": 45442695,
        //"avgVolume": 59104364,
        //"open": 173.62,
        //"previousClose": 173.51,
        //"eps": 5.91,
        //"pe": 29.2,
        //"earningsAnnouncement": "2023-07-26T20:00:00.000+0000",
        //"sharesOutstanding": 15728700416,
        //"timestamp": 1683921605
    }
}
