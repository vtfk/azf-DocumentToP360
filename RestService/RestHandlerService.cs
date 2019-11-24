using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using DocumentToP360.Models.P360.CreateCase;
using Communicate.Utilities.Archeo;
using DocumentToP360.Models.P360.UpdateCase;
using DocumentToP360.Models.P360.CreateDocument;
using System.Collections.Generic;
using DocumentToP360.Models.P360.PrivatePersonLookup;
using DocumentToP360.Models.P360.PrivatePersonSync;
using DocumentToP360.Models.P360.EnterpriseDepartmentLookup;
using System.Configuration;

namespace DocumentToP360.RestService
{

    public static class RestService
    {
        //HttpClient
        public static HttpClient newClient = new HttpClient()
        {

        };

        //Global variables
        public static string _ArchKey = Environment.GetEnvironmentVariable("ArcheoApiKey");
        public static string _BaseUrl = Environment.GetEnvironmentVariable("BaseURL");
        public static string _ApiAuthKey = Environment.GetEnvironmentVariable("a_Key");
        public static ArcheoLogger _ArchLogger = new ArcheoLogger(null, new ArcheoConfiguration() { ApiKey = _ArchKey });

        //GetPrivatePersons - Checks if a person exist with given PersonalIdNumber from input
        public static async Task<PrivatePersonLookupResponse> GetPrivatePerson(PrivatePersonLookupRequest privatePersonLookupRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(privatePersonLookupRequest)),
                "Request to GetPrivatePersons",
                "privatePersonLookupRequest.json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(privatePersonLookupRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/ContactService/GetPrivatePersons?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<PrivatePersonLookupResponse>();

                    //Logs response from P360
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from GetPrivatePersons",
                    "PrivatePersonLookupResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //If server response isn't 200
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://360.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }


            }
            catch (Exception ex)
            {
                //logs Exception
                _ArchLogger.LogException(ex, "Error during GetPrivatePersons execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends all logs to Archeo
                await _ArchLogger.SendLogs();

            }
        }

        //SynchronizePrivatePerson - Creates a new person with given input parameters 
        public static async Task<PrivatePersonSyncResponse> PrivatePersonSync(PrivatePersonSyncRequest privatePersonSyncRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(privatePersonSyncRequest)),
                "Request to SynchronizePrivatePerson",
                "privatePersonSyncRequest.Json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(privatePersonSyncRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/ContactService/SynchronizePrivatePerson?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<PrivatePersonSyncResponse>();

