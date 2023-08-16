using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool IsActive { get; set; }
        public string? IdAttribute { get; set; }
        public int SortOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }
        public string? ControlType { get; set; }
        public string? DefaultValue { get; set; }
        public int Idx { get; set; }
        public Guid? VendorClassId { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsChanged { get; set; }
    }
}