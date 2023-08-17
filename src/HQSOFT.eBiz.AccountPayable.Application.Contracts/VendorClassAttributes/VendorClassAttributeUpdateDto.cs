using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeUpdateDto : IHasConcurrencyStamp
    {
        public bool IsActive { get; set; }
        public Guid IdAttribute { get; set; }
        public int SortOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }
        public string? ControlType { get; set; }
        public string? DefaultValue { get; set; }
        public int Idx { get; set; }
        public Guid? VendorClassId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}