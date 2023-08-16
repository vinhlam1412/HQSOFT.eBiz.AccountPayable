using HQSOFT.eBiz.AccountPayable.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using HQSOFT.eBiz.AccountPayable.Localization;
using Volo.Abp.UI.Navigation;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Menus;

public class AccountPayableMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemVendorClasses(context, moduleMenu);

        AddMenuItemVendorClassAttributes(context, moduleMenu);

        AddMenuItemVendorClassCompanies(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<AccountPayableResource>();

        await Task.CompletedTask;
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            AccountPayableMenus.Prefix,
            context.GetLocalizer<AccountPayableResource>()["Menu:AccountPayable"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemVendorClasses(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.AccountPayableMenus.VendorClasses,
                context.GetLocalizer<AccountPayableResource>()["Menu:VendorClasses"],
                "/AccountPayable/VendorClasses",
                icon: "fa fa-file-alt",
                requiredPermissionName: AccountPayablePermissions.VendorClasses.Default
            )
        );
    }

    private static void AddMenuItemVendorClassAttributes(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.AccountPayableMenus.VendorClassAttributes,
                context.GetLocalizer<AccountPayableResource>()["Menu:VendorClassAttributes"],
                "/AccountPayable/VendorClassAttributes",
                icon: "fa fa-file-alt",
                requiredPermissionName: AccountPayablePermissions.VendorClassAttributes.Default
            )
        );
    }

    private static void AddMenuItemVendorClassCompanies(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.AccountPayableMenus.VendorClassCompanies,
                context.GetLocalizer<AccountPayableResource>()["Menu:VendorClassCompanies"],
                "/AccountPayable/VendorClassCompanies",
                icon: "fa fa-file-alt",
                requiredPermissionName: AccountPayablePermissions.VendorClassCompanies.Default
            )
        );
    }
}