using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class EfCoreVendorClassRepository : EfCoreRepository<AccountPayableDbContext, VendorClass, Guid>, IVendorClassRepository
    {
        public EfCoreVendorClassRepository(IDbContextProvider<AccountPayableDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<VendorClass>> GetListAsync(
            string filterText = null,
            string vendorClassCode = null,
            string description = null,
            Guid? currency = null,
            Guid? country = null,
            Guid? paymentMethodCode = null,
            Guid? cashAccount = null,
            Guid? termId = null,
            string receiptAction = null,
            Guid? aPAccount = null,
            Guid? aPCostCenter = null,
            Guid? expeneseAccount = null,
            Guid? expeneseCostCenter = null,
            Guid? discountAccount = null,
            Guid? discountCostCenter = null,
            Guid? cashDiscountAccount = null,
            Guid? cashDiscountCostCenter = null,
            Guid? prepaymentAccount = null,
            Guid? prepaymentCostCenter = null,
            Guid? reclassificationAccount = null,
            Guid? reclassificationCostCenter = null,
            Guid? pOAccrualAccount = null,
            Guid? pOAccrualCostCenter = null,
            Guid? unrealizedGaintAccount = null,
            Guid? unrealizedGaintCostCenter = null,
            Guid? unrealizedGaintLossAccount = null,
            Guid? unrealizedGaintLossCostCenter = null,
            Guid? retainagePayableAccount = null,
            Guid? retainagePayableCostCenter = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, vendorClassCode, description, currency, country, paymentMethodCode, cashAccount, termId, receiptAction, aPAccount, aPCostCenter, expeneseAccount, expeneseCostCenter, discountAccount, discountCostCenter, cashDiscountAccount, cashDiscountCostCenter, prepaymentAccount, prepaymentCostCenter, reclassificationAccount, reclassificationCostCenter, pOAccrualAccount, pOAccrualCostCenter, unrealizedGaintAccount, unrealizedGaintCostCenter, unrealizedGaintLossAccount, unrealizedGaintLossCostCenter, retainagePayableAccount, retainagePayableCostCenter);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string vendorClassCode = null,
            string description = null,
            Guid? currency = null,
            Guid? country = null,
            Guid? paymentMethodCode = null,
            Guid? cashAccount = null,
            Guid? termId = null,
            string receiptAction = null,
            Guid? aPAccount = null,
            Guid? aPCostCenter = null,
            Guid? expeneseAccount = null,
            Guid? expeneseCostCenter = null,
            Guid? discountAccount = null,
            Guid? discountCostCenter = null,
            Guid? cashDiscountAccount = null,
            Guid? cashDiscountCostCenter = null,
            Guid? prepaymentAccount = null,
            Guid? prepaymentCostCenter = null,
            Guid? reclassificationAccount = null,
            Guid? reclassificationCostCenter = null,
            Guid? pOAccrualAccount = null,
            Guid? pOAccrualCostCenter = null,
            Guid? unrealizedGaintAccount = null,
            Guid? unrealizedGaintCostCenter = null,
            Guid? unrealizedGaintLossAccount = null,
            Guid? unrealizedGaintLossCostCenter = null,
            Guid? retainagePayableAccount = null,
            Guid? retainagePayableCostCenter = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, vendorClassCode, description, currency, country, paymentMethodCode, cashAccount, termId, receiptAction, aPAccount, aPCostCenter, expeneseAccount, expeneseCostCenter, discountAccount, discountCostCenter, cashDiscountAccount, cashDiscountCostCenter, prepaymentAccount, prepaymentCostCenter, reclassificationAccount, reclassificationCostCenter, pOAccrualAccount, pOAccrualCostCenter, unrealizedGaintAccount, unrealizedGaintCostCenter, unrealizedGaintLossAccount, unrealizedGaintLossCostCenter, retainagePayableAccount, retainagePayableCostCenter);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VendorClass> ApplyFilter(
            IQueryable<VendorClass> query,
            string filterText,
            string vendorClassCode = null,
            string description = null,
            Guid? currency = null,
            Guid? country = null,
            Guid? paymentMethodCode = null,
            Guid? cashAccount = null,
            Guid? termId = null,
            string receiptAction = null,
            Guid? aPAccount = null,
            Guid? aPCostCenter = null,
            Guid? expeneseAccount = null,
            Guid? expeneseCostCenter = null,
            Guid? discountAccount = null,
            Guid? discountCostCenter = null,
            Guid? cashDiscountAccount = null,
            Guid? cashDiscountCostCenter = null,
            Guid? prepaymentAccount = null,
            Guid? prepaymentCostCenter = null,
            Guid? reclassificationAccount = null,
            Guid? reclassificationCostCenter = null,
            Guid? pOAccrualAccount = null,
            Guid? pOAccrualCostCenter = null,
            Guid? unrealizedGaintAccount = null,
            Guid? unrealizedGaintCostCenter = null,
            Guid? unrealizedGaintLossAccount = null,
            Guid? unrealizedGaintLossCostCenter = null,
            Guid? retainagePayableAccount = null,
            Guid? retainagePayableCostCenter = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VendorClassCode.ToLower().Contains(filterText.ToLower()) || e.Description.ToLower().Contains(filterText.ToLower()) || e.ReceiptAction.ToLower().Contains(filterText.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(vendorClassCode), e => e.VendorClassCode.ToLower().Contains(vendorClassCode.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.ToLower().Contains(description.ToLower()))
                    .WhereIf(currency.HasValue, e => e.Currency == currency)
                    .WhereIf(country.HasValue, e => e.Country == country)
                    .WhereIf(paymentMethodCode.HasValue, e => e.PaymentMethodCode == paymentMethodCode)
                    .WhereIf(cashAccount.HasValue, e => e.CashAccount == cashAccount)
                    .WhereIf(termId.HasValue, e => e.TermId == termId)
                    .WhereIf(!string.IsNullOrWhiteSpace(receiptAction), e => e.ReceiptAction.ToLower().Contains(receiptAction.ToLower()))
                    .WhereIf(aPAccount.HasValue, e => e.APAccount == aPAccount)
                    .WhereIf(aPCostCenter.HasValue, e => e.APCostCenter == aPCostCenter)
                    .WhereIf(expeneseAccount.HasValue, e => e.ExpeneseAccount == expeneseAccount)
                    .WhereIf(expeneseCostCenter.HasValue, e => e.ExpeneseCostCenter == expeneseCostCenter)
                    .WhereIf(discountAccount.HasValue, e => e.DiscountAccount == discountAccount)
                    .WhereIf(discountCostCenter.HasValue, e => e.DiscountCostCenter == discountCostCenter)
                    .WhereIf(cashDiscountAccount.HasValue, e => e.CashDiscountAccount == cashDiscountAccount)
                    .WhereIf(cashDiscountCostCenter.HasValue, e => e.CashDiscountCostCenter == cashDiscountCostCenter)
                    .WhereIf(prepaymentAccount.HasValue, e => e.PrepaymentAccount == prepaymentAccount)
                    .WhereIf(prepaymentCostCenter.HasValue, e => e.PrepaymentCostCenter == prepaymentCostCenter)
                    .WhereIf(reclassificationAccount.HasValue, e => e.ReclassificationAccount == reclassificationAccount)
                    .WhereIf(reclassificationCostCenter.HasValue, e => e.ReclassificationCostCenter == reclassificationCostCenter)
                    .WhereIf(pOAccrualAccount.HasValue, e => e.POAccrualAccount == pOAccrualAccount)
                    .WhereIf(pOAccrualCostCenter.HasValue, e => e.POAccrualCostCenter == pOAccrualCostCenter)
                    .WhereIf(unrealizedGaintAccount.HasValue, e => e.UnrealizedGaintAccount == unrealizedGaintAccount)
                    .WhereIf(unrealizedGaintCostCenter.HasValue, e => e.UnrealizedGaintCostCenter == unrealizedGaintCostCenter)
                    .WhereIf(unrealizedGaintLossAccount.HasValue, e => e.UnrealizedGaintLossAccount == unrealizedGaintLossAccount)
                    .WhereIf(unrealizedGaintLossCostCenter.HasValue, e => e.UnrealizedGaintLossCostCenter == unrealizedGaintLossCostCenter)
                    .WhereIf(retainagePayableAccount.HasValue, e => e.RetainagePayableAccount == retainagePayableAccount)
                    .WhereIf(retainagePayableCostCenter.HasValue, e => e.RetainagePayableCostCenter == retainagePayableCostCenter);
        }
    }
}