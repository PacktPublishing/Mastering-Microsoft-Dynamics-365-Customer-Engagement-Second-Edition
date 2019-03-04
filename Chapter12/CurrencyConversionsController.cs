using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using NewYorkZipCode.Models;
using Microsoft.OData.Core;

namespace NewYorkZipCode.Controllers
{
    [EnableQuery]
    public class CurrencyConversionsController : ODataController
    {
        public IHttpActionResult Get()
        {
            // returning the List CurrencyConversions
            return Ok(SampleDataSource.Instance.CurrencyConversions.AsQueryable());
        }
    }
}
