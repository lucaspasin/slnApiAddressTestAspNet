using ApiAddressTestAspNet.DTO;
using ApiAddressTestAspNet.Helpers;
using ApiAddressTestAspNet.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAddressTestAspNet.Services
{
    public interface IJwtService
    {
        Task<ReturnDTO> Authenticate(string username, string password);
    }

    public class JwtService : IJwtService
    {

        private readonly AppSettings _appSettings;
        private ILogger<JwtService> _logger;


        public JwtService(ILogger<JwtService> logger, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task<ReturnDTO> Authenticate(string username, string password)
        {
            ReturnDTO returnDTO =
                new ReturnDTO { Success = false, Message = ConstantManager.DefMessageFailAuth, ResultObject = null };

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                returnDTO.Message = ConstantManager.MessageFailAuthEmpty;
                return returnDTO;
            }

            if (username == "abuda" && password == "buda")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                DateTime dtExpiration = DateTime.UtcNow.AddMinutes(_appSettings.MinutesToExpire);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ConstantManager.CodeHash, password)
                    });

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = dtExpiration,
                    NotBefore = DateTime.UtcNow,
                    Subject = claimsIdentity,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

                Token token = new Token
                {
                    AccessToken = tokenHandler.WriteToken(securityToken),
                    DateTimeExpiration = dtExpiration,
                    TotalSecondsExpiration = _appSettings.MinutesToExpire * 60
                };

                returnDTO.Success = true;
                returnDTO.ResultObject = token;
                returnDTO.Message = ConstantManager.OK;
                return returnDTO;
            }

            return returnDTO;

        }
        public string GetNuHash(string pToken)
        {
            string nuhash = string.Empty;
            try
            {
                JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(pToken);
                nuhash = jwt.Claims.FirstOrDefault(c => c.Type == ConstantManager.CodeHash).Value;
            }
            catch (Exception ex)
            {
                nuhash = string.Empty;
                _logger.LogError($"JwtService.GetNuHash => Exception: {ex.Message}");
            }

            return nuhash;
        }

        public string GetTokenFromReqHeader(IHeaderDictionary pReqHeaders)
        {
            string acessToken = string.Empty;
            try
            {
                Dictionary<string, string> dicHeaders = pReqHeaders.ToDictionary(h => h.Key, h => string.Join(";", h.Value));
                acessToken = dicHeaders[ConstantManager.Authorization];
                acessToken = acessToken.Replace("Bearer ", "");
            }
            catch (Exception ex)
            {
                acessToken = string.Empty;
                _logger.LogError($"JwtService.GetTokenFromReqHeader => Exception: {ex.Message}");
            }

            return acessToken;
        }
    }
}
