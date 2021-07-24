using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.AutoMapper;
using Cashback.Service.Interfaces;
using Cashback.Service.Services;
using Moq;
using Xunit;

namespace Cashback.Application.Tests.Services
{
    public class ResellerServiceTests
    {
        private ResellerService resellerService;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IAuthService> _authService;
        //private readonly IMapper _mapper;

        public ResellerServiceTests()
        {
            
            _userRepository = new Mock<IUserRepository>();
            _authService = new Mock<IAuthService>();
            var mockMapper = new MapperConfiguration(cfg =>
                                                     {
                                                         cfg.AddProfile(new AutoMapperSetup());
                                                     });
            var mapper = mockMapper.CreateMapper();

            resellerService = new ResellerService(_userRepository.Object,
                mapper,
                _authService.Object);
            
        }

        [Fact]
        public async void GetResellerCashback_InvalidCPF()
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => resellerService.GetResellerCashback(123));
            Assert.Equal("CPF is not valid", exception.Message);
        }

        [Fact]
        public void Post_SendingValidObject()
        {
            var result = resellerService.PostReseller(new ResellerRequestDTO {
                                                                                 Name = "Camila Martins",
                                                                                 Cpf = 12345612345,
                                                                                 Email = "cmartinsdutra@gmail.com",
                                                                                 Password = "111222"});
                Assert.True(result);
        }

        [Fact]
        public void Post_SendingDomainInvalidObject()
        {
            var exception =  Assert.Throws<ValidationException>(() => resellerService.PostReseller(new ResellerRequestDTO
                                                                    {
                                                                        Name = "Camila Martins",
                                                                        Cpf = 12345612345,
                                                                        Email = "cmartinsdutra@gmail.com",
                                                                    }));

            Assert.Equal("The Password field is required.", exception.Message);
        }




    }
}
