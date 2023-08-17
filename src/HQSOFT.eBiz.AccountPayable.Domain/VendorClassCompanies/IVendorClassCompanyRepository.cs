using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public interface IVendorClassCompanyRepository : IRepository<VendorClassCompany, Guid>
    {
        Task<VendorClassCompanyWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VendorClassCompanyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VendorClassCompany>> GetListAsync(
                    string filterText = null,
                    Guid? companyId = null,
                    bool? exclude = null,
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
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            CancellationToken cancellationToken = default);
     
    }
}