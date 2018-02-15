using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acs.Mobile.EFR.Configuration
{
    public static class ServiceConfigurations
    {
        public static readonly string DefaultDomainName = "accessefm";
        public static readonly string DefaultBaseUrl = "https://passport-lab.accessefm.com/api/v2/mobile";
        public static readonly string ServiceVersion = "v2";
        public static readonly string ApiBaseUrlSegment = "";


        // baseURL + segment{apiVersion} + setment{1} + parm{1} + parm{2} + ...... ;

        //string ApiBaseUrl = Settings.GetBaseUrl();
        //string ApiVersion0 = "v2";
        //string ApiSegment1 = "/device/";
        // string ApiFinalUrl = ApiBaseUrl + ApiSegment1 + deviceInfo.SerialNumber;
    }
}