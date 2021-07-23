using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;

namespace Cashback.Service.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<ResellerRequestDTO, User>().ReverseMap();
            CreateMap<PurchaseDTO, Purchase>().ReverseMap();
            CreateMap<CashbackPurchaseDTO, Purchase>().ReverseMap();
        }
    }
}
