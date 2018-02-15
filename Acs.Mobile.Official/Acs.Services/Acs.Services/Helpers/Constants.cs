using System;
using System.Collections.Generic;
using System.Text;

namespace Acs.Services.Helpers
{
    public static class Constants
    {
        // "https://passport-lab.accessefm.com/api/v2/mobile";

        public static readonly string API_ACTIVE_PROTOCOL = "https";

        public static readonly string API_BASE_URL = "https://passport-lab.accessefm.com/api/v2/mobile";

        public static readonly string API_ACTIVE_VERSION = "/v2/";

        public static readonly string API_ACTIVE_SYSTEM = "/efr/";

        public static readonly string API_ACTIVE_PLATFORM = "/mobile/";

        public static readonly string API_HTTP_CONTENT_TYPE = "application/json";




        //// https://passport-lab.accessefm.com/api/v2/mobile/esig/groups     // get form groups

        //// https://passport-lab.accessefm.com/api/v2/mobile/esig/patient    // process

        //public static string BaseUrlDeafult = "https://passport-lab.accessefm.com/api/v2/mobile";

        //public static string APIHttpContentType = "application/json";

        public static string PatientSegment = "/esig/patient";

        /// <summary>Segment leading to <c>api</c> that will process forms.</summary>
        public static string ProcessFormsSegment = "esig/patient/forms";

        /// <summary>The <c>Form Groups</c> segment</summary>
        public static string FormGroupSegment = "/esig/groups";

        public static string PatientFormSegment = "esig/patient/form";

        public static string ESigForms = "/esig/forms";

        public static string AccountSegment = "/account";

        public static string DeviceSegment = "/device";
    }
}