using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using AutoMapper;

namespace HQSOFT.eBiz.AccountPayable.Blazor;

public class AccountPayableBlazorAutoMapperProfile : Profile
{
    public AccountPayableBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<VendorClassDto, VendorClassUpdateDto>();
        CreateMap<VendorClassDto, VendorClassCreateDto>();

        CreateMap<VendorClassAttributeDto, VendorClassAttributeUpdateDto>();
        CreateMap<VendorClassAttributeDto, VendorClassAttributeCreateDto>();

        CreateMap<VendorClassCompanyDto, VendorClassCompanyUpdateDto>();
        CreateMap<VendorClassCompanyDto, VendorClassCompanyCreateDto>();
    }
}