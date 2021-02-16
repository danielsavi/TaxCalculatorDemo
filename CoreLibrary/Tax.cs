using FixerSharp; //nuget-package a.k.a shortcut
using System;

namespace CoreLibrary
{
    public class Tax
    {
        public TaxResult CurrencyExchangeFromInvoiceDate(DateTime invoiceDate, double preTaxAmountEUR, string paymentCurrency)
        {
            return CurrencyExchangeFromInvoiceDate(invoiceDate, preTaxAmountEUR, paymentCurrency, null);
        }

        internal TaxResult CurrencyExchangeFromInvoiceDate(DateTime invoiceDate, double preTaxAmountEUR, string paymentCurrency, double? externalExchangeRate)
        {
            //basic input check
            if (invoiceDate.Year < 1999) throw new ArgumentException("Should not be less than 1999", "invoiceDate"); //business rule from fixer...
            if (preTaxAmountEUR <= 0) throw new ArgumentException("Should not be equal or less than zero", "preTaxAmountEUR");
            if (string.IsNullOrEmpty(paymentCurrency)) throw new ArgumentException("Should not be empty or null", "paymentCurrency");

            //hard-coded values just for simplicity
            //TODO: export to a private validator/checker
            double tax = 0;
            switch (paymentCurrency.ToUpper())
            {
                case "CAD":
                    tax = 0.11;
                    break;
                case "USD":
                    tax = 0.10;
                    break;
                case "EUR":
                    tax = 0.09;
                    break;
                default:
                    throw new ArgumentException($"value {paymentCurrency} not supported", "paymentCurrency");
            }

            var r = new TaxResult();
            //get external rate -> dirty way to branch 'Unit tests' and 'Integration Tests', removing external dependency
            if (externalExchangeRate == null)
            {
                Fixer.SetApiKey("YOUR_FIXER_KEY_HERE"); //TODO: store in a config file and set during app startup

                try
                {
                    // TODO: we should use an async call here (non-blocking), but also answer some more additional questions, such as:
                    // what happens when the external service call fails? now throwing error 500
                    // We could query multiple end-points/services 
                    // We're using a GET method, How about caching few requests? (like a day) just in-memory? on a DB? audit procedure compliance to follow?
                    // We could implement interesting things with Polly - such as Retry, Circuit-Braker, Cache, Fallback
                    //    https://www.c-sharpcorner.com/article/using-retry-pattern-in-asp-net-core-via-polly/
                    //    https://github.com/App-vNext/Polly
                    ExchangeRate exchangeRate = Fixer.Rate(Symbols.EUR, paymentCurrency, invoiceDate); //nuget-package a.k.a shortcut
                    r.ExchangeRate = exchangeRate.Rate;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Fixer error"); //TODO: Custom exception would be better, i.e. ExternalServiceException
                }

            }
            else
            {
                r.ExchangeRate = (double)externalExchangeRate;
            }

            r.PreTaxTotal = Math.Round(preTaxAmountEUR * r.ExchangeRate, 2, MidpointRounding.AwayFromZero);
            r.TaxAmount = Math.Round(r.PreTaxTotal * tax, 2, MidpointRounding.AwayFromZero);
            r.GrandTotal = Math.Round(r.PreTaxTotal + r.TaxAmount, 2, MidpointRounding.AwayFromZero);

            return r;
        }
    }
}
