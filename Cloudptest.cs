using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;

namespace cloudproj.Function
{
    public class Cloudptest
    {
        private readonly ILogger<Cloudptest> _logger;

        public Cloudptest(ILogger<Cloudptest> logger)
        {
            _logger = logger;
        }

        [Function("Cloudptest")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            if (req.Method == HttpMethods.Post)
            {
                using var reader = new StreamReader(req.Body);
                var body = await reader.ReadToEndAsync();
                var data = JsonSerializer.Deserialize<YourDataType>(body);
                _logger.LogInformation($"Received name: {data?.Name}, value: {data?.Value}");
                return new OkObjectResult($"Received name: {data?.Name}, value: {data?.Value}");
            }
            
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    public class YourDataType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
