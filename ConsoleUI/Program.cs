using CoreLibrary;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var t = new Tax();

                //Input: Invoice Date: Aug 5, 2020, Pre - Tax Amount: 123.45 EUR, Payment Currency: USD
                var r = t.CurrencyExchangeFromInvoiceDate(new DateTime(2020, 08, 5), 123.45, "USD");

                //Output: Pre - Tax Total: 146.57 USD, Tax Amount: 14.66 USD, Grand Total: 161.23 USD, Exchange Rate: 1.187247

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
