using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HQSOFT.eBiz.AccountPayable;

public abstract class AccountPayableController : AbpControllerBase
{
    protected AccountPayableController()
    {
        LocalizationResource = typeof(AccountPayableResource);
    }
}
