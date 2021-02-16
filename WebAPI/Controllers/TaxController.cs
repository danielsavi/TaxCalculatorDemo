using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreLibrary;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/GetTaxesFromInvoiceDate")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;

        public TaxController(ILogger<TaxController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTaxesFromInvoiceDate(DateTime invoiceDate, double preTaxAmountEUR, string paymentCurrency)
        {
            //TODO: we should implement input validation here, I don't think that we should trust the client or any data coming from the wire
            // validate invoiceDate - How many formats we should expect here? YYYY-MM-DD, DD-MM-YYYY, YYYYMMDD
            // validate preTaxAmountEUR - accepted chars [0..9] + '.,' RegExp?
            // validate paymentCurrency - String[3] ??? [A..Z] RegExp?
            // 
            // My testing values using Swagger interface were
            // 2013-07-29
            // 123.45
            // USD
            try
            {
                var result = new Tax().CurrencyExchangeFromInvoiceDate(invoiceDate, preTaxAmountEUR, paymentCurrency);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(); //we're assuming ArgumentException is well-handled by the CoreLibrary, let's inform the end-user about a bad input
            }
            catch (Exception ex) //just to be quick, any other exception will cause a 500-status code, we should create a global exception handler and/or custom exceptions
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }

}
