namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public static class VendorClassAttributeConsts
    {
        private const string DefaultSorting = "{0}IsActive asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VendorClassAttribute." : string.Empty);
        }

    }
}