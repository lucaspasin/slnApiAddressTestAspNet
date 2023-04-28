using ApiAddressTestAspNet.DTO;
using ApiAddressTestAspNet.Models;

namespace ApiAddressTestAspNet.Services
{
    public class AddressService
    {
        ILogger<AddressService> _logger;
        private WebRequestService _webRequestService;

        public AddressService(ILogger<AddressService> logger, WebRequestService webRequestService)
        {
            _logger = logger;
            _webRequestService = webRequestService;
        }

        public async Task<ReturnDTO> GetAddressByCodezip(string pCodezip)
        {

            if (!pCodezip.Length.Equals(8) || pCodezip.Length.Equals(8) && !Int32.TryParse(pCodezip, out int numValue))
            {
                return new ReturnDTO { ResultObject = null, Success = false, Message = "O CEP informado é inválido!" };
            }

            ReturnDTO returnDTO = await _webRequestService.GetAddressFromViaCEP(pCodezip);

            return returnDTO;
        }

        public async Task<ReturnDTO> GetAddressByCodezipCorreios(string pCodezip)
        {

            if (!pCodezip.Length.Equals(8) || pCodezip.Length.Equals(8) && !Int32.TryParse(pCodezip, out int numValue))
            {
                return new ReturnDTO { ResultObject = null, Success = false, Message = "O CEP informado é inválido!" };
            };

            ServiceReference1.AtendeClienteClient atendeCliente = new ServiceReference1.AtendeClienteClient();

            ServiceReference1.consultaCEPResponse consultaCEPResponse = await atendeCliente.consultaCEPAsync(pCodezip);

            Address address = new Address(consultaCEPResponse);

            return new ReturnDTO { ResultObject = address, Success = true, Message = "OK" };
        }
    }
}
