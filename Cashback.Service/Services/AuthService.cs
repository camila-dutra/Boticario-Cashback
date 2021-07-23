using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Cashback.Auth.Services;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;

namespace Cashback.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }


        public UserAuthenticateResponseDTO Authenticate(UserAuthenticateRequestDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Email/Password are required.");

            user.Password = EncryptPassword(user.Password);

            User _user = this._userRepository.Find(x => x.Email.ToUpper() == user.Email.ToUpper()
                                                        && x.Password.ToUpper() == user.Password.ToUpper());
            if (_user == null)
                throw new Exception("User not found");


            return new UserAuthenticateResponseDTO(_mapper.Map<ResellerRequestDTO>(_user), TokenService.GenerateToken(_user));
        }

        public string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
