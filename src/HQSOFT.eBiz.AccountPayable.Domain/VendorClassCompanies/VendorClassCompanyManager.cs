using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyManager : DomainService
    {
        private readonly IVendorClassCompanyRepository _vendorClassCompanyRepository;

        public VendorClassCompanyManager(IVendorClassCompanyRepository vendorClassCompanyRepository)
        {
            _vendorClassCompanyRepository = vendorClassCompanyRepository;
        }

        public async Task<VendorClassCompany> CreateAsync(
        Guid? vendorClassId, Guid companyId, bool exclude, int idx)
        {

            var vendorClassCompany = new VendorClassCompany(
             GuidGenerator.Create(),
             vendorClassId, companyId, exclude, idx
             );

            return await _vendorClassCompanyRepository.InsertAsync(vendorClassCompany);
        }

        public async Task<VendorClassCompany> UpdateAsync(
            Guid id,
            Guid? vendorClassId, Guid companyId, bool exclude, int idx, [CanBeNull] string concurrencyStamp = null
        )
        {

            var vendorClassCompany = await _vendorClassCompanyRepository.GetAsync(id);

            vendorClassCompany.VendorClassId = vendorClassId;
            vendorClassCompany.CompanyId = companyId;
            vendorClassCompany.Exclude = exclude;
            vendorClassCompany.Idx = idx;

            vendorClassCompany.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vendorClassCompanyRepository.UpdateAsync(vendorClassCompany);
        }

    }
}