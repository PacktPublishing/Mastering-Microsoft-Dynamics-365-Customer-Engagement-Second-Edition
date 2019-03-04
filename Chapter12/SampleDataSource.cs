using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace NewYorkZipCode.Models
{
    public class SampleDataSource
    {
        private static SampleDataSource instance = null;
        public static SampleDataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SampleDataSource();
                }
                return instance;
            }
        }
        // List to store objects of ZipCode Class which can be used as metadata for OData
        public List<ZipCode> ZipCodes { get; set; }
        // List to store objects of CurrencyConversion Class which can be used as metadata for OData
        public List<CurrencyConversion> CurrencyConversions { get; set; }

        // Constructor for SampleDataSource Class
        private SampleDataSource()
        {
            this.Reset();
            this.Initialize();
        }

 
        public void Reset()
        {
            this.ZipCodes = new List<ZipCode>();
            this.CurrencyConversions = new List<CurrencyConversion>();
        }

        public void Initialize()
        {
            // Retrieve JSON Data for Zip Codes from the url and store it in a json string
            string json = new WebClient().DownloadString("http://data.ny.gov/resource/8jaw-iviy.json");
            
            // Deserialize the JSON string and store it as a List of class Zip created below
            List<Zip> zipcodes = JsonConvert.DeserializeObject<List<Zip>>(json);

            // A New List to store the objects of ZipCode Class
            List<ZipCode> data = new List<ZipCode>();
            // A New List to store the objects of CurrencyConversion Class
            List<CurrencyConversion> currencyData = new List<CurrencyConversion>();

            // url to access the Currency Conversion JSON Data
            var url = "http://www.apilayer.net/api/live?access_key=b1e23a8acab4bb1cc22c765aae326ea3";
            // Store the JSON Data as a JSON string to currencyRates
            var currencyRates = _download_serialized_json_data<Currency>(url);
            
            // List to store the Currency Name inside quotes in the JSON data
            List<String> keyList = new List<String>(currencyRates.quotes.Keys);
            // List to store the Currency Conversion Rate inside quotes in the JSON data
            List<Decimal> ValueList = new List<Decimal>(currencyRates.quotes.Values);
            // Get the count of Currencies in the data
            var noOfCurrencies = keyList.Count;

            // For loop to set the currencyData List with objects of CurrencyConversion Class 
            for(int i = 0; i < noOfCurrencies; i++)
            {
                // Remove the USD prefix from the target currency
                String target = keyList[i].Replace("USD", "");
                // Add the object of CurrencyConversion Class having the record from JSON data to the currencyData List
                currencyData.Add(new CurrencyConversion
                    (Guid.NewGuid(), "USD", target, ValueList[i])
                    );
            }
            // For Loop to set the data List with objects of ZipCode Class
            foreach (var item in zipcodes)
            {
                // Add the object of ZipCode Class having the record from JSON data of Zip Class to the data List
                data.Add(new ZipCode
                    (Guid.NewGuid(), item.county, item.state_fips, item.county_code, item.county_fips,item.zip_code)
                    );
            }
            // Add the List of objects of ZipCode Class to ZipCodes List
            this.ZipCodes.AddRange(data);
            // Add the List of objects of CurrecyConversion Class to CurrencyConversions List
            this.CurrencyConversions.AddRange(currencyData);
            
        }
        // Function to get the JSON data as a string
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // Attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // If string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
    // Class to store the Zip Code JSON data coming from the url
    public class Zip
    {
        // Data Members with the same name as the Property Names in the JSON data for mapping
        public String county { get; set; }
        public String state_fips { get; set; }
        public String county_code { get; set; }
        public String county_fips { get; set; }
        public String zip_code { get; set; }
    }
    // Class to store the Currency Conversion JSON data coming from the url
    public class Currency
    {
        // Dictionary type Data Member to store the key value pair of Currency and Conversion Rate from the Property quotes in JSON data
        public Dictionary<String, Decimal> quotes { get; set; }
    }
}