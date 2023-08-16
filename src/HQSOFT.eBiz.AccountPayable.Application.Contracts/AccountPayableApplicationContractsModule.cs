using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AccountPayableDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class AccountPayableApplicationContractsModule : AbpModule
{

}
