using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public interface IVendorClassRepository : IRepository<VendorClass, Guid>
    {
        Task<List<VendorClass>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}