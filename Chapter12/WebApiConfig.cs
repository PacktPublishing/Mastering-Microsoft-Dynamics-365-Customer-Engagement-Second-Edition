using Microsoft.OData.Edm;
using NewYorkZipCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace NewYorkZipCode
{
    public static class WebApiConfig
    {
        // Register the service
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();
        }
        // Get the EdmModel
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            // Namespace for the OData
            builder.Namespace = "Sample";
            // Container Name for the OData
            builder.ContainerName = "DefaultContainer";
            // EntitySet Name for ZipCode Entity in OData
            builder.EntitySet<ZipCode>("ZipCodes");
            // EntitySet Name for CurrencyConversion Entity in OData
            builder.EntitySet<CurrencyConversion>("CurrencyConversions");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
