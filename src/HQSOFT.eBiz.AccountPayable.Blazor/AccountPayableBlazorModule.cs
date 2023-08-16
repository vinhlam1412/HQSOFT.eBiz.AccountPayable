using Microsoft.Extensions.DependencyInjection;
using HQSOFT.eBiz.AccountPayable.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using HQSOFT.SharedInformation;
using HQSOFT.eBiz.GeneralLedger;

namespace HQSOFT.eBiz.AccountPayable.Blazor;

[DependsOn(
    typeof(AccountPayableApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule),

    typeof(SharedInformationApplicationContractsModule),
     typeof(GeneralLedgerApplicationContractsModule)
    )]
public class AccountPayableBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AccountPayableBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AccountPayableBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new AccountPayableMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AccountPayableBlazorModule).Assembly);
        });
        context.Services.AddDevExpressBlazor(options => {
            options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
            options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
        });
        context.Services.AddDevExpressBlazor();

        context.Services.AddHttpClientProxies(
        typeof(SharedInformationApplicationContractsModule).Assembly,
        remoteServiceConfigurationName: "SharedInformation");

        context.Services.AddHttpClientProxies(
         typeof(GeneralLedgerApplicationContractsModule).Assembly,
         remoteServiceConfigurationName: "GeneralLedger");

        
    }
}
