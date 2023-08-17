using Volo.Abp.Application.Dtos;

namespace HQSOFT.eBiz.AccountPayable.Shared
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public LookupRequestDto()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}