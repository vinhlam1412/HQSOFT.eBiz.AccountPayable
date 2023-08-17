using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    [RemoteService(Name = "AccountPayable")]
    [Area("accountPayable")]
    [ControllerName("VendorClass")]
    [Route("api/account-payable/vendor-classes")]
    public class VendorClassController : AbpController, IVendorClassesAppService
    {
        private readonly IVendorClassesAppService _vendorClassesAppService;

        public VendorClassController(IVendorClassesAppService vendorClassesAppService)
        {
            _vendorClassesAppService = vendorClassesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<VendorClassDto>> GetListAsync(GetVendorClassesInput input)
        {
            return _vendorClassesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VendorClassDto> GetAsync(Guid id)
        {
            return _vendorClassesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<VendorClassDto> CreateAsync(VendorClassCreateDto input)
        {
            return _vendorClassesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VendorClassDto> UpdateAsync(Guid id, VendorClassUpdateDto input)
        {
            return _vendorClassesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vendorClassesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassExcelDownloadDto input)
        {
            return _vendorClassesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vendorClassesAppService.GetDownloadTokenAsync();
        }
    }
}