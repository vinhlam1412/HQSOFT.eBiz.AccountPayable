using HQSOFT.eBiz.AccountPayable.VendorClasses;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttribute : AuditedAggregateRoot<Guid>
    {
        public virtual bool IsActive { get; set; }

        public virtual Guid IdAttribute { get; set; }

        public virtual int SortOrder { get; set; }

        public virtual bool IsRequired { get; set; }

        public virtual bool IsInternal { get; set; }

        [CanBeNull]
        public virtual string? ControlType { get; set; }

        [CanBeNull]
        public virtual string? DefaultValue { get; set; }

        public virtual int Idx { get; set; }
        public Guid? VendorClassId { get; set; }

        public VendorClassAttribute()
        {

        }

        public VendorClassAttribute(Guid id, Guid? vendorClassId, bool isActive, Guid idAttribute, int sortOrder, bool isRequired, bool isInternal, string controlType, string defaultValue, int idx)
        {

            Id = id;
            IsActive = isActive;
            IdAttribute = idAttribute;
            SortOrder = sortOrder;
            IsRequired = isRequired;
            IsInternal = isInternal;
            ControlType = controlType;
            DefaultValue = defaultValue;
            Idx = idx;
            VendorClassId = vendorClassId;
        }

    }
}