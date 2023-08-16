using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.AccountPayable.Seed;

public class AccountPayableAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly AccountPayableSampleIdentityDataSeeder _accountPayableSampleIdentityDataSeeder;
    private readonly AccountPayableAuthServerDataSeeder _accountPayableAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public AccountPayableAuthServerDataSeedContributor(
        AccountPayableAuthServerDataSeeder accountPayableAuthServerDataSeeder,
        AccountPayableSampleIdentityDataSeeder accountPayableSampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _accountPayableAuthServerDataSeeder = accountPayableAuthServerDataSeeder;
        _accountPayableSampleIdentityDataSeeder = accountPayableSampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _accountPayableSampleIdentityDataSeeder.SeedAsync(context!);
            await _accountPayableAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
