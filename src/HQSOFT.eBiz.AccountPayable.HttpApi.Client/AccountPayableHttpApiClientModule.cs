using HQSOFT.eBiz.GeneralLedger;
using HQSOFT.SharedInformation;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AccountPayableApplicationContractsModule),
    typeof(AbpHttpClientModule),

    typeof(SharedInformationApplicationContractsModule),
    typeof(GeneralLedgerApplicationContractsModule)
    )]
public class AccountPayableHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AccountPayableApplicationContractsModule).Assembly,
            AccountPayableRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AccountPayableHttpApiClientModule>();
        });
    }
}
