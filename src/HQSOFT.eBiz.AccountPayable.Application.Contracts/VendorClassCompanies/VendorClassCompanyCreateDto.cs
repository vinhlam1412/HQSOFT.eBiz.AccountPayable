using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyCreateDto
    {
        public Guid CompanyId { get; set; }
        public bool Exclude { get; set; }
        public int Idx { get; set; }
        public Guid? VendorClassId { get; set; }
    }
}