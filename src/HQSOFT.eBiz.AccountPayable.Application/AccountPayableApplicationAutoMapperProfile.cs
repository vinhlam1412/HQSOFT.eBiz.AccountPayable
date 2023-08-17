using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using System;
using HQSOFT.eBiz.AccountPayable.Shared;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using AutoMapper;

namespace HQSOFT.eBiz.AccountPayable;

public class AccountPayableApplicationAutoMapperProfile : Profile
{
    public AccountPayableApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<VendorClass, VendorClassDto>()
            .Ignore(x => x.IsChanged);
        CreateMap<VendorClass, VendorClassExcelDto>();

        CreateMap<VendorClassAttribute, VendorClassAttributeDto>()
            .Ignore(x => x.IsChanged);
        CreateMap<VendorClassAttribute, VendorClassAttributeExcelDto>();
        CreateMap<VendorClassAttributeWithNavigationProperties, VendorClassAttributeWithNavigationPropertiesDto>();
        CreateMap<VendorClass, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.VendorClassCode));

        CreateMap<VendorClassCompany, VendorClassCompanyDto>()
            .Ignore(x => x.IsChanged);
        CreateMap<VendorClassCompany, VendorClassCompanyExcelDto>();
        CreateMap<VendorClassCompanyWithNavigationProperties, VendorClassCompanyWithNavigationPropertiesDto>();
    }
}