                    //Logs response from P360
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from SynchronizePrivatePerson",
                    "PrivatePersonSyncResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //If server response isn't 200
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://test.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }


            }
            catch (Exception ex)
            {
                //logs Exception
                _ArchLogger.LogException(ex, "Error during SynchronizePrivatePerson execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends all logs to Archeo
                await _ArchLogger.SendLogs();

            }
        }

        //GetEnterprise - Gets Recno for a department with given Initials parameter from input
        public static async Task<EnterpriseDepartmentLookupResponse> GetEnterpriseRecno(EnterpriseDepartmentLookupRequest enterpriseDepartmentLookupRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(enterpriseDepartmentLookupRequest)),
                "Request to GetEnterprise",
                "EnterpriseDepartmentLookupRequest.Json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(enterpriseDepartmentLookupRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/ContactService/GetEnterprises?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<EnterpriseDepartmentLookupResponse>();

                    //Logs the response
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from GetEnterprise",
                    "EnterpriseDepartmentLookupResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //If server response isn't 200
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://360test.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }

            }
            catch (Exception ex)
            {
                //logs Exception
                _ArchLogger.LogException(ex, "Error during GetEnterprise execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends log to Archeo
                await _ArchLogger.SendLogs();

            }
        }

        //CreateCase - Creates a case with given input parameters
        public static async Task<CreateCaseResponse> CreateCase(CreateCaseRequest createCaseRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(createCaseRequest)),
                "Request to CreateCase",
                "CreateCaseRequest.Json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(createCaseRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/CaseService/CreateCase?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<CreateCaseResponse>();

                    //Logs the response
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from CreateCase",
                    "CreateCaseResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //If server response isn't 200
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://360.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }


            }
            catch (Exception ex)
            {
                //logs exception
                _ArchLogger.LogException(ex, "Error during CreateCase execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends log to Archeo
                await _ArchLogger.SendLogs();

            }
        }

        //UpdateCase - Updates a case with given status parameter
        public static async Task<UpdateCaseResponse> UpdateCase(UpdateCaseRequest updateCaseRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(updateCaseRequest)),
                "Request to UpdateCase",
                "UpdateCaseRequest.Json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(updateCaseRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/CaseService/UpdateCase?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<UpdateCaseResponse>();

                    //Logs the response
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from UpdateCase",
                    "UpdateCaseResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //If server response isn't 200
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://360.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }
            }
            catch (Exception ex)
            {
                //logs exception
                _ArchLogger.LogException(ex, "Error during UpdateCase execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends log to Archeo
                await _ArchLogger.SendLogs();

            }
        }

        //CreateDocument - Gets document from BLOB storage and sends it to P360
        public static async Task<CreateDocumentResponse> CreateDocument(CreateDocumentRequest createDocumentRequest, string transactionId)
        {
            try
            {
                //Logs the request
                _ArchLogger.Log(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(createDocumentRequest)),
                "Request to CreateDocument",
                "CreateDocumentRequest.Json",
                transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Success",
                processed: DateTime.UtcNow);

                //Get the document for given case. Document from BLOB-URL
                string filepath = createDocumentRequest.parameter.URL;
                var response = await newClient.GetAsync(createDocumentRequest.parameter.URL);
                System.IO.MemoryStream stream = (System.IO.MemoryStream)await response.Content.ReadAsStreamAsync();


                //Converts the file to ByteArray
                byte[] bytes = stream.ToArray();

                //If List is empty, create a new one and add the following parameters
                if (createDocumentRequest.parameter.Files == null)
                {
                    createDocumentRequest.parameter.Files = new List<File>();
                }
                createDocumentRequest.parameter.Files.Add(new File()
                {
                    Data = bytes,
                    Format = "pdf",
                    Title = createDocumentRequest.parameter.Title,
                    URL = createDocumentRequest.parameter.URL,
                    Status = createDocumentRequest.parameter.StatusFile
                });

                //Json serializer
                var stringContent = new StringContent(JsonConvert.SerializeObject(createDocumentRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await newClient.PostAsync(_BaseUrl + "/DocumentService/CreateDocument?authKey=" + _ApiAuthKey, stringContent);

                if (result.IsSuccessStatusCode)
                {
                    //Read Server Response
                    var responseData = await result.Content.ReadAsAsync<CreateDocumentResponse>();

                    //Logs the response
                    _ArchLogger.Log(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData)),
                    "Response from CreateDocument",
                    "CreateDocumentResponse.json",
                    transactionId: transactionId,
                    transactionType: "DocumentToP360",
                    status: "Success",
                    processed: DateTime.UtcNow);
                    return responseData;
                }
                else
                {
                    //logs exception
                    _ArchLogger.LogHttpFailure(
                      response: result,
                      transactionId: transactionId,
                      transactionType: "DocumentToP360",
                      status: "Error",
                      description: "Return code to https://360.vtfk.no was NOT 200",
                      processed: DateTime.UtcNow);
                    return null;
                }


            }
            catch (Exception ex)
            {
                //logs exception
                _ArchLogger.LogException(ex, "Error during UpdateCase execution", transactionId: transactionId,
                transactionType: "DocumentToP360",
                status: "Error",
                logTimestamp: DateTime.UtcNow);
                return null;
            }
            finally
            {
                //Sends log to Archeo
                await _ArchLogger.SendLogs();

            }
        }
    }
}

