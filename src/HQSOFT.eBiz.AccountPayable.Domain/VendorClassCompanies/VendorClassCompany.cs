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

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompany : AuditedAggregateRoot<Guid>
    {
        public virtual Guid CompanyId { get; set; }

        public virtual bool Exclude { get; set; }

        public virtual int Idx { get; set; }
        public Guid? VendorClassId { get; set; }

        public VendorClassCompany()
        {

        }

        public VendorClassCompany(Guid id, Guid? vendorClassId, Guid companyId, bool exclude, int idx)
        {

            Id = id;
            CompanyId = companyId;
            Exclude = exclude;
            Idx = idx;
            VendorClassId = vendorClassId;
        }

    }
}