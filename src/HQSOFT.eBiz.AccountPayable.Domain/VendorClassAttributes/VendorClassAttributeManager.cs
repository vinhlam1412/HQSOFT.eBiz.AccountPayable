using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeManager : DomainService
    {
        private readonly IVendorClassAttributeRepository _vendorClassAttributeRepository;

        public VendorClassAttributeManager(IVendorClassAttributeRepository vendorClassAttributeRepository)
        {
            _vendorClassAttributeRepository = vendorClassAttributeRepository;
        }

        public async Task<VendorClassAttribute> CreateAsync(
        Guid? vendorClassId, bool isActive, Guid idAttribute, int sortOrder, bool isRequired, bool isInternal, string controlType, string defaultValue, int idx)
        {

            var vendorClassAttribute = new VendorClassAttribute(
             GuidGenerator.Create(),
             vendorClassId, isActive, idAttribute, sortOrder, isRequired, isInternal, controlType, defaultValue, idx
             );

            return await _vendorClassAttributeRepository.InsertAsync(vendorClassAttribute);
        }

        public async Task<VendorClassAttribute> UpdateAsync(
            Guid id,
            Guid? vendorClassId, bool isActive, Guid idAttribute, int sortOrder, bool isRequired, bool isInternal, string controlType, string defaultValue, int idx, [CanBeNull] string concurrencyStamp = null
        )
        {

            var vendorClassAttribute = await _vendorClassAttributeRepository.GetAsync(id);

            vendorClassAttribute.VendorClassId = vendorClassId;
            vendorClassAttribute.IsActive = isActive;
            vendorClassAttribute.IdAttribute = idAttribute;
            vendorClassAttribute.SortOrder = sortOrder;
            vendorClassAttribute.IsRequired = isRequired;
            vendorClassAttribute.IsInternal = isInternal;
            vendorClassAttribute.ControlType = controlType;
            vendorClassAttribute.DefaultValue = defaultValue;
            vendorClassAttribute.Idx = idx;

            vendorClassAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vendorClassAttributeRepository.UpdateAsync(vendorClassAttribute);
        }

    }
}