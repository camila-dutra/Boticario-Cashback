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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ResellerService(IUserRepository userRepository,
            IMapper mapper,
            IAuthService authService)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._authService = authService;
        }
        
        public bool PostReseller(ResellerRequestDTO reseller)
        {
            Validator.ValidateObject(reseller, new ValidationContext(reseller), true);

            User _user = _mapper.Map<User>(reseller);
            _user.Password = _authService.EncryptPassword(_user.Password); // encrypting password

            this._userRepository.Create(_user);

            return true;
        }

        public async Task<object> GetResellerCashback(long cpf)
        {
            if (cpf > 0)
            {
                if (cpf.ToString().Length != 11)
                    throw new Exception("CPF is not valid");
            }
            else
            {
                cpf = 12312312323;
            }
            
            using (var client = new HttpClient())
            {
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
