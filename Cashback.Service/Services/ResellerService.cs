using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cashback.Service.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;
        private readonly IMapper _mapper;
        public ResellerService(IResellerRepository resellerRepository, IMapper mapper)
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
    }
}
