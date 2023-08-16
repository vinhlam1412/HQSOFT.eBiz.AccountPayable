using HQSOFT.eBiz.AccountPayable.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public interface IVendorClassAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<VendorClassAttributeWithNavigationPropertiesDto>> GetListAsync(GetVendorClassAttributesInput input);

        Task<VendorClassAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VendorClassAttributeDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetVendorClassLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VendorClassAttributeDto> CreateAsync(VendorClassAttributeCreateDto input);

        Task<VendorClassAttributeDto> UpdateAsync(Guid id, VendorClassAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}