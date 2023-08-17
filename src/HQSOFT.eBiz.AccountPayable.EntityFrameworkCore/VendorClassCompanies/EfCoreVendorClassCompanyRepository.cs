using HQSOFT.eBiz.AccountPayable.VendorClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class EfCoreVendorClassCompanyRepository : EfCoreRepository<AccountPayableDbContext, VendorClassCompany, Guid>, IVendorClassCompanyRepository
    {
        public EfCoreVendorClassCompanyRepository(IDbContextProvider<AccountPayableDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VendorClassCompanyWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vendorClassCompany => new VendorClassCompanyWithNavigationProperties
                {
                    VendorClassCompany = vendorClassCompany,
                    VendorClass = dbContext.Set<VendorClass>().FirstOrDefault(c => c.Id == vendorClassCompany.VendorClassId)
                }).FirstOrDefault();
        }

        public async Task<List<VendorClassCompanyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, companyId, exclude, idxMin, idxMax, vendorClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassCompanyConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VendorClassCompanyWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vendorClassCompany in (await GetDbSetAsync())
                   join vendorClass in (await GetDbContextAsync()).Set<VendorClass>() on vendorClassCompany.VendorClassId equals vendorClass.Id into vendorClasses
                   from vendorClass in vendorClasses.DefaultIfEmpty()
                   select new VendorClassCompanyWithNavigationProperties
                   {
                       VendorClassCompany = vendorClassCompany,
                       VendorClass = vendorClass
                   };
        }

        protected virtual IQueryable<VendorClassCompanyWithNavigationProperties> ApplyFilter(
            IQueryable<VendorClassCompanyWithNavigationProperties> query,
            string filterText,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(companyId.HasValue, e => e.VendorClassCompany.CompanyId == companyId)
                    .WhereIf(exclude.HasValue, e => e.VendorClassCompany.Exclude == exclude)
                    .WhereIf(idxMin.HasValue, e => e.VendorClassCompany.Idx >= idxMin.Value)
                    .WhereIf(idxMax.HasValue, e => e.VendorClassCompany.Idx <= idxMax.Value)
                    .WhereIf(vendorClassId != null && vendorClassId != Guid.Empty, e => e.VendorClass != null && e.VendorClass.Id == vendorClassId);
        }

        public async Task<List<VendorClassCompany>> GetListAsync(
            string filterText = null,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
             Guid? vendorClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, companyId, exclude, idxMin, idxMax, vendorClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassCompanyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? vendorClassId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, companyId, exclude, idxMin, idxMax, vendorClassId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VendorClassCompany> ApplyFilter(
            IQueryable<VendorClassCompany> query,
            string filterText,
            Guid? companyId = null,
            bool? exclude = null,
            int? idxMin = null,
            int? idxMax = null,
             Guid? vendorClassId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(companyId.HasValue, e => e.CompanyId == companyId)
                    .WhereIf(companyId.HasValue, e => e.VendorClassId == vendorClassId)
                    .WhereIf(exclude.HasValue, e => e.Exclude == exclude)
                    .WhereIf(idxMin.HasValue, e => e.Idx >= idxMin.Value)
                    .WhereIf(idxMax.HasValue, e => e.Idx <= idxMax.Value);
        }

    }
}