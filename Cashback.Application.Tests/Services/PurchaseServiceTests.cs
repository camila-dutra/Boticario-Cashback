using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Service.AutoMapper;
using Cashback.Service.Services;
using Moq;
using Xunit;

namespace Cashback.Application.Tests.Services
{
    public class PurchaseServiceTests
    {
        private PurchaseService purchaseService;
        private readonly Mock<IPurchaseRepository> _purchaseRepository;
        private readonly Mock<IUserStatusRepository> _userStatusRepository;
        private readonly Mock<IUserRepository> _userRepository;

        public PurchaseServiceTests()
        {
            _purchaseRepository = new Mock<IPurchaseRepository>();
            _userStatusRepository = new Mock<IUserStatusRepository>();
            _userRepository = new Mock<IUserRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
                                                     {
                                                         cfg.AddProfile(new AutoMapperSetup());
                                                     });
            var mapper = mockMapper.CreateMapper();

            purchaseService = new PurchaseService(_purchaseRepository.Object,
                _userStatusRepository.Object,
                _userRepository.Object,
                mapper);

        }

        [Fact]
        public void Post_SendingValidObject()
        {
            var exception = Assert.Throws<Exception>(() => purchaseService.PostPurchase(new PurchaseDTO()
                                                         {
                                                             Code = "XIUJHY",
                                                             Value = 800,
                                                             Date = DateTime.Now,
                                                             Cpf = 12345612345
                                                         }));
            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public void Post_SendindDomainInvalidObject()
        {
            var exception = Assert.Throws<ValidationException>(() => purchaseService.PostPurchase(new PurchaseDTO()
                                                                   {
                                                                       Value = 800,
                                                                       Date = DateTime.Now,
                                                                       Cpf = 12345678912
                                                                   }));
            Assert.Equal("The Code field is required.", exception.Message);
        }
    }
}
