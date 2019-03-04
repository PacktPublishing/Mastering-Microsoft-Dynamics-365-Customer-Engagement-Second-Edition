using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewYorkZipCode.Models
{
    public class ZipCode
    {
        // Data Members of the class
        public Guid ID { get; set; }
        public String CountyName { get; set; }
        public String StateFIPS { get; set; }
        public String CountyCode { get; set; }
        public String CountyFIPS { get; set; }
        public String Zipcode { get; set; }

        // Parameterized Constructor to initialize the object from the JSON data
        public ZipCode(Guid ID, String CountyName, String StateFIPS, String CountyCode, String CountyFIPS, String Zipcode)
        {
            this.ID = ID;
            this.CountyName = CountyName;
            this.StateFIPS = StateFIPS;
            this.CountyCode = CountyCode;
            this.CountyFIPS = CountyFIPS;
            this.Zipcode = Zipcode;
        }
    }
}