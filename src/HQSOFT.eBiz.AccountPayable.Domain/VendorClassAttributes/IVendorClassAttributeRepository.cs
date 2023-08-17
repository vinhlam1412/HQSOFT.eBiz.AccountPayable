using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public interface IVendorClassAttributeRepository : IRepository<VendorClassAttribute, Guid>
    {
        Task<VendorClassAttributeWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VendorClassAttributeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? isActive = null,
            Guid? idAttribute = null,
            int? sortOrderMin = null,
            int? sortOrderMax = null,
            bool? isRequired = null,
            bool? isInternal = null,
            string controlType = null,
            string defaultValue = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VendorClassAttribute>> GetListAsync(
                    string filterText = null,
                    bool? isActive = null,
                    Guid? idAttribute = null,
                    int? sortOrderMin = null,
                    int? sortOrderMax = null,
                    bool? isRequired = null,
                    bool? isInternal = null,
                    string controlType = null,
                    string defaultValue = null,
                    int? idxMin = null,
                    int? idxMax = null,
                    Guid? vendorClassId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<List<VendorClassAttribute>> GetShortListAsync(
                   string filterText = null,
                   bool? isActive = null,
                   Guid? idAttribute = null,
                   int? sortOrderMin = null,
                   int? sortOrderMax = null,
                   bool? isRequired = null,
                   bool? isInternal = null,
                   string controlType = null,
                   string defaultValue = null,
                   int? idxMin = null,
                   int? idxMax = null,
                    Guid? vendorClassId = null,
                   string sorting = null,
                   int maxResultCount = int.MaxValue,
                   int skipCount = 0,
                   CancellationToken cancellationToken = default
               );


        Task<long> GetCountAsync(
            string filterText = null,
            bool? isActive = null,
            Guid? idAttribute = null,
            int? sortOrderMin = null,
            int? sortOrderMax = null,
            bool? isRequired = null,
            bool? isInternal = null,
            string controlType = null,
            string defaultValue = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            CancellationToken cancellationToken = default);

        Task<long> GetShortCountAsync(
           string filterText = null,
           bool? isActive = null,
           Guid? idAttribute = null,
           int? sortOrderMin = null,
           int? sortOrderMax = null,
           bool? isRequired = null,
           bool? isInternal = null,
           string controlType = null,
           string defaultValue = null,
           int? idxMin = null,
           int? idxMax = null,
           Guid? vendorClassId = null,
           CancellationToken cancellationToken = default);
    }
}