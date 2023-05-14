# Axiom Assessment (Savvas Polydorou)
If you have problems running the project, please contact me and we can resolve it. The code on this github repository will not be updated after sending the source code link to Stelios Ioannou.
## API Documentation
Click [here](https://localhost:7257/swagger/index.html) to view API documentation on Swagger. The Swagger page contains definitions for each one of the endpoints, along with the schema of the objects, with their validation attributes.
## Running the project locally
- Clone the project (or download a .zip version of it) and open it on Visual Studio (I'm using VS 2022)
- Open the CustomersAPI.sln solution
- Right click on CustomersAPI -> Set as startup project
- If a "Trust ASP.NET Core SSL Certificate" prompt comes up, press Yes twice
- 
- You should land on this URL (https://localhost:7257/swagger/index.html) on your preferred browser and should see the Swagger UI index page
## Important notes
- Make sure to send a request to the Authenticate endpoint in order to grab a bearer token so you can use the rest of the endpoints. All of the  Customer endpoints need authentication in order to produce results.
- The external API to fetch information about a company's ticker symbol can be found [here](https://site.financialmodelingprep.com/developer/docs/). If needed, register your own free account to grab an API key to use their API on this project. Alternatively, click [here](https://github.com/SavvasPolydorou/CustomersAPI/blob/380a1e0bf4441e4df69103255110ff0bf9084d5d/Services/CustomerService.cs#L134) to use my own personal API key, although a few requests are left and the account will be closed right after someone reviews this assessment.
- Sensitive information was left in plain sight in the code (such API keys and JWT bearer tokens), but this is not recommended. This is fine for the purpose of this project.
- Passwords are not hashed.
- In order to grab a bearer token, you don't need to actually exist in one of the customer records. The user just needs to provide a username and a password, and a bearer token will be returned. This was done intentionally, as there wasn't a point in checking with the customer records to validate that the usernames and passwords match. I just wanted to demonstrate how authentication and JWT works.
- For POST and PUT requests, I used the Customer model as the user only needs to privide a company's ticker symbol. The external API handles the rest and there is a DTO object that the API outputs, hence why there is more info on the response body.
- If the external API doesn't have any information about the given company's ticker symbol, a **null** value is passed.
- If a user updates the value of the company's ticker symbol, the external API is called again to update the model.
- Validation and error handling exist for the requests, check the Schema at the bottom of the Swagger index page to view the policies around each property of each object.

