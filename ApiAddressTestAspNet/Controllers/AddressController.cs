using ApiAddressTestAspNet.DTO;
using ApiAddressTestAspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAddressTestAspNet.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : Controller
    {

        private AddressService _addressService;
        ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger, AddressService addressService)
        {
            this._addressService = addressService;
            this._logger = logger;
        }

        // GET: api/address/find-by-codezip/{codezip}
        /// <summary>
        /// Obtem um endereço de acordo com o cep informado
        /// </summary>
        /// <param name="codezip"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o endereço relacionado ao CEP informado</response>
        /// <response code="404">Endereço informado não localizado!</response>
        [ProducesResponseType(typeof(ReturnAddressDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnDTO), StatusCodes.Status404NotFound)]
        [Route("find-by-codezip/{codezip}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAddress(string codezip)
        {
            ReturnDTO returnDTO = await _addressService.GetAddressByCodezip(codezip);

            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }

            return new NotFoundObjectResult(returnDTO);
        }

        // GET: api/address/find-by-codezipcorreio/{codezip}
        /// <summary>
        /// Obtem um endereço de acordo com o cep informado
        /// </summary>
        /// <param name="codezip"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o endereço relacionado ao CEP informado</response>
        /// <response code="404">Endereço informado não localizado!</response>
        [ProducesResponseType(typeof(ReturnAddressDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnDTO), StatusCodes.Status404NotFound)]
        [Route("find-by-codezipcorreio/{codezip}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAddressCorreio(string codezip)
        {
            ReturnDTO returnDTO = await _addressService.GetAddressByCodezipCorreios(codezip);

            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }

            return new NotFoundObjectResult(returnDTO);
        }
    }
}
