# TaxCalculatorDemo

## Exercise:
Build a simple REST API with an endpoint that will calculate the taxes and perform currency exchange for
invoices. The endpoint should take three inputs: Invoice Date, Invoice Pre-Tax Amount in Euro (EUR),
and Payment Currency. After completing currency calculations, the endpoint should return four outputs:
Pre-Tax Amount, Tax Amount, Grand Total, and Exchange Rate.
Only two currencies need to be supported for currency conversion: Canadian Dollar (CAD) and US Dollar
(USD). The exchange rates should be obtained from an external API. A free account from Fixer.io
(https://fixer.io) should be sufficient, but feel free to use another API/service if preferred. The exchange
rate used for each calculation should be determined by the invoice date.
Tax is determined by the payment currency as follows. For this exercise it’s fine to hard-code the
following values: CAD = 11%, USD = 10%, and EUR = 9%.

## Test cases:
1. Input: Invoice Date: Aug 5, 2020, Pre-Tax Amount: 123.45 EUR, Payment Currency: USD
Output : Pre-Tax Total: 146.57 USD, Tax Amount: 14.66 USD, Grand Total: 161.23 USD, Exchange Rate: 1.187247
2. Input: Invoice Date: Jul 12, 2019, Pre-Tax Amount: 1,000.00 EUR, Payment Currency: EUR
Output: Pre-Tax Total: 1,000.00 EUR, Tax Amount: 90.00 EUR, Grand Total: 1,090.00 EUR, Exchange Rate: 1
3. Input: Invoice Date: Aug 19, 2020, Pre-Tax Amount: 6,543.21 EUR, Payment Currency: CAD
Output: Pre-Tax Total: 10,239.07 CAD, Tax Amount: 1,126.30 CAD, Grand Total: 11,365.37 CAD, Exchange Rate: 1.564839

## Timeboxing:
The expected time allocation for this test is around 2 hours. Your code does not need to be &quot;production
ready&quot; as we just want to get a feel for how you analyze, design, and code around a specific (if
somewhat contrived) problem. Please add code comments detailing any shortcuts you might have taken
and if you have thoughts about what you would improve if you spent more time on the solution.

## What we’re looking for:
Please share the finished source code with us for review. The source code should open and compile in
Visual Studio 2019 and be written in C# using the .NET Framework or .NET Core. Here are some of the
things we will be looking for when evaluating your code:
- An application architecture that is appropriate for the size of the exercise
- Clean and well-structured code that is easy to understand
- A good understanding .NET and API development
- Basic validation and simple error handling
- Unit tests for the core business logic


---
### Personal notes
1st things 1st, set your **YOUR_FIXER_KEY_HERE** in CoreLibrary project, file Tax.cs

Since we just have one GET call, I decided to renamed it to
/api/GetTaxesFromInvoiceDate 

if we need to support all sorts of RestAPI operations, it would be:
GET    /api/taxcalculation
POST   /api/taxcalculation
PUT    /api/taxcalculation
DELETE /api/taxcalculation

I have left out other features such as global exception handling, advanced logging, CORS, 
security (token based authentication/authorization) ...

Some improvements down the road may include: 
Non-Blocking API calls -> httpclient [6]
Write more Integration Tests
Caching


### FIXER call test
http://data.fixer.io/api/2020-08-05?access_key={YOUR_KEY}&symbols=USD&format=1


### Some short-cuts and resources used:
1. https://github.com/meziantou/Meziantou.MSBuild.InternalsVisibleTo
2. https://github.com/rmorrin/FixerSharp
3. https://stackify.com/web-api-error-handling/
4. https://jonhilton.net/2017/01/24/retrieve-data-from-a-third-party-openweather-api-using-asp-net-core-web-api/
5. https://www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api
6. https://medium.com/cheranga/calling-web-apis-using-typed-httpclients-net-core-20d3d5ce980
7. https://blog.restcase.com/rest-api-error-codes-101/