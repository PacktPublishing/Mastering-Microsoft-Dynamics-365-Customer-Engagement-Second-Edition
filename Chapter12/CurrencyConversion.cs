using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewYorkZipCode.Models
{
    public class CurrencyConversion
    {
        // Data Members of the Class
        public Guid ID { get; set; }
        public String SourceCurrency { get; set; }
        public String TargetCurrency { get; set; }
        public Decimal ConversionRate { get; set; }

        // Parameterized Constructor to initialize the object from the JSON data
        public CurrencyConversion(Guid ID, String SourceCurrency, String TargetCurrency, Decimal ConversionRate)
        {
            this.ID = ID;
            this.SourceCurrency = SourceCurrency;
            this.TargetCurrency = TargetCurrency;
            this.ConversionRate = ConversionRate;
        }
    }

}