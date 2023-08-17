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

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class EfCoreVendorClassAttributeRepository : EfCoreRepository<AccountPayableDbContext, VendorClassAttribute, Guid>, IVendorClassAttributeRepository
    {
        public EfCoreVendorClassAttributeRepository(IDbContextProvider<AccountPayableDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VendorClassAttributeWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vendorClassAttribute => new VendorClassAttributeWithNavigationProperties
                {
                    VendorClassAttribute = vendorClassAttribute,
                    VendorClass = dbContext.Set<VendorClass>().FirstOrDefault(c => c.Id == vendorClassAttribute.VendorClassId)
                }).FirstOrDefault();
        }

        public async Task<List<VendorClassAttributeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isActive, idAttribute, sortOrderMin, sortOrderMax, isRequired, isInternal, controlType, defaultValue, idxMin, idxMax, vendorClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassAttributeConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VendorClassAttributeWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vendorClassAttribute in (await GetDbSetAsync())
                   join vendorClass in (await GetDbContextAsync()).Set<VendorClass>() on vendorClassAttribute.VendorClassId equals vendorClass.Id into vendorClasses
                   from vendorClass in vendorClasses.DefaultIfEmpty()
                   select new VendorClassAttributeWithNavigationProperties
                   {
                       VendorClassAttribute = vendorClassAttribute,
                       VendorClass = vendorClass
                   };
        }

        protected virtual IQueryable<VendorClassAttributeWithNavigationProperties> ApplyFilter(
            IQueryable<VendorClassAttributeWithNavigationProperties> query,
            string filterText,
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
            Guid? vendorClassId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VendorClassAttribute.ControlType.ToLower().Contains(filterText.ToLower()) || e.VendorClassAttribute.DefaultValue.ToLower().Contains(filterText.ToLower()))
                    .WhereIf(isActive.HasValue, e => e.VendorClassAttribute.IsActive == isActive)
                    .WhereIf(idAttribute.HasValue, e => e.VendorClassAttribute.IdAttribute == idAttribute)
                    .WhereIf(sortOrderMin.HasValue, e => e.VendorClassAttribute.SortOrder >= sortOrderMin.Value)
                    .WhereIf(sortOrderMax.HasValue, e => e.VendorClassAttribute.SortOrder <= sortOrderMax.Value)
                    .WhereIf(isRequired.HasValue, e => e.VendorClassAttribute.IsRequired == isRequired)
                    .WhereIf(isInternal.HasValue, e => e.VendorClassAttribute.IsInternal == isInternal)
                    .WhereIf(!string.IsNullOrWhiteSpace(controlType), e => e.VendorClassAttribute.ControlType.ToLower().Contains(controlType.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(defaultValue), e => e.VendorClassAttribute.DefaultValue.ToLower().Contains(defaultValue.ToLower()))
                    .WhereIf(idxMin.HasValue, e => e.VendorClassAttribute.Idx >= idxMin.Value)
                    .WhereIf(idxMax.HasValue, e => e.VendorClassAttribute.Idx <= idxMax.Value)
                    .WhereIf(vendorClassId != null && vendorClassId != Guid.Empty, e => e.VendorClass != null && e.VendorClass.Id == vendorClassId);
        }

        public async Task<List<VendorClassAttribute>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, isActive, idAttribute, sortOrderMin, sortOrderMax, isRequired, isInternal, controlType, defaultValue, idxMin, idxMax, vendorClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isActive, idAttribute, sortOrderMin, sortOrderMax, isRequired, isInternal, controlType, defaultValue, idxMin, idxMax, vendorClassId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<VendorClassAttribute>> GetShortListAsync(
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
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, isActive, idAttribute, sortOrderMin, sortOrderMax, isRequired, isInternal, controlType, defaultValue, idxMin, idxMax, vendorClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorClassAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetShortCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isActive, idAttribute, sortOrderMin, sortOrderMax, isRequired, isInternal, controlType, defaultValue, idxMin, idxMax, vendorClassId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        protected virtual IQueryable<VendorClassAttribute> ApplyFilter(
            IQueryable<VendorClassAttribute> query,
            string filterText,
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
             Guid? vendorClassId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ControlType.ToLower().Contains(filterText.ToLower()) || e.DefaultValue.ToLower().Contains(filterText.ToLower()))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(idAttribute.HasValue, e => e.IdAttribute == idAttribute)
                    .WhereIf(vendorClassId.HasValue, e => e.VendorClassId == vendorClassId)
                    .WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin.Value)
                    .WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax.Value)
                    .WhereIf(isRequired.HasValue, e => e.IsRequired == isRequired)
                    .WhereIf(isInternal.HasValue, e => e.IsInternal == isInternal)
                    .WhereIf(!string.IsNullOrWhiteSpace(controlType), e => e.ControlType.ToLower().Contains(controlType.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(defaultValue), e => e.DefaultValue.ToLower().Contains(defaultValue.ToLower()))
                    .WhereIf(idxMin.HasValue, e => e.Idx >= idxMin.Value)
                    .WhereIf(idxMax.HasValue, e => e.Idx <= idxMax.Value);
        }

    }
}