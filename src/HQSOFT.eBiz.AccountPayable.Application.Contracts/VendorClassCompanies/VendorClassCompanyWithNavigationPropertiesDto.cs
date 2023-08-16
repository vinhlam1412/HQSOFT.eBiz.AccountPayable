using HQSOFT.eBiz.AccountPayable.VendorClasses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyWithNavigationPropertiesDto
    {
        public VendorClassCompanyDto VendorClassCompany { get; set; }

        public VendorClassDto VendorClass { get; set; }

    }
}