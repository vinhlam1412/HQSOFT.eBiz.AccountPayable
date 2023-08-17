using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

[ConnectionStringName(AccountPayableDbProperties.ConnectionStringName)]
public class AccountPayableDbContext : AbpDbContext<AccountPayableDbContext>, IAccountPayableDbContext
{
    public DbSet<VendorClassCompany> VendorClassCompanies { get; set; }
    public DbSet<VendorClassAttribute> VendorClassAttributes { get; set; }
    public DbSet<VendorClass> VendorClasses { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public AccountPayableDbContext(DbContextOptions<AccountPayableDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAccountPayable();
    }
}