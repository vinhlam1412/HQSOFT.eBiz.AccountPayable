using System;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeExcelDto
    {
        public bool IsActive { get; set; }
        public string? IdAttribute { get; set; }
        public int SortOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }
        public string? ControlType { get; set; }
        public string? DefaultValue { get; set; }
        public int Idx { get; set; }
    }
}