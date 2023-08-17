namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public static class VendorClassConsts
    {
        private const string DefaultSorting = "{0}VendorClassCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VendorClass." : string.Empty);
        }

    }
}