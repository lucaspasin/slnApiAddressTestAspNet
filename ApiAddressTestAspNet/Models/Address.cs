namespace ApiAddressTestAspNet.Models
{
    public class Address
    {
        public Address() { }

        public Address(AddressViaCEP addressViaCEP)
        {
            if (addressViaCEP == null)
            {
                return;
            }
            this.Codezip = addressViaCEP.cep;
            this.NmCountryState = addressViaCEP.uf;
            this.NmCity = addressViaCEP.localidade;
            this.NmNeighborhood = addressViaCEP.bairro;
            this.NmAddress = addressViaCEP.logradouro;
        }

        public Address(ServiceReference1.consultaCEPResponse consultaCEPResponse)
        {
            if (consultaCEPResponse == null)
            {
                return;
            }
            this.Codezip = consultaCEPResponse.@return.cep;
            this.NmCountryState = consultaCEPResponse.@return.uf;
            this.NmCity = consultaCEPResponse.@return.cidade;
            this.NmNeighborhood = consultaCEPResponse.@return.bairro;
            this.NmAddress = consultaCEPResponse.@return.end;
        }

        public string Codezip { get; set; } = string.Empty;
        public string NmCountryState { get; set; } = string.Empty;
        public string NmCity { get; set; } = string.Empty;
        public string NmNeighborhood { get; set; } = string.Empty;
        public string NmAddress { get; set; } = string.Empty;
        public string NuAddressNumber { get; set; } = string.Empty;
    }
}
