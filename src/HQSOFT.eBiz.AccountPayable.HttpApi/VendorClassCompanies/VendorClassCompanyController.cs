using HQSOFT.eBiz.AccountPayable.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    [RemoteService(Name = "AccountPayable")]
    [Area("accountPayable")]
    [ControllerName("VendorClassCompany")]
    [Route("api/account-payable/vendor-class-companies")]
    public class VendorClassCompanyController : AbpController, IVendorClassCompaniesAppService
    {
        private readonly IVendorClassCompaniesAppService _vendorClassCompaniesAppService;

        public VendorClassCompanyController(IVendorClassCompaniesAppService vendorClassCompaniesAppService)
        {
            _vendorClassCompaniesAppService = vendorClassCompaniesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<VendorClassCompanyWithNavigationPropertiesDto>> GetListAsync(GetVendorClassCompaniesInput input)
        {
            return _vendorClassCompaniesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<VendorClassCompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _vendorClassCompaniesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VendorClassCompanyDto> GetAsync(Guid id)
        {
            return _vendorClassCompaniesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("vendor-class-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetVendorClassLookupAsync(LookupRequestDto input)
        {
            return _vendorClassCompaniesAppService.GetVendorClassLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<VendorClassCompanyDto> CreateAsync(VendorClassCompanyCreateDto input)
        {
            return _vendorClassCompaniesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VendorClassCompanyDto> UpdateAsync(Guid id, VendorClassCompanyUpdateDto input)
        {
            return _vendorClassCompaniesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vendorClassCompaniesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassCompanyExcelDownloadDto input)
        {
            return _vendorClassCompaniesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vendorClassCompaniesAppService.GetDownloadTokenAsync();
        }
    }
}