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
            IAuthService authService,
            IMapper mapper)
        {
            this._userRepository = userRepository;
            this._authService = authService;
            this._mapper = mapper;
        }
        
        public bool PostReseller(ResellerRequestDTO reseller)
        {
            Validator.ValidateObject(reseller, new ValidationContext(reseller), true);

            User _user = _mapper.Map<User>(reseller);
            _user.Password = _authService.EncryptPassword(_user.Password); // encrypting the password

            this._userRepository.Create(_user);

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
