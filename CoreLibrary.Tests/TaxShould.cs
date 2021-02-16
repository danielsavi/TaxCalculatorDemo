using System;
using Xunit;

namespace CoreLibrary.Tests
{
    public class TaxShould
    {
        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Validate_Input1_USD()
        {
            //Arrange  Subscriber_Constructor_Negative_Threshold_ShouldReturn_ArgumentException()
            var t = new Tax();

            //Act
            //1. Input: Invoice Date: Aug 5, 2020, Pre-Tax Amount: 123.45 EUR, Payment Currency: USD
            var r = t.CurrencyExchangeFromInvoiceDate(new DateTime(2020, 08, 05), 123.45, "USD", 1.187247);

            //Assert Output : Pre-Tax Total: 146.57 USD, Tax Amount: 14.66 USD, Grand Total: 161.23 USD, Exchange Rate: 1.187247
            Assert.Equal(146.57, r.PreTaxTotal);
            Assert.Equal(14.66, r.TaxAmount);
            Assert.Equal(161.23, r.GrandTotal);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Validate_Input2_EUR()
        {
            //Arrange
            var t = new Tax();

            //Act
            //2. Input: Invoice Date: Jul 12, 2019, Pre-Tax Amount: 1,000.00 EUR, Payment Currency: EUR
            var r = t.CurrencyExchangeFromInvoiceDate(new DateTime(2019, 07, 12), 1000.00, "EUR", 1);

            //Assert Output: Pre-Tax Total: 1,000.00 EUR, Tax Amount: 90.00 EUR, Grand Total: 1,090.00 EUR, Exchange Rate: 1
            Assert.Equal(1000.00, r.PreTaxTotal);
            Assert.Equal(90.00, r.TaxAmount);
            Assert.Equal(1090.00, r.GrandTotal);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Validate_Input3_CAD()
        {
            //Arrange
            var t = new Tax();

            //Act
            //3. Input: Invoice Date: Aug 19, 2020, Pre-Tax Amount: 6,543.21 EUR, Payment Currency: CAD
            var r = t.CurrencyExchangeFromInvoiceDate(new DateTime(2020, 08, 19), 6543.21, "CAD", 1.564839);

            //Assert Output: Pre-Tax Total: 10,239.07 CAD, Tax Amount: 1,126.30 CAD, Grand Total: 11,365.37 CAD, Exchange Rate: 1.564839
            Assert.Equal(10239.07, r.PreTaxTotal);
            Assert.Equal(1126.30, r.TaxAmount);
            Assert.Equal(11365.37, r.GrandTotal);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_InvoiceDate_PriorTo1999_ShouldReturn_ArgumentException()
        {
            //Arrange
            var t = new Tax();

            //Act
            var ex = Assert.Throws<ArgumentException>(() => t.CurrencyExchangeFromInvoiceDate(new DateTime(1998, 08, 19), 1, "CAD"));

            //Assert
            Assert.Equal("Should not be less than 1999 (Parameter 'invoiceDate')", ex.Message);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Zero_preTaxAmountEUR_ShouldReturn_ArgumentException()
        {
            //Arrange
            var t = new Tax();

            //Act
            var ex = Assert.Throws<ArgumentException>(() => t.CurrencyExchangeFromInvoiceDate(new DateTime(2019, 08, 19), 0, "CAD"));

            //Assert
            Assert.Equal("Should not be equal or less than zero (Parameter 'preTaxAmountEUR')", ex.Message);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Empty_paymentCurrency_ShouldReturn_ArgumentException()
        {
            //Arrange
            var t = new Tax();

            //Act
            var ex = Assert.Throws<ArgumentException>(() => t.CurrencyExchangeFromInvoiceDate(new DateTime(2019, 08, 19), 10, ""));

            //Assert
            Assert.Equal("Should not be empty or null (Parameter 'paymentCurrency')", ex.Message);
        }

        [Fact]
        public void CurrencyExchangeFromInvoiceDate_Invalid_paymentCurrency_ShouldReturn_ArgumentException()
        {
            //Arrange
            var t = new Tax();

            //Act
            var ex = Assert.Throws<ArgumentException>(() => t.CurrencyExchangeFromInvoiceDate(new DateTime(2019, 08, 19), 10, "XYZ"));

            //Assert
            Assert.Equal("value XYZ not supported (Parameter 'paymentCurrency')", ex.Message);
        }
    }

}
