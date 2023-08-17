using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.Application.Services;

namespace HQSOFT.eBiz.AccountPayable;

public abstract class AccountPayableAppService : ApplicationService
{
    protected AccountPayableAppService()
    {
        LocalizationResource = typeof(AccountPayableResource);
        ObjectMapperContext = typeof(AccountPayableApplicationModule);
    }
}
