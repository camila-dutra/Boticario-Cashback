using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cashback.Service.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IResellerRepository _resellerRepository;
        private readonly IMapper _mapper;

        public ResellerService(IResellerRepository resellerRepository, IMapper mapper, IHttpClientFactory clientFactory)
        {
            this._resellerRepository = resellerRepository;
            this._mapper = mapper;
            this._clientFactory = clientFactory;
        }
        
        public bool PostReseller(ResellerRequestDTO reseller)
        {
            Validator.ValidateObject(reseller, new ValidationContext(reseller), true);

            User _user = _mapper.Map<User>(reseller);
            //_user.Password = EncryptPassword(_user.Password); // encrypting the password

            this._resellerRepository.Create(_user);

            return true;
        }

        public async Task<CashbackResponseDTO> GetResellerCashback(long cpf)
        {
            CashbackResponseDTO cashback = new CashbackResponseDTO();
            if (cpf <= 0)
                throw new Exception("CPF is not valid");

            User _user = this._resellerRepository.Find(x => x.Cpf == cpf);
            if (_user == null)
                throw new Exception("User not found");

            string url = "https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/cashback?cpf=" + _user.Cpf;

            cashback = await OnGet(url, cashback);

            return cashback;
        }

        public async Task<CashbackResponseDTO> OnGet(string url, CashbackResponseDTO cashback)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("token", "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var responseStream = await response.Content.ReadAsStreamAsync();
            cashback = await JsonSerializer.DeserializeAsync<CashbackResponseDTO>(responseStream);

            return cashback;
        }
    }
}
