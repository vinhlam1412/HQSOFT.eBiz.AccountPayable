using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

public class AccountPayableHttpApiHostMigrationsDbContext : AbpDbContext<AccountPayableHttpApiHostMigrationsDbContext>
{
    public AccountPayableHttpApiHostMigrationsDbContext(DbContextOptions<AccountPayableHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAccountPayable();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
    }
}
