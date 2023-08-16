using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

[DependsOn(
    typeof(AccountPayableDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AccountPayableEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AccountPayableDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<VendorClass, VendorClasses.EfCoreVendorClassRepository>();

            options.AddRepository<VendorClassAttribute, VendorClassAttributes.EfCoreVendorClassAttributeRepository>();

            options.AddRepository<VendorClassCompany, VendorClassCompanies.EfCoreVendorClassCompanyRepository>();

        });
    }
}