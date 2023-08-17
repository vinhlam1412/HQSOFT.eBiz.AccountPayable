using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid CompanyId { get; set; }
        public bool Exclude { get; set; }
        public int Idx { get; set; }
        public Guid? VendorClassId { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsChanged { get; set; }
    }
}