using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyUpdateDto : IHasConcurrencyStamp
    {
        public Guid CompanyId { get; set; }
        public bool Exclude { get; set; }
        public int Idx { get; set; }
        public Guid? VendorClassId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}