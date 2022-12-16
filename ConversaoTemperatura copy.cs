using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ConversaoTemperatura
{
    public class ConversaoTemperatura
    {
        private readonly ILogger<ConversaoTemperatura> _logger;

        public ConversaoTemperatura(ILogger<ConversaoTemperatura> log)
        {
            _logger = log;
        }

        [FunctionName("ConverterFarenheitParaCelsius")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        [OpenApiParameter(name: "farenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **farenheit** para a conversão em Celsius")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor em Celsius")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterFarenheitParaCelsius/{farenheit}")] HttpRequest req, double farenheit)
        {
            _logger.LogInformation("Parâmetro recebido: {farenheit}", farenheit);

            var valorEmCelsius = (farenheit - 32) / 1.8;

            string responseMessage = $"O valor em Celsius é: {valorEmCelsius}";

            _logger.LogInformation($"Conversão realizada: {responseMessage}");
            return new OkObjectResult(responseMessage);
        }
    }
}

