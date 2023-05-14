# Axiom Assessment (Savvas Polydorou)
## API Documentation
Click [here](https://localhost:7257/swagger/index.html) to view API documentation on Swagger
## Running the project locally
### Notes
- The external API to fetch information about a company's ticker symbol can be found [here](https://site.financialmodelingprep.com/developer/docs/). If needed, register your own free account to grab an API key to use their API on this project. Alternatively, click [here](https://github.com/SavvasPolydorou/CustomersAPI/blob/380a1e0bf4441e4df69103255110ff0bf9084d5d/Services/CustomerService.cs#L134) to use my own personal API key, although a few requests are left and the account will be closed right after someone reviews this assessment.
- Sensitive information was left in plain sight in the code (such as API keys and JWT bearer tokens), but this is not recommended. This is fine for the purpose of this project.
- Make sure to send a request to the Authenticate endpoint in order to grab a bearer token so you can use the rest of the endpoints. All of the  Customer endpoints need authentication in order to produce results.
