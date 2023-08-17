using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public bool? IsActive { get; set; }
        public Guid? IdAttribute { get; set; }
        public int? SortOrderMin { get; set; }
        public int? SortOrderMax { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsInternal { get; set; }
        public string? ControlType { get; set; }
        public string? DefaultValue { get; set; }
        public int? IdxMin { get; set; }
        public int? IdxMax { get; set; }
        public Guid? VendorClassId { get; set; }

        public VendorClassAttributeExcelDownloadDto()
        {

        }
    }
}