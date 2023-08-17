using HQSOFT.eBiz.AccountPayable.VendorClasses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeWithNavigationPropertiesDto
    {
        public VendorClassAttributeDto VendorClassAttribute { get; set; }

        public VendorClassDto VendorClass { get; set; }

    }
}