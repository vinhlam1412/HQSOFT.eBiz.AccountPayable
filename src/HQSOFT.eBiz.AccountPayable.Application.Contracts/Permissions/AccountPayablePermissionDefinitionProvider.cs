using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HQSOFT.eBiz.AccountPayable.Permissions;

public class AccountPayablePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AccountPayablePermissions.GroupName, L("Permission:AccountPayable"));

        var vendorClassPermission = myGroup.AddPermission(AccountPayablePermissions.VendorClasses.Default, L("Permission:VendorClasses"));
        vendorClassPermission.AddChild(AccountPayablePermissions.VendorClasses.Create, L("Permission:Create"));
        vendorClassPermission.AddChild(AccountPayablePermissions.VendorClasses.Edit, L("Permission:Edit"));
        vendorClassPermission.AddChild(AccountPayablePermissions.VendorClasses.Delete, L("Permission:Delete"));

        var vendorClassAttributePermission = myGroup.AddPermission(AccountPayablePermissions.VendorClassAttributes.Default, L("Permission:VendorClassAttributes"));
        vendorClassAttributePermission.AddChild(AccountPayablePermissions.VendorClassAttributes.Create, L("Permission:Create"));
        vendorClassAttributePermission.AddChild(AccountPayablePermissions.VendorClassAttributes.Edit, L("Permission:Edit"));
        vendorClassAttributePermission.AddChild(AccountPayablePermissions.VendorClassAttributes.Delete, L("Permission:Delete"));

        var vendorClassCompanyPermission = myGroup.AddPermission(AccountPayablePermissions.VendorClassCompanies.Default, L("Permission:VendorClassCompanies"));
        vendorClassCompanyPermission.AddChild(AccountPayablePermissions.VendorClassCompanies.Create, L("Permission:Create"));
        vendorClassCompanyPermission.AddChild(AccountPayablePermissions.VendorClassCompanies.Edit, L("Permission:Edit"));
        vendorClassCompanyPermission.AddChild(AccountPayablePermissions.VendorClassCompanies.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AccountPayableResource>(name);
    }
}