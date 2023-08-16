using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

public static class AccountPayableDbContextModelCreatingExtensions
{
    public static void ConfigureAccountPayable(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(AccountPayableDbProperties.DbTablePrefix + "Questions", AccountPayableDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VendorClass>(b =>
{
    b.ToTable(AccountPayableDbProperties.DbTablePrefix + "VendorClasses", AccountPayableDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.VendorClassCode).HasColumnName(nameof(VendorClass.VendorClassCode)).IsRequired();
    b.Property(x => x.Description).HasColumnName(nameof(VendorClass.Description)).IsRequired();
    b.Property(x => x.Currency).HasColumnName(nameof(VendorClass.Currency));
    b.Property(x => x.Country).HasColumnName(nameof(VendorClass.Country));
    b.Property(x => x.PaymentMethodCode).HasColumnName(nameof(VendorClass.PaymentMethodCode));
    b.Property(x => x.CashAccount).HasColumnName(nameof(VendorClass.CashAccount));
    b.Property(x => x.TermId).HasColumnName(nameof(VendorClass.TermId));
    b.Property(x => x.ReceiptAction).HasColumnName(nameof(VendorClass.ReceiptAction));
    b.Property(x => x.APAccount).HasColumnName(nameof(VendorClass.APAccount));
    b.Property(x => x.APCostCenter).HasColumnName(nameof(VendorClass.APCostCenter));
    b.Property(x => x.ExpeneseAccount).HasColumnName(nameof(VendorClass.ExpeneseAccount));
    b.Property(x => x.ExpeneseCostCenter).HasColumnName(nameof(VendorClass.ExpeneseCostCenter));
    b.Property(x => x.DiscountAccount).HasColumnName(nameof(VendorClass.DiscountAccount));
    b.Property(x => x.DiscountCostCenter).HasColumnName(nameof(VendorClass.DiscountCostCenter));
    b.Property(x => x.CashDiscountAccount).HasColumnName(nameof(VendorClass.CashDiscountAccount));
    b.Property(x => x.CashDiscountCostCenter).HasColumnName(nameof(VendorClass.CashDiscountCostCenter));
    b.Property(x => x.PrepaymentAccount).HasColumnName(nameof(VendorClass.PrepaymentAccount));
    b.Property(x => x.PrepaymentCostCenter).HasColumnName(nameof(VendorClass.PrepaymentCostCenter));
    b.Property(x => x.ReclassificationAccount).HasColumnName(nameof(VendorClass.ReclassificationAccount));
    b.Property(x => x.ReclassificationCostCenter).HasColumnName(nameof(VendorClass.ReclassificationCostCenter));
    b.Property(x => x.POAccrualAccount).HasColumnName(nameof(VendorClass.POAccrualAccount));
    b.Property(x => x.POAccrualCostCenter).HasColumnName(nameof(VendorClass.POAccrualCostCenter));
    b.Property(x => x.UnrealizedGaintAccount).HasColumnName(nameof(VendorClass.UnrealizedGaintAccount));
    b.Property(x => x.UnrealizedGaintCostCenter).HasColumnName(nameof(VendorClass.UnrealizedGaintCostCenter));
    b.Property(x => x.UnrealizedGaintLossAccount).HasColumnName(nameof(VendorClass.UnrealizedGaintLossAccount));
    b.Property(x => x.UnrealizedGaintLossCostCenter).HasColumnName(nameof(VendorClass.UnrealizedGaintLossCostCenter));
    b.Property(x => x.RetainagePayableAccount).HasColumnName(nameof(VendorClass.RetainagePayableAccount));
    b.Property(x => x.RetainagePayableCostCenter).HasColumnName(nameof(VendorClass.RetainagePayableCostCenter));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VendorClassAttribute>(b =>
{
    b.ToTable(AccountPayableDbProperties.DbTablePrefix + "VendorClassAttributes", AccountPayableDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.IsActive).HasColumnName(nameof(VendorClassAttribute.IsActive));
    b.Property(x => x.IdAttribute).HasColumnName(nameof(VendorClassAttribute.IdAttribute));
    b.Property(x => x.SortOrder).HasColumnName(nameof(VendorClassAttribute.SortOrder));
    b.Property(x => x.IsRequired).HasColumnName(nameof(VendorClassAttribute.IsRequired));
    b.Property(x => x.IsInternal).HasColumnName(nameof(VendorClassAttribute.IsInternal));
    b.Property(x => x.ControlType).HasColumnName(nameof(VendorClassAttribute.ControlType));
    b.Property(x => x.DefaultValue).HasColumnName(nameof(VendorClassAttribute.DefaultValue));
    b.Property(x => x.Idx).HasColumnName(nameof(VendorClassAttribute.Idx));
    b.HasOne<VendorClass>().WithMany().HasForeignKey(x => x.VendorClassId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VendorClassCompany>(b =>
{
    b.ToTable(AccountPayableDbProperties.DbTablePrefix + "VendorClassCompanies", AccountPayableDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CompanyId).HasColumnName(nameof(VendorClassCompany.CompanyId));
    b.Property(x => x.Exclude).HasColumnName(nameof(VendorClassCompany.Exclude));
    b.Property(x => x.Idx).HasColumnName(nameof(VendorClassCompany.Idx));
    b.HasOne<VendorClass>().WithMany().HasForeignKey(x => x.VendorClassId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}