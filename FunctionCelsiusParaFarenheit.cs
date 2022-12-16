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
    public class FunctionCelsiusParaFarenheit
    {
        private readonly ILogger<FunctionCelsiusParaFarenheit> _logger;

        public FunctionCelsiusParaFarenheit(ILogger<FunctionCelsiusParaFarenheit> log)
        {
            _logger = log;
        }

        [FunctionName("CelsiusParaFarenheit")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        [OpenApiParameter(name: "celsius", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **Celsius** para a conversão em Farenheit")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor em Celsius")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterFarenheitParaCelsius/{celsius}")] HttpRequest req, double celsius)
        {
            _logger.LogInformation("Parâmetro recebido: {celsius}", celsius);

            var valorEmFarenheit = (celsius * 1.8) + 32;

            string responseMessage = $"O valor em Celsius é: {valorEmFarenheit}";

            _logger.LogInformation($"Conversão realizada: {responseMessage}");
            return new OkObjectResult(responseMessage);
        }
    }
}

