
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

using Acs.Domain.Models;
using Acs.Domain.Models.ResponseModels;
using Acs.Services.Helpers;


namespace Acs.Services.AuthServices
{
    public class ESigAuthService : IESigAuthService
    {
        /// <summary>Initializes a new instance of the <see cref="ESigAuthService"/> class.</summary>
        public ESigAuthService() { }

        private bool _isAuthenticated = false;

        private Domain.Models.RequestModels.AuthModel SetCommonUserFields(Domain.Models.RequestModels.AuthModel user)
        {
            user.ApplicationId = Settings.GetApplicationId();
            user.SerialNumber = Settings.GetSerialNumber();
            user.Version = Settings.GetVersion();
            user.AuthToken = Settings.GetAuthorizationToken();
            user.DomainName = Settings.GetDomainName();

            return user;
        }

        public bool ValidateLogin(string barCode)
        {
            // Call service on server
            var passesAuth = true;

            return passesAuth;
        }

        public async Task<Domain.Models.ResponseModels.AuthModel> AuthenticateUserAsync(Domain.Models.RequestModels.AuthModel user)
        {
            user = SetCommonUserFields(user);

            var client = new HttpClient();
            client.BaseAddress = new Uri(Settings.GetBaseUrl());

            var data = JsonConvert.SerializeObject(user);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(Settings.GetBaseUrl() + "/account", content);

            var result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.AuthModel>(
                response.Content.ReadAsStringAsync().Result);

            return result;
        }


        // 
        //  FORMS   -- TODO: Forms need to be broken out into their own service, viewModel and interface
        //

        public async Task<Domain.Models.ResponseModels.AddFormModel> AddFormsAsync(
            Domain.Models.RequestModels.AddFormModel addFormReq)
        {
            Domain.Models.ResponseModels.AddFormModel result = null;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.GetBaseUrl());

                var data = JsonConvert.SerializeObject(addFormReq);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Settings.GetBaseUrl() + "/efr/patient/form", content);

                result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.AddFormModel>(
                    response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception erf)
            {
                Debug.Write(erf);
            }

