using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Acs.Common.Util;
using Acs.Services.Helpers;

namespace Acs.Services.RegistrationServices
{
    /// <summary>
    /// Facilitates the communication between a physical device and Acs regarding the Registration 
    /// management of a physical device.
    /// </summary>
    public class RegistrerDeviceService : IRegistrerDeviceService
    {
        /// <summary>Initializes a new instance of the type.</summary>
        public RegistrerDeviceService() { }

        /// <summary>Checks for the status of a particular device.</summary>
        /// <param name="deviceInfo">A <see cref="Domain.Models.ResponseModels.RegisterDeviceModel"/> type providing necessary 
        /// on which to verify the device.</param>
        /// <returns><see cref="Domain.Models.ResponseModels.RegisterDeviceModel"/> containing the current <c>Status</c> 
        /// of the device.</returns>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> GetDeviceStatusAsync(
            Domain.Models.RequestModels.RegisterDeviceModel deviceInfo)
        {
            if (null == deviceInfo || String.IsNullOrWhiteSpace(deviceInfo.SerialNumber))
            {
                throw new ArgumentException($"Argument can neither be null nor empty.", nameof(deviceInfo));
            }

            var apiSegment = "/device/";

            // Use the base url and append the api's segment for this method.
            string requestUrl = new StringBuilder(50).Append(UrlBuildFactory.GetDefaultBaseUrl()).Append(apiSegment)
                .Append(deviceInfo.SerialNumber).ToString();

            // https://passport-lab.accessefm.com/api/v2/mobile/device/R38J60D3CDN

            Domain.Models.ResponseModels.RegisterDeviceModel result = null;
            try
            {
                // Ensure the HttpClient is being disposed.
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(requestUrl);

                    // Calling the API.
                    var response = await client.GetAsync(requestUrl);

                    var placesJson = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.RegisterDeviceModel>(placesJson);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"EXCEPTION: {ex.Message}");
            }
            
            return result;
        }


        /// <summary>
        /// Allows a user to request a physical device be registered for use with the eForm platform. A device must be 
        /// approved before it will be allowed to access the syste.
        /// </summary>
        /// <param name="registerModel">A set of information that uniquely identifies the device.</param>
        /// <returns></returns>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> RegisterDeviceAsync(
            Domain.Models.RequestModels.RegisterDeviceModel registerModel)
        {
            if (null == registerModel)
                throw new ArgumentException($"Argument can neither be null nor empty.", nameof(registerModel));

            var apiSegment = "/device";

            // Use the base url and append the api's segment for this method.
            string requestUrl = new StringBuilder(50)
                .Append(UrlBuildFactory.GetDefaultBaseUrl())
                .Append(apiSegment).ToString();

            Domain.Models.ResponseModels.RegisterDeviceModel result = null;
            try
            {
                //var client = new HttpClient();
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(requestUrl);

                    var data = JsonConvert.SerializeObject(registerModel);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                    result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.RegisterDeviceModel>(
                        response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception e)
            {
                var inMsg = (null == e.InnerException) ? String.Empty : $"Inner Excp: {e.InnerException.Message}";
                var msg = ($"Exception in RegistrerDeviceService: {e.Message}{Environment.NewLine}{inMsg}");
                Debug.WriteLine(msg);
            }
            
            return result;
        }


        public async Task<bool> ChangeDeviceStatusAsync(Domain.Models.RequestModels.RegisterDeviceModel deviceInfo)
        {
            if (null == deviceInfo)
                throw new ArgumentException($"Argument can neither be null nor empty.", nameof(deviceInfo));

            var apiSegment = "/device/";
            string requestUrl = new StringBuilder(50).Append(UrlBuildFactory.GetDefaultBaseUrl())
                .Append(apiSegment).Append(deviceInfo.SerialNumber).Append("/2").ToString();

            var placesJson = String.Empty;
            var jsonString = String.Empty;

            try
            {
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                   // client.BaseAddress = new Uri(Settings.GetBaseUrl());
                    client.BaseAddress = new Uri(apiSegment);
                    var response = await client.PutAsync(apiSegment, httpContent);

                    placesJson = response.Content.ReadAsStringAsync().Result;
                }
                return true;
            }
            catch (Exception e)
            {
                var inMsg = (null == e.InnerException) ? String.Empty : $"Inner Excp: {e.InnerException.Message}";
                var msg = ($"Exception in RegistrerDeviceService: {e.Message}{Environment.NewLine}{inMsg}");
                Debug.WriteLine(msg);
            }

            return false;
        }        
    }
}