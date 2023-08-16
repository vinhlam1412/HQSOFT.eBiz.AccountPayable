using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public Guid? CompanyId { get; set; }
        public bool? Exclude { get; set; }
        public int? IdxMin { get; set; }
        public int? IdxMax { get; set; }
        public Guid? VendorClassId { get; set; }

        public VendorClassCompanyExcelDownloadDto()
        {

        }
    }
}