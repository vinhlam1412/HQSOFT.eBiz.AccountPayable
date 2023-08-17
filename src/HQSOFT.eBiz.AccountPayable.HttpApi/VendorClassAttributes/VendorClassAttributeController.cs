using HQSOFT.eBiz.AccountPayable.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    [RemoteService(Name = "AccountPayable")]
    [Area("accountPayable")]
    [ControllerName("VendorClassAttribute")]
    [Route("api/account-payable/vendor-class-attributes")]
    public class VendorClassAttributeController : AbpController, IVendorClassAttributesAppService
    {
        private readonly IVendorClassAttributesAppService _vendorClassAttributesAppService;

        public VendorClassAttributeController(IVendorClassAttributesAppService vendorClassAttributesAppService)
        {
            _vendorClassAttributesAppService = vendorClassAttributesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<VendorClassAttributeDto>> GetListAsync(GetVendorClassAttributesInput input)
        {
            return _vendorClassAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<VendorClassAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _vendorClassAttributesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VendorClassAttributeDto> GetAsync(Guid id)
        {
            return _vendorClassAttributesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("vendor-class-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetVendorClassLookupAsync(LookupRequestDto input)
        {
            return _vendorClassAttributesAppService.GetVendorClassLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<VendorClassAttributeDto> CreateAsync(VendorClassAttributeCreateDto input)
        {
            return _vendorClassAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VendorClassAttributeDto> UpdateAsync(Guid id, VendorClassAttributeUpdateDto input)
        {
            return _vendorClassAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vendorClassAttributesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassAttributeExcelDownloadDto input)
        {
            return _vendorClassAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vendorClassAttributesAppService.GetDownloadTokenAsync();
        }
    }
}