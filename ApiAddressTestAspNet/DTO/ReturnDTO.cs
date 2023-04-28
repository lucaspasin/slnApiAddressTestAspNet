using ApiAddressTestAspNet.Models;

namespace ApiAddressTestAspNet.DTO
{
    public class ReturnBaseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ReturnDTO : ReturnBaseDTO
    {
        public object ResultObject { get; set; }
    }

    public class ReturnTokenDTO : ReturnBaseDTO
    {
        public Token token { get; set; }
    }

    public class ReturnAddressDTO : ReturnBaseDTO
    {
        public Address address { get; set; }
    }
}
