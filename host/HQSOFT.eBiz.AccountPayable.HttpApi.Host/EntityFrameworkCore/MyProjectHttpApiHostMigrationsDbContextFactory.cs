using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

public class AccountPayableHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AccountPayableHttpApiHostMigrationsDbContext>
{
    public AccountPayableHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AccountPayableHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("AccountPayable"));

        return new AccountPayableHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
