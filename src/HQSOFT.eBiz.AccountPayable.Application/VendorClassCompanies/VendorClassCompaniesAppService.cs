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
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{

    [Authorize(AccountPayablePermissions.VendorClassCompanies.Default)]
    public class VendorClassCompaniesAppService : ApplicationService, IVendorClassCompaniesAppService
    {
        private readonly IDistributedCache<VendorClassCompanyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVendorClassCompanyRepository _vendorClassCompanyRepository;
        private readonly VendorClassCompanyManager _vendorClassCompanyManager;
        private readonly IRepository<VendorClass, Guid> _vendorClassRepository;

        public VendorClassCompaniesAppService(IVendorClassCompanyRepository vendorClassCompanyRepository, VendorClassCompanyManager vendorClassCompanyManager, IDistributedCache<VendorClassCompanyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<VendorClass, Guid> vendorClassRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vendorClassCompanyRepository = vendorClassCompanyRepository;
            _vendorClassCompanyManager = vendorClassCompanyManager; _vendorClassRepository = vendorClassRepository;
        }

        public virtual async Task<PagedResultDto<VendorClassCompanyDto>> GetListAsync(GetVendorClassCompaniesInput input)
        {
            var totalCount = await _vendorClassCompanyRepository.GetCountAsync(input.FilterText, input.CompanyId, input.Exclude, input.IdxMin, input.IdxMax, input.VendorClassId);
            var items = await _vendorClassCompanyRepository.GetListAsync(input.FilterText, input.CompanyId, input.Exclude, input.IdxMin, input.IdxMax, input.VendorClassId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VendorClassCompanyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorClassCompany>, List<VendorClassCompanyDto>>(items)
            };
        }

        public virtual async Task<VendorClassCompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VendorClassCompanyWithNavigationProperties, VendorClassCompanyWithNavigationPropertiesDto>
                (await _vendorClassCompanyRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VendorClassCompanyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VendorClassCompany, VendorClassCompanyDto>(await _vendorClassCompanyRepository.GetAsync(id));
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

        [Authorize(AccountPayablePermissions.VendorClassCompanies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vendorClassCompanyRepository.DeleteAsync(id);
        }

        [Authorize(AccountPayablePermissions.VendorClassCompanies.Create)]
        public virtual async Task<VendorClassCompanyDto> CreateAsync(VendorClassCompanyCreateDto input)
        {

            var vendorClassCompany = await _vendorClassCompanyManager.CreateAsync(
            input.VendorClassId, input.CompanyId, input.Exclude, input.Idx
            );

            return ObjectMapper.Map<VendorClassCompany, VendorClassCompanyDto>(vendorClassCompany);
        }

        [Authorize(AccountPayablePermissions.VendorClassCompanies.Edit)]
        public virtual async Task<VendorClassCompanyDto> UpdateAsync(Guid id, VendorClassCompanyUpdateDto input)
        {

            var vendorClassCompany = await _vendorClassCompanyManager.UpdateAsync(
            id,
            input.VendorClassId, input.CompanyId, input.Exclude, input.Idx, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VendorClassCompany, VendorClassCompanyDto>(vendorClassCompany);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassCompanyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var vendorClassCompanies = await _vendorClassCompanyRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.CompanyId, input.Exclude, input.IdxMin, input.IdxMax);
            var items = vendorClassCompanies.Select(item => new
            {
                CompanyId = item.VendorClassCompany.CompanyId,
                Exclude = item.VendorClassCompany.Exclude,
                Idx = item.VendorClassCompany.Idx,

                VendorClass = item.VendorClass?.VendorClassCode,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VendorClassCompanies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VendorClassCompanyExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
   
    }
}