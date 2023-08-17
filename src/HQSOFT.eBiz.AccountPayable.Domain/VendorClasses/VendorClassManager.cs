using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class VendorClassManager : DomainService
    {
        private readonly IVendorClassRepository _vendorClassRepository;

        public VendorClassManager(IVendorClassRepository vendorClassRepository)
        {
            _vendorClassRepository = vendorClassRepository;
        }

        public async Task<VendorClass> CreateAsync(
        string vendorClassCode, string description, Guid currency, Guid country, Guid paymentMethodCode, Guid cashAccount, Guid termId, string receiptAction, Guid aPAccount, Guid aPCostCenter, Guid expeneseAccount, Guid expeneseCostCenter, Guid discountAccount, Guid discountCostCenter, Guid cashDiscountAccount, Guid cashDiscountCostCenter, Guid prepaymentAccount, Guid prepaymentCostCenter, Guid reclassificationAccount, Guid reclassificationCostCenter, Guid pOAccrualAccount, Guid pOAccrualCostCenter, Guid unrealizedGaintAccount, Guid unrealizedGaintCostCenter, Guid unrealizedGaintLossAccount, Guid unrealizedGaintLossCostCenter, Guid retainagePayableAccount, Guid retainagePayableCostCenter)
        {
            Check.NotNullOrWhiteSpace(vendorClassCode, nameof(vendorClassCode));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vendorClass = new VendorClass(
             GuidGenerator.Create(),
             vendorClassCode, description, currency, country, paymentMethodCode, cashAccount, termId, receiptAction, aPAccount, aPCostCenter, expeneseAccount, expeneseCostCenter, discountAccount, discountCostCenter, cashDiscountAccount, cashDiscountCostCenter, prepaymentAccount, prepaymentCostCenter, reclassificationAccount, reclassificationCostCenter, pOAccrualAccount, pOAccrualCostCenter, unrealizedGaintAccount, unrealizedGaintCostCenter, unrealizedGaintLossAccount, unrealizedGaintLossCostCenter, retainagePayableAccount, retainagePayableCostCenter
             );

            return await _vendorClassRepository.InsertAsync(vendorClass);
        }

        public async Task<VendorClass> UpdateAsync(
            Guid id,
            string vendorClassCode, string description, Guid currency, Guid country, Guid paymentMethodCode, Guid cashAccount, Guid termId, string receiptAction, Guid aPAccount, Guid aPCostCenter, Guid expeneseAccount, Guid expeneseCostCenter, Guid discountAccount, Guid discountCostCenter, Guid cashDiscountAccount, Guid cashDiscountCostCenter, Guid prepaymentAccount, Guid prepaymentCostCenter, Guid reclassificationAccount, Guid reclassificationCostCenter, Guid pOAccrualAccount, Guid pOAccrualCostCenter, Guid unrealizedGaintAccount, Guid unrealizedGaintCostCenter, Guid unrealizedGaintLossAccount, Guid unrealizedGaintLossCostCenter, Guid retainagePayableAccount, Guid retainagePayableCostCenter, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(vendorClassCode, nameof(vendorClassCode));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vendorClass = await _vendorClassRepository.GetAsync(id);

            vendorClass.VendorClassCode = vendorClassCode;
            vendorClass.Description = description;
            vendorClass.Currency = currency;
            vendorClass.Country = country;
            vendorClass.PaymentMethodCode = paymentMethodCode;
            vendorClass.CashAccount = cashAccount;
            vendorClass.TermId = termId;
            vendorClass.ReceiptAction = receiptAction;
            vendorClass.APAccount = aPAccount;
            vendorClass.APCostCenter = aPCostCenter;
            vendorClass.ExpeneseAccount = expeneseAccount;
            vendorClass.ExpeneseCostCenter = expeneseCostCenter;
            vendorClass.DiscountAccount = discountAccount;
            vendorClass.DiscountCostCenter = discountCostCenter;
            vendorClass.CashDiscountAccount = cashDiscountAccount;
            vendorClass.CashDiscountCostCenter = cashDiscountCostCenter;
            vendorClass.PrepaymentAccount = prepaymentAccount;
            vendorClass.PrepaymentCostCenter = prepaymentCostCenter;
            vendorClass.ReclassificationAccount = reclassificationAccount;
            vendorClass.ReclassificationCostCenter = reclassificationCostCenter;
            vendorClass.POAccrualAccount = pOAccrualAccount;
            vendorClass.POAccrualCostCenter = pOAccrualCostCenter;
            vendorClass.UnrealizedGaintAccount = unrealizedGaintAccount;
            vendorClass.UnrealizedGaintCostCenter = unrealizedGaintCostCenter;
            vendorClass.UnrealizedGaintLossAccount = unrealizedGaintLossAccount;
            vendorClass.UnrealizedGaintLossCostCenter = unrealizedGaintLossCostCenter;
            vendorClass.RetainagePayableAccount = retainagePayableAccount;
            vendorClass.RetainagePayableCostCenter = retainagePayableCostCenter;

            vendorClass.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vendorClassRepository.UpdateAsync(vendorClass);
        }

    }
}