### TASK:

So at the moment the api is currently returning a random selection from a fixed set of Greggs products directly 
from the controller itself. We currently have a data access class and it's interface but 
it's not plugged in (please ignore the class itself, we're pretending it hits a database),
we're also going to pretend that the data access functionality is fully tested so we don't need 
to worry about testing those lines of functionality.

We're mainly looking for the way you work, your code structure and how you would approach tackling the following 
scenarios.

## User Stories
Our product owners have asked us to implement the following stories, we'd like you to have 
a go at implementing them. You can use whatever patterns you're used to using or even better 
whatever patterns you would like to use to achieve the goal. Anyhow, back to the 
user stories:

## User Story 1
**As a** Greggs Fanatic<br/>
**I want to** be able to get the latest menu of products rather than the random static products it returns now<br/>
**So that** I get the most recently available products.

**Acceptance Criteria**<br/>
**Given** a previously implemented data access layer<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** a list or products is returned that uses the data access implementation rather than the static list it current utilises

### User Story 2
**As a** Greggs Entrepreneur<br/>
**I want to** get the price of the products returned to me in Euros<br/>
**So that** I can set up a shop in Europe as part of our expansion

------------------------------
### SOLUTION:

Added 2 endpoints, for Greggs Fanatic and Greggs Entrepreneur.
Added CurrencyConverter
Currency validation for Entrepreneur (FluentValidation).
Serilog logging (console & file)
Tests (nSubstitute, FluentAssertions, FastEndpoints.Testing)

Used FastEndpoints: https://fast-endpoints.com/docs, benefits:
- class per endpoint, vertical slicing, validation, many more.

Probably I shouldn't but:
- upgraded to .NET 8
- upgraded libs in Tests projects
- fixed ProductAccess class, as I expect that my DB will be able to sort data before applying Take / Skip functions. Introduced Query specification pattern and implemented it as well.
- I used existing data, as I'm believer in integration testing of API's, as it's better to use real or in memory DB

Assumptions made:
1) It was not clear what exactly should be returned to the client, I used common class for both scenarios ProductDto. (if the data there can't be displayed for both scenarios,
   then it can be modified on individual endpoints)

2) If there is no pageSize specified I set it default value to 10, to prevent sending all data by default

3) It was not specified what should be the order for Entrepreneur, so I left it unordered.

4) CurrencyConverter rounding to 2 decimals by default

=====================================================================




**Acceptance Criteria**<br/>
**Given** an exchange rate of 1GBP to 1.11EUR<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** I will get the products and their price(s) returned
