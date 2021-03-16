using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Empresa.Projeto_Demanda
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "cadastro")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];


            AzureSqlRepository azureSqlRepository = new AzureSqlRepository();
            if(req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UserSql data = System.Text.Json.JsonSerializer.Deserialize<UserSql>(requestBody,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                azureSqlRepository.SaveDapper(data);
                return new OkObjectResult("Saved");
            } else if(req.Method == HttpMethods.Get)
            {
                var itens = azureSqlRepository.GetAll();

                return new OkObjectResult(itens);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}

