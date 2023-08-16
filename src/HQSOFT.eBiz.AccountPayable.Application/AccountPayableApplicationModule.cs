using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AccountPayableDomainModule),
    typeof(AccountPayableApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class AccountPayableApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AccountPayableApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AccountPayableApplicationModule>(validate: true);
        });
    }
}
