using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AccountPayableApplicationModule),
    typeof(AccountPayableDomainTestModule)
    )]
public class AccountPayableApplicationTestModule : AbpModule
{

}
