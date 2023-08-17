using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable.Blazor.WebAssembly;

[DependsOn(
    typeof(AccountPayableBlazorModule),
    typeof(AccountPayableHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class AccountPayableBlazorWebAssemblyModule : AbpModule
{

}
