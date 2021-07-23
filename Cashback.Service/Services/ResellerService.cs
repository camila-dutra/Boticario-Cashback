using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;

using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cashback.Service.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;
        private readonly IMapper _mapper;

        public ResellerService(IResellerRepository resellerRepository, 
            IMapper mapper)
        {
            this._resellerRepository = resellerRepository;
            this._mapper = mapper;
        }
        
        public bool PostReseller(ResellerRequestDTO reseller)
        {
            Validator.ValidateObject(reseller, new ValidationContext(reseller), true);

            User _user = _mapper.Map<User>(reseller);
            //_user.Password = EncryptPassword(_user.Password); // encrypting the password

            this._resellerRepository.Create(_user);

            return true;
        }

        public async Task<object> GetResellerCashback(long cpf)
        {
            using (var client = new HttpClient())
            {
                cpf = (cpf > 0) ? cpf : 12312312323;
                string url = "https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/cashback?cpf=" + cpf;
                client.DefaultRequestHeaders.Add("token", "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");

                var httpResponse = await client.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonString = await httpResponse.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<CashbackResponseDTO>(jsonString);

                    return new {responseJson?.Body?.Credit};
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
