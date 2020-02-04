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

        public Task<OutputClaimsModel> SignUpAsync(InputClaimsModel model)
        {
            var result = Process(model, IdentityAction.SignUp);
            return Task.FromResult(result);
        }

        public Task<OutputClaimsModel> SignInAsync(InputClaimsModel model)
        {
            var result = Process(model, IdentityAction.SignIn);
            return Task.FromResult(result);
        }

        private OutputClaimsModel Process(InputClaimsModel model, IdentityAction action)
        {
            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(Process)} - Start");

            var isValid = action == IdentityAction.SignUp
                ? ValidateInputClaims(model)
                : true;
            
            if (!isValid)
                throw new ArgumentException("Validation failed for InputClaims.");

            var outputClaims = new OutputClaimsModel
            {
                loyaltyNumber = new Random().Next(100, 1000).ToString(),
                action = $"{action.ToString()} - generate random number for loyaltyNumber"
            };

            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(Process)} - End");

            return outputClaims;
        }

        private async Task<InputClaimsModel> ParseInputClaimsAsync(HttpRequest request)
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

            _logger.LogInformation($"{nameof(IdentityService)}.{nameof(ParseInputClaimsAsync)} - End");

            // return the parsed claims
            return inputClaims;
        }

        private bool ValidateInputClaims(InputClaimsModel inputClaims)
        {
            // Run a simple input validation
            if (inputClaims.firstName.ToLower() == "test")
                throw new ArgumentException("Test name is not valid, please provide a valid name");

            // validation passes
            return true;
        }

    }
}