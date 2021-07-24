using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cashback.Data.Interfaces;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Domain.Enums;
using Cashback.Service.Interfaces;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cashback.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository,
            IUserStatusRepository userStatusRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this._purchaseRepository = purchaseRepository;
            this._userStatusRepository = userStatusRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public bool PostPurchase(PurchaseDTO purchase)
        {
            Validator.ValidateObject(purchase, new ValidationContext(purchase), true);

            User _user = this._userRepository.Find(x => x.Cpf == purchase.Cpf);
            if (_user == null)
                throw new Exception("User not found");

            Purchase _purchase = _mapper.Map<Purchase>(purchase);

            this.SetStatusPurchase(_purchase);

            this._purchaseRepository.Create(_purchase);

            return true;
        }

        public void SetStatusPurchase(Purchase purchase)
        {
            UserStatus _userStatus = this._userStatusRepository.Find(x => x.Cpf == purchase.Cpf);

            if (_userStatus != null && PurchaseStatus.Approved.Equals(_userStatus.Status))
                purchase.Status = _userStatus.Status;
            else
                purchase.Status = 2;

        }

        public List<CashbackPurchaseDTO> GetPurchases(long cpf)
        {
            IEnumerable<Purchase> list = this._purchaseRepository.GetAll(cpf);
            List<CashbackPurchaseDTO> listCashback = _mapper.Map<List<CashbackPurchaseDTO>>(list);

            foreach (var purchase in listCashback)
            {
                this.CashbackCalculator(purchase);
                this.GetStatusPurchase(purchase);
            }

            return listCashback;
        }

        public void GetStatusPurchase(CashbackPurchaseDTO purchase)
        {
            purchase.DscStatus = ( purchase.Status == (int)PurchaseStatus.Approved ? "Approved"
                : (purchase.Status == (int)PurchaseStatus.UnderEvaluation ? "Under Evaluation" : "Canceled"));

        }

        public void CashbackCalculator(CashbackPurchaseDTO purchase)
        {
            purchase.CashbackPerc = purchase.Value <= 1000 ? "10%" : purchase.Value <= 1500 ? "15%" : "20";

            purchase.CashbackValue = purchase.Value * (purchase.Value <= 1000 ? (decimal)0.10
                : purchase.Value <= 1500 ? (decimal)0.15 : (decimal)0.20);
        }
    }
}
