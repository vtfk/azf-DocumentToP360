using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DocumentToP360.Models.VTFK;
using System.Collections.Generic;
using DocumentToP360.Models.P360.EnterpriseDepartmentLookup;
using System.Linq;

namespace DocumentToP360
{
    public static class HttpSyncStart
    {
        //Starts the service and sends the request to the orchestration
        [FunctionName("HttpSyncStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, methods: "post", Route = "orchestrators/{functionName}/wait")]
            HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string functionName,
            ILogger log)
        {
            //Creates a guid before starting the orchestration and reads input from user
            string transactionId = Guid.NewGuid().ToString();
            DocToP360Request eventData = await req.Content.ReadAsAsync<DocToP360Request>();
            string instanceId = await starter.StartNewAsync(functionName, transactionId, (eventData, transactionId));

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
 
            //Creates a JSON-file that includes all the input parameters for the orchestration.
            /*
            System.IO.File.WriteAllText(@"FILEPATH", JsonConvert.SerializeObject(new DocToP360Request()
            {

                parameter = new Parameter()
                {
                    PrivateAddress = new Privateaddress()
                    {

                    },
                    ArchiveCodes = new List<Archivecode> { new Archivecode(){
                    } },
                    Files = new List<Models.VTFK.File>()
                    {
                        new Models.VTFK.File(){}
                    }


                }
            }, Formatting.Indented));*/

            var res = starter.CreateCheckStatusResponse(req, transactionId);
            return res;
        }
    }
}
