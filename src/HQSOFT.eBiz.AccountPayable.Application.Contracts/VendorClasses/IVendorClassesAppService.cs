using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public interface IVendorClassesAppService : IApplicationService
    {
        Task<PagedResultDto<VendorClassDto>> GetListAsync(GetVendorClassesInput input);

        Task<VendorClassDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<VendorClassDto> CreateAsync(VendorClassCreateDto input);

        Task<VendorClassDto> UpdateAsync(Guid id, VendorClassUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorClassExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}