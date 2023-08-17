using Volo.Abp.Reflection;

namespace HQSOFT.eBiz.AccountPayable.Permissions;

public class AccountPayablePermissions
{
    public const string GroupName = "AccountPayable";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AccountPayablePermissions));
    }

    public static class VendorClasses
    {
        public const string Default = GroupName + ".VendorClasses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class VendorClassAttributes
    {
        public const string Default = GroupName + ".VendorClassAttributes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class VendorClassCompanies
    {
        public const string Default = GroupName + ".VendorClassCompanies";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}