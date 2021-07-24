using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public void Post_SendingValidObjectNotFound()
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

        [Fact]
        public void Put_CalculatingCashback()
        {
            CashbackPurchaseDTO purchase1 = new CashbackPurchaseDTO(){
                                                                         Code = "XXUYHR", 
                                                                         Value = 900, 
                                                                         Date = DateTime.Now, 
                                                                         Cpf = 12345678912
                                                                     };
            CashbackPurchaseDTO purchase2 = new CashbackPurchaseDTO()
                                            {
                                                Code = "XXUYHT",
                                                Value = 1100,
                                                Date = DateTime.Now,
                                                Cpf = 12345678912
                                            };
            CashbackPurchaseDTO purchase3 = new CashbackPurchaseDTO()
                                            {
                                                Code = "XXUYHY",
                                                Value = 1600,
                                                Date = DateTime.Now,
                                                Cpf = 12345678912
                                            };

            purchaseService.CashbackCalculator(purchase1);
            purchaseService.CashbackCalculator(purchase2);
            purchaseService.CashbackCalculator(purchase3);

            Assert.True(purchase1.CashbackPerc.Equals("10%"));
            Assert.True(purchase1.CashbackValue.Equals(90));
            Assert.True(purchase2.CashbackPerc.Equals("15%"));
            Assert.True(purchase2.CashbackValue.Equals(165));
            Assert.True(purchase3.CashbackPerc.Equals("20%"));
            Assert.True(purchase3.CashbackValue.Equals(320));
        }
    }
}
