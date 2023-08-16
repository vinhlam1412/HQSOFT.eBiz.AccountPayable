using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class VendorClass : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string VendorClassCode { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual Guid Currency { get; set; }

        public virtual Guid Country { get; set; }

        public virtual Guid PaymentMethodCode { get; set; }

        public virtual Guid CashAccount { get; set; }

        public virtual Guid TermId { get; set; }

        [CanBeNull]
        public virtual string? ReceiptAction { get; set; }

        public virtual Guid APAccount { get; set; }

        public virtual Guid APCostCenter { get; set; }

        public virtual Guid ExpeneseAccount { get; set; }

        public virtual Guid ExpeneseCostCenter { get; set; }

        public virtual Guid DiscountAccount { get; set; }

        public virtual Guid DiscountCostCenter { get; set; }

        public virtual Guid CashDiscountAccount { get; set; }

        public virtual Guid CashDiscountCostCenter { get; set; }

        public virtual Guid PrepaymentAccount { get; set; }

        public virtual Guid PrepaymentCostCenter { get; set; }

        public virtual Guid ReclassificationAccount { get; set; }

        public virtual Guid ReclassificationCostCenter { get; set; }

        public virtual Guid POAccrualAccount { get; set; }

        public virtual Guid POAccrualCostCenter { get; set; }

        public virtual Guid UnrealizedGaintAccount { get; set; }

        public virtual Guid UnrealizedGaintCostCenter { get; set; }

        public virtual Guid UnrealizedGaintLossAccount { get; set; }

        public virtual Guid UnrealizedGaintLossCostCenter { get; set; }

        public virtual Guid RetainagePayableAccount { get; set; }

        public virtual Guid RetainagePayableCostCenter { get; set; }

        public VendorClass()
        {

        }

        public VendorClass(Guid id, string vendorClassCode, string description, Guid currency, Guid country, Guid paymentMethodCode, Guid cashAccount, Guid termId, string receiptAction, Guid aPAccount, Guid aPCostCenter, Guid expeneseAccount, Guid expeneseCostCenter, Guid discountAccount, Guid discountCostCenter, Guid cashDiscountAccount, Guid cashDiscountCostCenter, Guid prepaymentAccount, Guid prepaymentCostCenter, Guid reclassificationAccount, Guid reclassificationCostCenter, Guid pOAccrualAccount, Guid pOAccrualCostCenter, Guid unrealizedGaintAccount, Guid unrealizedGaintCostCenter, Guid unrealizedGaintLossAccount, Guid unrealizedGaintLossCostCenter, Guid retainagePayableAccount, Guid retainagePayableCostCenter)
        {

            Id = id;
            Check.NotNull(vendorClassCode, nameof(vendorClassCode));
            Check.NotNull(description, nameof(description));
            VendorClassCode = vendorClassCode;
            Description = description;
            Currency = currency;
            Country = country;
            PaymentMethodCode = paymentMethodCode;
            CashAccount = cashAccount;
            TermId = termId;
            ReceiptAction = receiptAction;
            APAccount = aPAccount;
            APCostCenter = aPCostCenter;
            ExpeneseAccount = expeneseAccount;
            ExpeneseCostCenter = expeneseCostCenter;
            DiscountAccount = discountAccount;
            DiscountCostCenter = discountCostCenter;
            CashDiscountAccount = cashDiscountAccount;
            CashDiscountCostCenter = cashDiscountCostCenter;
            PrepaymentAccount = prepaymentAccount;
            PrepaymentCostCenter = prepaymentCostCenter;
            ReclassificationAccount = reclassificationAccount;
            ReclassificationCostCenter = reclassificationCostCenter;
            POAccrualAccount = pOAccrualAccount;
            POAccrualCostCenter = pOAccrualCostCenter;
            UnrealizedGaintAccount = unrealizedGaintAccount;
            UnrealizedGaintCostCenter = unrealizedGaintCostCenter;
            UnrealizedGaintLossAccount = unrealizedGaintLossAccount;
            UnrealizedGaintLossCostCenter = unrealizedGaintLossCostCenter;
            RetainagePayableAccount = retainagePayableAccount;
            RetainagePayableCostCenter = retainagePayableCostCenter;
        }

    }
}