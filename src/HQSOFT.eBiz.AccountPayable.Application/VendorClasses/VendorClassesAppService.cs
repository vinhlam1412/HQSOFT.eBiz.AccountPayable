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
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{

    [Authorize(AccountPayablePermissions.VendorClasses.Default)]
    public class VendorClassesAppService : ApplicationService, IVendorClassesAppService
    {
        private readonly IDistributedCache<VendorClassExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVendorClassRepository _vendorClassRepository;
        private readonly VendorClassManager _vendorClassManager;

        public VendorClassesAppService(IVendorClassRepository vendorClassRepository, VendorClassManager vendorClassManager, IDistributedCache<VendorClassExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vendorClassRepository = vendorClassRepository;
            _vendorClassManager = vendorClassManager;
        }

        public virtual async Task<PagedResultDto<VendorClassDto>> GetListAsync(GetVendorClassesInput input)
        {
            var totalCount = await _vendorClassRepository.GetCountAsync(input.FilterText, input.VendorClassCode, input.Description, input.Currency, input.Country, input.PaymentMethodCode, input.CashAccount, input.TermId, input.ReceiptAction, input.APAccount, input.APCostCenter, input.ExpeneseAccount, input.ExpeneseCostCenter, input.DiscountAccount, input.DiscountCostCenter, input.CashDiscountAccount, input.CashDiscountCostCenter, input.PrepaymentAccount, input.PrepaymentCostCenter, input.ReclassificationAccount, input.ReclassificationCostCenter, input.POAccrualAccount, input.POAccrualCostCenter, input.UnrealizedGaintAccount, input.UnrealizedGaintCostCenter, input.UnrealizedGaintLossAccount, input.UnrealizedGaintLossCostCenter, input.RetainagePayableAccount, input.RetainagePayableCostCenter);
            var items = await _vendorClassRepository.GetListAsync(input.FilterText, input.VendorClassCode, input.Description, input.Currency, input.Country, input.PaymentMethodCode, input.CashAccount, input.TermId, input.ReceiptAction, input.APAccount, input.APCostCenter, input.ExpeneseAccount, input.ExpeneseCostCenter, input.DiscountAccount, input.DiscountCostCenter, input.CashDiscountAccount, input.CashDiscountCostCenter, input.PrepaymentAccount, input.PrepaymentCostCenter, input.ReclassificationAccount, input.ReclassificationCostCenter, input.POAccrualAccount, input.POAccrualCostCenter, input.UnrealizedGaintAccount, input.UnrealizedGaintCostCenter, input.UnrealizedGaintLossAccount, input.UnrealizedGaintLossCostCenter, input.RetainagePayableAccount, input.RetainagePayableCostCenter, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VendorClassDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorClass>, List<VendorClassDto>>(items)
            };
        }

        public virtual async Task<VendorClassDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VendorClass, VendorClassDto>(await _vendorClassRepository.GetAsync(id));
        }

        [Authorize(AccountPayablePermissions.VendorClasses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vendorClassRepository.DeleteAsync(id);
        }

        [Authorize(AccountPayablePermissions.VendorClasses.Create)]
        public virtual async Task<VendorClassDto> CreateAsync(VendorClassCreateDto input)
        {

            var vendorClass = await _vendorClassManager.CreateAsync(
            input.VendorClassCode, input.Description, input.Currency, input.Country, input.PaymentMethodCode, input.CashAccount, input.TermId, input.ReceiptAction, input.APAccount, input.APCostCenter, input.ExpeneseAccount, input.ExpeneseCostCenter, input.DiscountAccount, input.DiscountCostCenter, input.CashDiscountAccount, input.CashDiscountCostCenter, input.PrepaymentAccount, input.PrepaymentCostCenter, input.ReclassificationAccount, input.ReclassificationCostCenter, input.POAccrualAccount, input.POAccrualCostCenter, input.UnrealizedGaintAccount, input.UnrealizedGaintCostCenter, input.UnrealizedGaintLossAccount, input.UnrealizedGaintLossCostCenter, input.RetainagePayableAccount, input.RetainagePayableCostCenter
            );

            return ObjectMapper.Map<VendorClass, VendorClassDto>(vendorClass);
        }

        [Authorize(AccountPayablePermissions.VendorClasses.Edit)]
        public virtual async Task<VendorClassDto> UpdateAsync(Guid id, VendorClassUpdateDto input)
        {

            var vendorClass = await _vendorClassManager.UpdateAsync(
            id,
            input.VendorClassCode, input.Description, input.Currency, input.Country, input.PaymentMethodCode, input.CashAccount, input.TermId, input.ReceiptAction, input.APAccount, input.APCostCenter, input.ExpeneseAccount, input.ExpeneseCostCenter, input.DiscountAccount, input.DiscountCostCenter, input.CashDiscountAccount, input.CashDiscountCostCenter, input.PrepaymentAccount, input.PrepaymentCostCenter, input.ReclassificationAccount, input.ReclassificationCostCenter, input.POAccrualAccount, input.POAccrualCostCenter, input.UnrealizedGaintAccount, input.UnrealizedGaintCostCenter, input.UnrealizedGaintLossAccount, input.UnrealizedGaintLossCostCenter, input.RetainagePayableAccount, input.RetainagePayableCostCenter, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VendorClass, VendorClassDto>(vendorClass);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _vendorClassRepository.GetListAsync(input.FilterText, input.VendorClassCode, input.Description, input.Currency, input.Country, input.PaymentMethodCode, input.CashAccount, input.TermId, input.ReceiptAction, input.APAccount, input.APCostCenter, input.ExpeneseAccount, input.ExpeneseCostCenter, input.DiscountAccount, input.DiscountCostCenter, input.CashDiscountAccount, input.CashDiscountCostCenter, input.PrepaymentAccount, input.PrepaymentCostCenter, input.ReclassificationAccount, input.ReclassificationCostCenter, input.POAccrualAccount, input.POAccrualCostCenter, input.UnrealizedGaintAccount, input.UnrealizedGaintCostCenter, input.UnrealizedGaintLossAccount, input.UnrealizedGaintLossCostCenter, input.RetainagePayableAccount, input.RetainagePayableCostCenter);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<VendorClass>, List<VendorClassExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VendorClasses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VendorClassExcelDownloadTokenCacheItem { Token = token },
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