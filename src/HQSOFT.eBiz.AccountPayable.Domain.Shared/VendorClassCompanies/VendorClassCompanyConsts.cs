namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public static class VendorClassCompanyConsts
    {
        private const string DefaultSorting = "{0}CompanyId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VendorClassCompany." : string.Empty);
        }

    }
}