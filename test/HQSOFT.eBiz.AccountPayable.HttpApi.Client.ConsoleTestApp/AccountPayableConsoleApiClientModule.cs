using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AccountPayableHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AccountPayableConsoleApiClientModule : AbpModule
{

}
