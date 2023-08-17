using HQSOFT.eBiz.AccountPayable.Shared;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HQSOFT.eBiz.AccountPayable.Permissions;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{

    [Authorize(AccountPayablePermissions.VendorClassAttributes.Default)]
    public class VendorClassAttributesAppService : ApplicationService, IVendorClassAttributesAppService
    {
        private readonly IDistributedCache<VendorClassAttributeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVendorClassAttributeRepository _vendorClassAttributeRepository;
        private readonly VendorClassAttributeManager _vendorClassAttributeManager;
        private readonly IRepository<VendorClass, Guid> _vendorClassRepository;

        public VendorClassAttributesAppService(IVendorClassAttributeRepository vendorClassAttributeRepository, VendorClassAttributeManager vendorClassAttributeManager, IDistributedCache<VendorClassAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<VendorClass, Guid> vendorClassRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vendorClassAttributeRepository = vendorClassAttributeRepository;
            _vendorClassAttributeManager = vendorClassAttributeManager; _vendorClassRepository = vendorClassRepository;
        }

        public virtual async Task<PagedResultDto<VendorClassAttributeDto>> GetListAsync(GetVendorClassAttributesInput input)
        {
            var totalCount = await _vendorClassAttributeRepository.GetCountAsync(input.FilterText, input.IsActive, input.IdAttribute, input.SortOrderMin, input.SortOrderMax, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.IdxMin, input.IdxMax, input.VendorClassId);
            var items = await _vendorClassAttributeRepository.GetListAsync(input.FilterText, input.IsActive, input.IdAttribute, input.SortOrderMin, input.SortOrderMax, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.IdxMin, input.IdxMax, input.VendorClassId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VendorClassAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorClassAttribute>, List<VendorClassAttributeDto>>(items)
            };
        }

        public virtual async Task<VendorClassAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VendorClassAttributeWithNavigationProperties, VendorClassAttributeWithNavigationPropertiesDto>
                (await _vendorClassAttributeRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VendorClassAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VendorClassAttribute, VendorClassAttributeDto>(await _vendorClassAttributeRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetVendorClassLookupAsync(LookupRequestDto input)
        {
            var query = (await _vendorClassRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.VendorClassCode != null &&
                         x.VendorClassCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<VendorClass>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorClass>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(AccountPayablePermissions.VendorClassAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vendorClassAttributeRepository.DeleteAsync(id);
        }

        [Authorize(AccountPayablePermissions.VendorClassAttributes.Create)]
        public virtual async Task<VendorClassAttributeDto> CreateAsync(VendorClassAttributeCreateDto input)
        {

            var vendorClassAttribute = await _vendorClassAttributeManager.CreateAsync(
            input.VendorClassId, input.IsActive, input.IdAttribute, input.SortOrder, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.Idx
            );

            return ObjectMapper.Map<VendorClassAttribute, VendorClassAttributeDto>(vendorClassAttribute);
        }

        [Authorize(AccountPayablePermissions.VendorClassAttributes.Edit)]
        public virtual async Task<VendorClassAttributeDto> UpdateAsync(Guid id, VendorClassAttributeUpdateDto input)
        {

            var vendorClassAttribute = await _vendorClassAttributeManager.UpdateAsync(
            id,
            input.VendorClassId, input.IsActive, input.IdAttribute, input.SortOrder, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.Idx, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VendorClassAttribute, VendorClassAttributeDto>(vendorClassAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var vendorClassAttributes = await _vendorClassAttributeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsActive, input.IdAttribute, input.SortOrderMin, input.SortOrderMax, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.IdxMin, input.IdxMax);
            var items = vendorClassAttributes.Select(item => new
            {
                IsActive = item.VendorClassAttribute.IsActive,
                IdAttribute = item.VendorClassAttribute.IdAttribute,
                SortOrder = item.VendorClassAttribute.SortOrder,
                IsRequired = item.VendorClassAttribute.IsRequired,
                IsInternal = item.VendorClassAttribute.IsInternal,
                ControlType = item.VendorClassAttribute.ControlType,
                DefaultValue = item.VendorClassAttribute.DefaultValue,
                Idx = item.VendorClassAttribute.Idx,

                VendorClass = item.VendorClass?.VendorClassCode,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VendorClassAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VendorClassAttributeExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public async Task<PagedResultDto<VendorClassAttributeDto>> GeShortListAsync(GetVendorClassAttributesInput input)
        {
            var totalCount = await _vendorClassAttributeRepository.GetShortCountAsync(input.FilterText, input.IsActive, input.IdAttribute, input.SortOrderMin, input.SortOrderMax, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.IdxMin, input.IdxMax, input.VendorClassId);
            var items = await _vendorClassAttributeRepository.GetShortListAsync(input.FilterText, input.IsActive, input.IdAttribute, input.SortOrderMin, input.SortOrderMax, input.IsRequired, input.IsInternal, input.ControlType, input.DefaultValue, input.IdxMin, input.IdxMax, input.VendorClassId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VendorClassAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorClassAttribute>, List<VendorClassAttributeDto>>(items)
            };
        }
    }
}