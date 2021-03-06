//-----------------------------------------------------------------------
// <copyright file="AdministrationService.cs" company="AccountGo">
// Copyright (c) AccountGo. All rights reserved.
// <author>Marvin Perez</author>
// <date>1/11/2015 9:48:38 AM</date>
// </copyright>
//-----------------------------------------------------------------------

using System.Linq;
using System.Collections.Generic;
using Core.Data;
using Core.Domain;
using Core.Domain.Financials;
using System;
using Core.Domain.TaxSystem;

namespace Services.Administration
{
    public class AdministrationService : BaseService, IAdministrationService
    {
        private readonly IRepository<FinancialYear> _fiscalYearRepo;
        private readonly IRepository<TaxGroup> _taxGroupRepo;
        private readonly IRepository<ItemTaxGroup> _itemTaxGroupRepo;
        private readonly IRepository<PaymentTerm> _paymentTermRepo;
        private readonly IRepository<Bank> _bankRepo;
        private readonly IRepository<GeneralLedgerSetting> _genetalLedgerSetting;
        private readonly IRepository<Tax> _taxRepo;
        private readonly IRepository<Company> _company;

        public AdministrationService(IRepository<FinancialYear> fiscalYearRepo,
            IRepository<TaxGroup> taxGroupRepo,
            IRepository<ItemTaxGroup> itemTaxGroupRepo,
            IRepository<PaymentTerm> paymentTermRepo,
            IRepository<Bank> bankRepo,
            IRepository<Tax> taxRepo,
            IRepository<GeneralLedgerSetting> generalLedgerSetting,
            IRepository<Company> company = null)
            : base(null, generalLedgerSetting, paymentTermRepo, bankRepo)
        {
            _fiscalYearRepo = fiscalYearRepo;
            _taxGroupRepo = taxGroupRepo;
            _itemTaxGroupRepo = itemTaxGroupRepo;
            _paymentTermRepo = paymentTermRepo;
            _bankRepo = bankRepo;
            _genetalLedgerSetting = generalLedgerSetting;
            _taxRepo = taxRepo;
            _company = company;
        }

        public ICollection<Tax> GetAllTaxes(bool includeInActive)
        {
            var query = from f in _taxRepo.Table
                        select f;
            return query.ToList();
        }

        public void AddNewTax(Tax tax)
        {
            _taxRepo.Insert(tax);
        }

        public void UpdateTax(Tax tax)
        {
            _taxRepo.Update(tax);
        }

        public void DeleteTax(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ItemTaxGroup> GetItemTaxGroups(bool includeInActive)
        {
            var query = from f in _itemTaxGroupRepo.Table
                        select f;
            return query.ToList();
        }

        public ICollection<TaxGroup> GetTaxGroups(bool includeInActive)
        {
            var query = from f in _taxGroupRepo.Table
                        select f;
            return query.ToList();
        }

        public void InitializeCompany()
        {
            if (_company.Table.FirstOrDefault() == null)
            {
                //DbInitializerHelper.Initialize();
            }
        }

        public Company GetDefaultCompany()
        {
            return _company.Table.ToList().FirstOrDefault();
        }

        public ICollection<PaymentTerm> GetPaymentTerms()
        {
            var query = from f in _paymentTermRepo.Table
                        select f;
            return query.ToList();
        }

        public ICollection<FinancialYear> GetFinancialYears()
        {
            var query = from f in _fiscalYearRepo.Table
                        select f;
            return query.ToList();
        }

        public void SaveCompany(Company company)
        {
            if (company.Id == 0) { _company.Insert(company); }
            else { _company.Update(company); }
        }
    }
}
