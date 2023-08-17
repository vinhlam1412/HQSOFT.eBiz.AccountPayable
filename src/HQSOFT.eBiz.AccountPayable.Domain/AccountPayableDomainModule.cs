using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(AccountPayableDomainSharedModule)
)]
public class AccountPayableDomainModule : AbpModule
{

}
