using HQSOFT.eBiz.AccountPayable.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public interface IVendorClassCompaniesAppService : IApplicationService
    {
        Task<PagedResultDto<VendorClassCompanyWithNavigationPropertiesDto>> GetListAsync(GetVendorClassCompaniesInput input);

        Task<VendorClassCompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VendorClassCompanyDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetVendorClassLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VendorClassCompanyDto> CreateAsync(VendorClassCompanyCreateDto input);

        Task<VendorClassCompanyDto> UpdateAsync(Guid id, VendorClassCompanyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassCompanyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}