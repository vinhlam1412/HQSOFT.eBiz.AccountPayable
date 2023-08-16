using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.AccountPayable.Seed;

public class AccountPayableHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly AccountPayableSampleDataSeeder _accountPayableSampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public AccountPayableHttpApiHostDataSeedContributor(
        AccountPayableSampleDataSeeder accountPayableSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _accountPayableSampleDataSeeder = accountPayableSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _accountPayableSampleDataSeeder.SeedAsync(context!);
        }
    }
}
