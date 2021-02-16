using System;
using Xunit;

namespace CoreLibrary.IntegrationTests
{
    public class TaxShould
    {
        //TODO: remove project dependency from CoreLibrary using some DI approach
        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Validate_Input1_USD()
        {
            //Arrange
            var t = new Tax();

            //Act
            //1. Input: Invoice Date: Aug 5, 2020, Pre-Tax Amount: 123.45 EUR, Payment Currency: USD
            var r = t.CurrencyExchangeFromInvoiceDate(new DateTime(2020, 08, 05), 123.45, "USD");

            //Assert Output : Pre-Tax Total: 146.57 USD, Tax Amount: 14.66 USD, Grand Total: 161.23 USD, Exchange Rate: 1.187247
            Assert.Equal(146.57, r.PreTaxTotal);
            Assert.Equal(14.66, r.TaxAmount);
            Assert.Equal(161.23, r.GrandTotal);
        }

    }
}
