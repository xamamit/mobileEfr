using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Acs.Services.Helpers
{
    public static class UrlBuildFactory
    {
        //     "https://passport-lab.accessefm.com/api/v2/mobile";

        //      private static readonly string _baseUrlDeafult = "https://passport-lab.accessefm.com/api/v2/mobile";
        // API_ACTIVE_PROTOCOL = "https";
        //public static readonly string API_DFLT_PROTOCOL = "https";
        //public static readonly string API_DFLT_BASE_URL_NO_PROTOCOL = "passport-lab.accessefm.com/api";
        //public static readonly string API_DFLT_BASE_URL_HTTP_PROTOCOL = "http://passport-lab.accessefm.com/api";
        //public static readonly string API_DFLT_BASE_URL_HTTPS_PROTOCOL = "https://passport-lab.accessefm.com/api";

        //      API_DFLT_BASE_URL = "https://passport-lab.accessefm.com/api";

        //      API_DFLT_VERSION = "/v2/";

        //      API_DFLT_SYSTEM = "/efr/";

        //      API_DFLT_PLATFORM = "/mobile/";

        //      API_HTTP_CONTENT_TYPE = "application/json";

        //      https://passport-lab.accessefm.com/api      /v2/                mobile
        //      API_DFLT_BASE_URL +                         API_DFLT_VERSION +  API_DFLT_PLATFORM


        public static string GetDefaultBaseUrl()
        {
            
            return Constants.API_BASE_URL;
        }

        public static string GetActiveApiProtocol()
        {
            return Constants.API_ACTIVE_PROTOCOL;
        }

        //public static string GetBaseUrl()
        //{
        //    return Constants.API_ACTIVE_BASE_URL_HTTPS_PROTOCOL;
        //}

        public static string GetActiveApiVersion()
        {
            return Constants.API_ACTIVE_VERSION;
        }

        public static string GetApiActiveSystem()
        {
            return Constants.API_ACTIVE_SYSTEM;
        }

        public static string GetApiPlatform()
        {
            return Constants.API_ACTIVE_PLATFORM;
        }

        public static string GetApiHttpContentType()
        {
            return Constants.API_HTTP_CONTENT_TYPE;
        }
    }
}