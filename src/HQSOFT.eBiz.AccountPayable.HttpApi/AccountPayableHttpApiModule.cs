using Localization.Resources.AbpUi;
using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace HQSOFT.eBiz.AccountPayable;

[DependsOn(
    typeof(AccountPayableApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AccountPayableHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AccountPayableHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AccountPayableResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
