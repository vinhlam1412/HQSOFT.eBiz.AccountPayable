using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

[ConnectionStringName(AccountPayableDbProperties.ConnectionStringName)]
public interface IAccountPayableDbContext : IEfCoreDbContext
{
    DbSet<VendorClassCompany> VendorClassCompanies { get; set; }
    DbSet<VendorClassAttribute> VendorClassAttributes { get; set; }
    DbSet<VendorClass> VendorClasses { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}