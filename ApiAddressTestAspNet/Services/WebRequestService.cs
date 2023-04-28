using ApiAddressTestAspNet.DTO;
using ApiAddressTestAspNet.Helpers;
using ApiAddressTestAspNet.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace ApiAddressTestAspNet.Services
{
    public class WebRequestService
    {
        ILogger<WebRequestService> _logger;

        public WebRequestService(ILogger<WebRequestService> logger)
        {
            this._logger = logger;
        }

        private async Task<RestResponse> MakeWebRequest(RestRequest pRestRequest, string pDeUrl)
        {
            RestClient restClient = new RestClient(pDeUrl);
            RestResponse restResponse = null;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                restResponse = await restClient.ExecuteAsync(pRestRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"WebRequestService.MakeWebRequest => Exception: {ex.Message}");
            }

            return restResponse;
        }

        public async Task<ReturnDTO> GetAddressFromViaCEP(string pCodezip)
        {
            string deURL = $"https://viacep.com.br/ws/{pCodezip}/json";
            RestRequest request = new RestRequest(resource:"", method: Method.Get);
            RestResponse response = await this.MakeWebRequest(request, deURL);

            ReturnDTO returnDTO = new ReturnDTO
            {
                ResultObject = null,
                Success = false,
                Message = ConstantManager.DefMessageAddressNotFound
            };

            try
            {
                if (response != null && response.IsSuccessful)
                {
                    AddressViaCEP addressViaCEP =
                        JsonSerializer.Deserialize<AddressViaCEP>(response.Content);

                    if (addressViaCEP != null && !string.IsNullOrEmpty(addressViaCEP.cep))
                    {
                        Address address = new Address(addressViaCEP);

                        _logger.LogInformation("WebRequestService.GetAddressFromViaCEP => Address OK");

                        returnDTO = new ReturnDTO
                        {
                            ResultObject = address,
                            Message = ConstantManager.OK,
                            Success = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"WebRequestService.GetAddressFromViaCEP => Exception: {ex.Message}");
            }

            return returnDTO;
        }

    }
}
