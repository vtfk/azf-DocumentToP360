using Communicate.Utilities.Archeo;
using DocumentToP360.Models.P360;
using DocumentToP360.Models.P360.CreateCase;
using DocumentToP360.Models.P360.CreateDocument;
using DocumentToP360.Models.P360.EnterpriseDepartmentLookup;
using DocumentToP360.Models.P360.PrivatePersonLookup;
using DocumentToP360.Models.P360.PrivatePersonSync;
using DocumentToP360.Models.P360.UpdateCase;
using DocumentToP360.Models.VTFK;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace DocumentToP360
{
    public class HandleRequest
    {
        private static ILogger _globalLogger;

        [FunctionName("HandlePerson")]
        public static async Task Run([OrchestrationTrigger]IDurableOrchestrationContext context, ILogger log)
        {
            _globalLogger = log;

            //Input from VTFK 
            var docToP360Request = context.GetInput<(DocToP360Request DocToP360Request, string archeoTransGuid)>();

            PrivatePersonLookupRequest privatePersonLookupRequest = (PrivatePersonLookupRequest)docToP360Request.DocToP360Request;
            
            //Check if the personalIdNumber exsits in P360
            if (!await context.CallActivityAsync<bool>("DoesPersonExsist", (privatePersonLookupRequest, docToP360Request.archeoTransGuid)))
            {
                //if not, create a user with given input
                PrivatePersonSyncRequest privatePersonSyncRequest = (PrivatePersonSyncRequest)docToP360Request.DocToP360Request;

                await context.CallActivityAsync("CreatePrivatePerson", (privatePersonSyncRequest, docToP360Request.archeoTransGuid));

            }

            //
            EnterpriseDepartmentLookupRequest enterpriseDepartmentLookupRequest = (EnterpriseDepartmentLookupRequest)docToP360Request.DocToP360Request.parameter;

            var recNO = await context.CallActivityAsync<int>("GetEnterPriseRecno", (enterpriseDepartmentLookupRequest, docToP360Request.archeoTransGuid));

            CreateCaseRequest createCaseRequest = (CreateCaseRequest)(docToP360Request.DocToP360Request, recNO);

            createCaseRequest.parameter.ResponsibleEnterpriseRecno = recNO;

            var caseNumber = await context.CallActivityAsync<string>("CreateCase", (createCaseRequest, docToP360Request.archeoTransGuid));

            CreateDocumentRequest createDocumentRequest = (CreateDocumentRequest)(docToP360Request.DocToP360Request, caseNumber, recNO);

            CreateDocumentResponse createDocumentResponse = await context.CallActivityAsync<CreateDocumentResponse>("CreateDocument", (createDocumentRequest, docToP360Request.archeoTransGuid));

            UpdateCaseRequest updateCaseRequest = (UpdateCaseRequest)(docToP360Request.DocToP360Request, caseNumber);
            
     
            UpdateCaseResponse UpdateCaseResponse = await context.CallActivityAsync<UpdateCaseResponse>("UpdateCase", (updateCaseRequest, docToP360Request.archeoTransGuid));



        }

        [FunctionName("DoesPersonExsist")]
        public static async Task<bool> DoesPersonExsist([ActivityTrigger] IDurableActivityContext context)
        {
            var privatePersonLookupRequest = context.GetInput<(PrivatePersonLookupRequest personLookupRequest, string archeoId)>();

            PrivatePersonLookupResponse responseGetPrivatePersons = await RestService.RestService.GetPrivatePerson(privatePersonLookupRequest.personLookupRequest, privatePersonLookupRequest.archeoId);

            if (responseGetPrivatePersons.PrivatePersons.Count() == 0)
            {
                _globalLogger.LogInformation("Private person does NOT exsist");
                return false;
            }


            _globalLogger.LogInformation("Private person does exsist");
            return true;
        }

        [FunctionName("CreatePrivatePerson")]
        public static async Task<PrivatePersonSyncResponse> CreatePrivatePerson([ActivityTrigger] IDurableActivityContext context)
        {
            var contextInput = context.GetInput<(PrivatePersonSyncRequest privatePersonSyncRequest, string archeoId)>();

            PrivatePersonSyncResponse privatePersonSynResponse = await RestService.RestService.PrivatePersonSync(contextInput.privatePersonSyncRequest, contextInput.archeoId);

            if (privatePersonSynResponse.Recno > 0 && privatePersonSynResponse.Successful)
            {
                return privatePersonSynResponse;
            }
            return null;
        }

        [FunctionName("GetEnterPriseRecno")]
        public static async Task<int> GetEnterPriseRecno([ActivityTrigger] IDurableActivityContext context)
        {
            var contextInput = context.GetInput<(EnterpriseDepartmentLookupRequest enterpriseDepartmentLookupRequest, string archeoId)>();

            EnterpriseDepartmentLookupResponse enterpriseDepartmentLookupResponse = await RestService.RestService.GetEnterpriseRecno(contextInput.enterpriseDepartmentLookupRequest, contextInput.archeoId);

            if (enterpriseDepartmentLookupResponse.Enterprises?.Count() == 0)
                return 0;

            return enterpriseDepartmentLookupResponse.Enterprises.FirstOrDefault().Recno;
        }

        [FunctionName("CreateCase")]
        public static async Task<string> CreateCase([ActivityTrigger] IDurableActivityContext context)
        {
            var contextInput = context.GetInput<(CreateCaseRequest createCaseRequest, string archeoId)>();

            CreateCaseResponse createCaseResponse = await RestService.RestService.CreateCase(contextInput.createCaseRequest, contextInput.archeoId);

            if (string.IsNullOrEmpty(createCaseResponse.CaseNumber))
            {
                return null;
            }

            return createCaseResponse.CaseNumber;
        }

        [FunctionName("CreateDocument")]
        public static async Task<CreateDocumentResponse> CreateDocument([ActivityTrigger] IDurableActivityContext context)
        {
            var contextInput = context.GetInput<(CreateDocumentRequest createDocumentRequest, string archeoId)>();

            CreateDocumentResponse createDocumentResponse = await RestService.RestService.CreateDocument(contextInput.createDocumentRequest, contextInput.archeoId);

            return createDocumentResponse;
        }

        [FunctionName("UpdateCase")]
        public static async Task<UpdateCaseResponse> UpdateCase([ActivityTrigger] IDurableActivityContext context)
        {
            var contextInput = context.GetInput<(UpdateCaseRequest updateCaseRequest, string archeoId)>();

            UpdateCaseResponse updateCaseResponse = await RestService.RestService.UpdateCase(contextInput.updateCaseRequest, contextInput.archeoId);

            return updateCaseResponse;
        }

    }
}
