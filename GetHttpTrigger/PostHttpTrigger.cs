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

namespace GetHttpTrigger
{
    public static class PostHttpTrigger
    {
        [FunctionName("PostHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethods.Post), Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = default;
            var person = await req.Content.ReadAsAsync<Person>();

            ObjectResult result;
            string responseMessage = default;
            name= person.Name;
            if (string.IsNullOrEmpty(name))
            {
                responseMessage = "please provide a value!!";
                result = new BadRequestObjectResult(responseMessage);
            }
            else
            {
                responseMessage = $"hey there {name} from body";
                result = new OkObjectResult(responseMessage);
            }

            return result;
        }
    }
}
