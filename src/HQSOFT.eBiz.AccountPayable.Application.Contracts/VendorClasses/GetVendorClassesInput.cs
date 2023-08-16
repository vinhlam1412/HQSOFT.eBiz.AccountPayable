using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class GetVendorClassesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? VendorClassCode { get; set; }
        public string? Description { get; set; }
        public Guid? Currency { get; set; }
        public Guid? Country { get; set; }
        public Guid? PaymentMethodCode { get; set; }
        public Guid? CashAccount { get; set; }
        public Guid? TermId { get; set; }
        public string? ReceiptAction { get; set; }
        public Guid? APAccount { get; set; }
        public Guid? APCostCenter { get; set; }
        public Guid? ExpeneseAccount { get; set; }
        public Guid? ExpeneseCostCenter { get; set; }
        public Guid? DiscountAccount { get; set; }
        public Guid? DiscountCostCenter { get; set; }
        public Guid? CashDiscountAccount { get; set; }
        public Guid? CashDiscountCostCenter { get; set; }
        public Guid? PrepaymentAccount { get; set; }
        public Guid? PrepaymentCostCenter { get; set; }
        public Guid? ReclassificationAccount { get; set; }
        public Guid? ReclassificationCostCenter { get; set; }
        public Guid? POAccrualAccount { get; set; }
        public Guid? POAccrualCostCenter { get; set; }
        public Guid? UnrealizedGaintAccount { get; set; }
        public Guid? UnrealizedGaintCostCenter { get; set; }
        public Guid? UnrealizedGaintLossAccount { get; set; }
        public Guid? UnrealizedGaintLossCostCenter { get; set; }
        public Guid? RetainagePayableAccount { get; set; }
        public Guid? RetainagePayableCostCenter { get; set; }

        public GetVendorClassesInput()
        {

        }
    }
}