            return result;
        }

        /// <summary>Gets the individual forms that are members of the specified 
        /// <see cref="Domain.Models.RequestModels.FormGroupModel.FormGroupId"/>.</summary>
        /// <param name="formGroupRequest">The form group request.</param>
        /// <returns>A Domain.Models.ResponseModels.GroupOfFormTypesModel type.</returns>
        public async Task<Domain.Models.ResponseModels.GroupOfFormTypesModel> GetGroupOfFormTypesAsync(
            Domain.Models.RequestModels.FormGroupModel formGroupRequest)
        {
            Domain.Models.ResponseModels.GroupOfFormTypesModel result = null;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.GetBaseUrl());

                var data = JsonConvert.SerializeObject(formGroupRequest);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Settings.GetBaseUrl() + "/efr/forms", content);

                result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.GroupOfFormTypesModel>(
                    response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception erf)
            {
                Debug.Write(erf);
            }

            return result;
        }


        public async Task<Domain.Models.ResponseModels.FormGroupGroupingsModel> GetFormGroupsAsync(
            Domain.Models.RequestModels.FormGroupModel formGroupRequest)
        {
            Domain.Models.ResponseModels.FormGroupGroupingsModel result = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.GetBaseUrl());

                var data = JsonConvert.SerializeObject(formGroupRequest);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Settings.GetBaseUrl() + "/efr/groups", content);

                result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.FormGroupGroupingsModel>(
                    response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception erf)
            {
                Debug.Write(erf);
            }

            return result;
        }


        /// <summary>Processes the forms asynchronous.</summary>
        /// <param name="processFormRequest">The process form request.</param>
        /// <returns></returns>
        public async Task<Domain.Models.ResponseModels.ProcessPatientFormModel> ProcessFormsAsync(
            Domain.Models.RequestModels.ProcessPatientFormModel processFormRequest)
        {
            Domain.Models.ResponseModels.ProcessPatientFormModel result = null;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.GetBaseUrl());

                var data = JsonConvert.SerializeObject(processFormRequest);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                // Add the form(s) to the patient's list of forms.
                HttpResponseMessage response = await client.PostAsync(Settings.GetBaseUrl() + "/efr/patient/forms", content);

                result = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.ProcessPatientFormModel>(
                    response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception erf)
            {
                Debug.Write(erf);
            }

            return result;
        }

        /// <summary>Searches for a, <c>Person</c> given a specific <paramref name="accountNumber"/>..</summary>
        /// <param name="accountNumber">The Person's account number.</param>
        /// <returns>A <see cref="Domain.Models.ResponseModels.PaitentAuthModel"/> found to have the matching 
        /// <paramref name="accountNumber"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="accountNumber"/>is null <c>or</c> the 
        /// <see cref="accountNumber.AccountNumber"/> property is null.</exception>
        /// <exception cref="ArgumentException">If <paramref name="accountNumber.AccountNumber"/>is <c>empty</c>.</exception>
        public async Task<Domain.Models.ResponseModels.PaitentAuthModel> GetPersonDetialsByAccountNoAsync(
            Domain.Models.RequestModels.PaitentAuthModel accountNumber)
        {
            // bool argsAreNotNull = accountNumber?.AccountNumber != null;

            if (null == accountNumber || null == accountNumber.AccountNumber)
            {
                throw new ArgumentNullException(nameof(accountNumber), "Argument: accountNumber can not be null.");
            }

            if (string.IsNullOrWhiteSpace(accountNumber.AccountNumber))
            {
                throw new ArgumentException("Argument: accountNumber can not be null.", nameof(accountNumber));
            }

            Domain.Models.ResponseModels.PaitentAuthModel response = null;

            var basePath = Settings.GetBaseUrl();
            var apiSegment = "/efr/patient";

            var callingThisUri = Path.Combine(Settings.GetBaseUrl(), apiSegment);
            var callingThisUriExtraStep = Path.Combine(basePath, apiSegment);

            try
            {
                using (HttpClient client = new HttpClient())
                // using (HttpResponseMessage response = new HttpResponseMessage())
                {
                    // var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.GetBaseUrl());
                    var data = JsonConvert.SerializeObject(accountNumber);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");

                    // Search for a specific Person (patient) by Account Number.
                    HttpResponseMessage result =
                        await client.PostAsync(Settings.GetBaseUrl() + "/efr/patient", content).ConfigureAwait(false);

                    response = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.PaitentAuthModel>(
                        result.Content.ReadAsStringAsync().Result);

                    // TODO; Move this
                    content.Dispose();
                }
            }
            catch (Exception erf)
            {
                Debug.Write(erf);
            }

            return response;
        }



        #region Check Status And Register
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


        #endregion Check Status And Register


        ///// <summary>
        ///// Searches for a, <c>Person</c> given a specific <paramref name="accountNumber" />..
        ///// </summary>
        ///// <param name="accountNumber">The Person's account number.</param>
        ///// <returns>
        ///// A <see cref="Domain.Models.ResponseModels.PaitentAuthModel" /> found to have the matching
        ///// <paramref name="accountNumber" />.
        ///// </returns>
        ///// <exception cref="ArgumentNullException">If <paramref name="accountNumber" />is null <c>or</c> the
        ///// <see cref="accountNumber.AccountNumber" /> property is null.</exception>
        ///// <exception cref="ArgumentException">If <paramref name="accountNumber,AccountNumber" />is <c>empty</c>.</exception>
        //public async Task<Domain.Models.ResponseModels.PaitentAuthModel> GetPersonDetialsByAccountNoAsync(
        //    Domain.Models.RequestModels.PaitentAuthModel accountNumber)
        //{
        //    // bool argsAreNotNull = accountNumber?.AccountNumber != null;

        //    if (null == accountNumber || null == accountNumber.AccountNumber)
        //    {
        //        throw new ArgumentNullException(nameof(accountNumber), "Argument: accountNumber can not be null.");
        //    }

        //    if (string.IsNullOrWhiteSpace(accountNumber.AccountNumber))
        //    {
        //        throw new ArgumentException("Argument: accountNumber can not be null.", nameof(accountNumber));
        //    }

        //    Domain.Models.ResponseModels.PaitentAuthModel response = null;

        //    var basePath = Settings.GetBaseUrl();
        //    var apiSegment = "/efr/patient";

        //    var callingThisUri = Path.Combine(Settings.GetBaseUrl(), apiSegment);
        //    var callingThisUriExtraStep = Path.Combine(basePath, apiSegment);

        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //            // using (HttpResponseMessage response = new HttpResponseMessage())
        //        {
        //            // var client = new HttpClient();
        //            client.BaseAddress = new Uri(Settings.GetBaseUrl());
        //            var data = JsonConvert.SerializeObject(accountNumber);
        //            var content = new StringContent(data, Encoding.UTF8, "application/json");

        //            // Search for a specific Person (patient) by Account Number.
        //            HttpResponseMessage result =
        //                await client.PostAsync(Settings.GetBaseUrl() + "/efr/patient", content).ConfigureAwait(false);

        //            response = JsonConvert.DeserializeObject<Domain.Models.ResponseModels.PaitentAuthModel>(
        //                result.Content.ReadAsStringAsync().Result);
        //        }

        //    }
        //    catch (Exception erf)
        //    {
        //        Debug.Write(erf);
        //    }

        //    return response;
        //}
    }
}