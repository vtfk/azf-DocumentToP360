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

namespace DocumentToP360
{
    public static class HttpSyncStart
    {
        //Starting the orchestration
        [FunctionName("HttpSyncStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, methods: "post", Route = "orchestrators/{functionName}/wait")]
            HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string functionName,
            ILogger log)
        {

            string newInstanceId = Guid.NewGuid().ToString();
            object eventData = await req.Content.ReadAsAsync<object>();
            string instanceId = await starter.StartNewAsync(functionName, newInstanceId, (eventData, newInstanceId));

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
            var res = starter.CreateCheckStatusResponse(req, instanceId);
            return res;
        }
    }
}
