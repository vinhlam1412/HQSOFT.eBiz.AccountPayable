using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HQSOFT.eBiz.AccountPayable.Blazor;

public abstract class AccountPayableComponentBase : AbpComponentBase
{
    protected AccountPayableComponentBase()
    {
        LocalizationResource = typeof(AccountPayableResource);
    }
}
