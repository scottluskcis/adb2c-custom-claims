using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Contoso.AADB2C.API.Extensions;
using Contoso.AADB2C.API.Models;
using Newtonsoft.Json;

namespace Contoso.AADB2C.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ILogger _logger;

        public IdentityService(ILogger<IdentityService> logger)
        {
            _logger = logger;
        }

        public async Task<OutputClaimsModel> ProcessAsync(HttpRequest request)
        {
            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(ProcessAsync)} - Start");

            var inputClaims = await ParseInputClaimsAsync(request);
            var outputClaims = await SetOutputClaimsAsync(inputClaims);

            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(ProcessAsync)} - End");

            return outputClaims;
        }

        public Task<OutputClaimsModel> SetOutputClaimsAsync(InputClaimsModel inputClaims)
        {
            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(SetOutputClaimsAsync)} - Start");

            if (inputClaims == null)
                throw new ArgumentNullException(nameof(inputClaims));

            // Create an output claims object and set the loyalty number with a random value
            var outputClaims = new OutputClaimsModel
            {
                loyaltyNumber = new Random().Next(100, 1000).ToString()
            };

            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(SetOutputClaimsAsync)} - End");

            return Task.FromResult(outputClaims);
        }

        public async Task<InputClaimsModel> ParseInputClaimsAsync(HttpRequest request)
        {
            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(ParseInputClaimsAsync)} - Start");

            // input data required
            if (request?.Body == null)
                throw new ArgumentException("No request body content was found");

            // read the input claims from the request body
            var input = await request.GetRawBodyStringAsync();

            // input is required, throw exception if not found
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Request content is empty");

            // convert the input string into expected model
            var inputClaims = JsonConvert.DeserializeObject<InputClaimsModel>(input);

            // validate we have the expected object
            if (inputClaims == null)
                throw new ArgumentException("Can not deserialize input claims");

            // Run a simple input validation
            if (inputClaims.firstName.ToLower() == "test")
                throw new ArgumentException("Test name is not valid, please provide a valid name");

            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(ParseInputClaimsAsync)} - End");

            // return the parsed claims
            return inputClaims;
        }

    }
}