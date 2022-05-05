# RetailInMotion

A small web app that delivers some information associated with a city.
The web application has a simple interface, where the user selects a city and its data is displayed on screen.

# The Challenge

# Create a an API solution implementing the follow actions:
- Create a new order
- Update the order delivery address
- Update the order items
- Cancel an order
- Retrieve a single order
- Retrieve a paginated list of orders
* Bonus point: manage the inventory stock for products
# Additional requirements:
- The solution has to build and execute without errors
- The solution has to be production ready (deployable)
# Additional notes
- A product can be just a GUID
- There is no expectation of validating the product, this would be outside the scope of this assignment
- Feel free to use whatever persistence layer you see fit

# Technologies and frameworks used:
  * Asp Net 6.0 used
  * Poject implented using CQRS and DDD principles in a Clean Architecture 

# Database Configuration
This project is configured to use an in-memory database by default.
To use SQL Server, update WebUI/appsettings.json as follows:
  "UseInMemoryDatabase": false,
Verify that the DefaultConnection connection string within appsettings.json points to a valid SQL Server instance.
  
# Requirements
  * Dotnet SDK   
# Usage
  * Backend
      * Set WebAPI as startup project
      * Project will launch using a Swagger UI
     
