using Blazorise;
using DevExpress.Blazor;
using HQSOFT.eBiz.AccountPayable.Permissions;
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.GeneralLedger.Accounts;
using HQSOFT.SharedInformation.Companies;
using HQSOFT.SharedInformation.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using static DevExpress.Drawing.Printing.Internal.DXPageSizeInfo;
using static HQSOFT.SharedInformation.Permissions.SharedInformationPermissions;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Pages.AccountPayable.VendorClass
{
    public partial class VendorClasses
    {

        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();

        private bool CanCreateVendorClass { get; set; }
        private bool CanEditVendorClass { get; set; }
        private bool CanDeleteVendorClass { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int MaxCount { get; } = 1000;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;

        private IReadOnlyCollection<CountryDto> CountriesCollection { get; set; } = new List<CountryDto>();
        private IReadOnlyCollection<CompanyDto> CompaniesCollection { get; set; } = new List<CompanyDto>();
        private IReadOnlyList<AccountDto> AccountsCollection { get; set; } = new List<AccountDto>();




        private VendorClassDto EditingVendorClass = new VendorClassDto();
        private Guid EditingVendorClassId { get; set; }

        private Guid Term { get; set; }

        private GetVendorClassesInput Filter { get; set; }

        private VendorClassCompanyDto EditingVendorClassCompany { get; set; }


        //======================= Grid ====================
        private IGrid VendorClassCompanyGrid { get; set; } //Company grid control name
        private List<VendorClassCompanyDto> VendorClassCompanies { get; set; } = new List<VendorClassCompanyDto>(); //Data source used to bind to grid
        private IReadOnlyList<object> selectedVendorClassCompanies { get; set; } = new List<VendorClassCompanyDto>(); //Selected rows on grid

        private EditContext _gridVendorClassCompanyEditContext;

        private Guid EditingVendorClassCompaniesId { get; set; } = Guid.Empty; //Editing VendorClassCompanies Id on grid


        //private IGrid WarehouseLocationGrid { get; set; } //WarehouseLocationDto grid control name
        //private List<WarehouseLocationDto> WarehouseLocation { get; set; } = new List<WarehouseLocationDto>(); //Data source used to bind to grid
        //private IReadOnlyList<object> selectedWarehouseLocation { get; set; } = new List<WarehouseLocationDto>(); //Selected rows on grid

        //private EditContext _gridWarehouseLocationtEditContext;

        private string FocusedColumn { get; set; }

        private bool IsDataEntryChanged = false;

        private EditForm EditFormMain { get; set; } //Id of Main form




        private readonly IUiMessageService _uiMessageService;

        [Parameter]
        public string Id { get; set; }
        //================================================Initialize Section================================================
        #region
        public VendorClasses(IUiMessageService uiMessageService)
        {
            EditingVendorClass = new VendorClassDto();
            Filter = new GetVendorClassesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            _uiMessageService = uiMessageService;

            CountriesCollection = new List<CountryDto>();
            CompaniesCollection = new List<CompanyDto>();
            AccountsCollection = new List<AccountDto>();
        }

        EditContext GridVendorClassCompanyEditContext
        {
            get { return VendorClassCompanyGrid.IsEditing() ? _gridVendorClassCompanyEditContext : null; }
            set { _gridVendorClassCompanyEditContext = value; }
        }

        protected override async Task OnInitializedAsync()
        {
            Term = Guid.Empty;
            if (IsDataEntryChanged)
            {
                var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                if (confirmed)
                {
                    await JSRuntime.InvokeVoidAsync("preventRefresh");
                }
            }
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetCountryCollectionLookupAsync();
            await GetCompanyCollectionLookupAsync();
            await GetAccountCollectionLookupAsync();
        }
        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:VendorClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {

            Toolbar.AddButton(L["New"], async () =>
            {

            }, IconName.Add, requiredPolicyName: AccountPayablePermissions.VendorClasses.Create);
            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateVendorClass = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Create);
            CanEditVendorClass = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Edit);
            CanDeleteVendorClass = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Delete);
        }
        #endregion

        //================================================CRUD & Load Main Data Source Section===============================
        #region
        private async Task GetVendorClassCompanyAsync()
        {
            var result = await VendorClassCompaniesAppService.GetListAsync(new GetVendorClassCompaniesInput
            {
                VendorClassId = EditingVendorClassId,
                MaxResultCount = MaxCount
            });
            VendorClassCompanies = ObjectMapper.Map<List<VendorClassCompanyDto>, List<VendorClassCompanyDto>>((List<VendorClassCompanyDto>)result.Items);
        }

        private async Task DeleteVendorClassCompany(Guid Id)
        {
            await Task.CompletedTask;
            await VendorClassCompaniesAppService.DeleteAsync(Id);
            await GetVendorClassCompanyAsync();
        }
        #endregion


        //================================================Load Data Source for ListView & Others==============================
        #region
        private async Task GetCountryCollectionLookupAsync()
        {
            CountriesCollection = (await CountriesAppService.GetListAsync(new GetCountriesInput { MaxResultCount = MaxCount, })).Items;
        }

        private async Task GetCompanyCollectionLookupAsync()
        {
            CompaniesCollection = (await CompaniesAppService.GetListAsync(new GetCompaniesInput { MaxResultCount = MaxCount, })).Items;
        }
        private async Task GetAccountCollectionLookupAsync()
        {
            AccountsCollection = (await AccountsAppService.GetShortListAsync(new GetAccountsInput { MaxResultCount = MaxCount, })).Items;
        }

        #endregion


        //================================================Validations=========================================================
        #region
        #endregion


        //================================================Controls triggers/events============================================
        #region
        private async Task VendorClassCompanies_OnFocusedRowChanged(GridFocusedRowChangedEventArgs e)
        {
            if (VendorClassCompanyGrid.IsEditing() && GridVendorClassCompanyEditContext.IsModified())
            {
                await VendorClassCompanyGrid.SaveChangesAsync();
                IsDataEntryChanged = true;
            }
            else
            {
                await VendorClassCompanyGrid.CancelEditAsync();
            }
        }

        private async Task VendorClassCompanies_OnRowDoubleClick(GridRowClickEventArgs e)
        {
            FocusedColumn = e.Column.Name;
            await e.Grid.StartEditRowAsync(e.VisibleIndex);
            EditingVendorClassCompany = (VendorClassCompanyDto)e.Grid.GetFocusedDataItem();
            EditingVendorClassCompaniesId = EditingVendorClassCompany.Id;
        }

        private void VendorClassCompanies_OnCustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                var newRow = (VendorClassCompanyDto)e.EditModel;
                newRow.Id = Guid.Empty;
                if (VendorClassCompanyGrid.GetVisibleRowCount() > 0)
                    newRow.Idx = VendorClassCompanies.Max(x => x.Idx) + 1;
                else
                    newRow.Idx = 1;
                //newRow.Id = Guid.NewGuid();
                newRow.CompanyId = EditingVendorClassId;
                newRow.ConcurrencyStamp = string.Empty;

            }
        }

        private async Task VendorClassCompanies_EditModelSaving(GridEditModelSavingEventArgs e)
        {


            VendorClassCompanyDto editModel = (VendorClassCompanyDto)e.EditModel;
            // Re-query a data item from the data source.
            VendorClassCompanyDto dataItem = e.IsNew ? new VendorClassCompanyDto() : VendorClassCompanies.Find(item => item.Idx == editModel.Idx);
            //Assign changes from the edit model to the data item.
            if (dataItem != null && !e.IsNew)
            {
                VendorClassCompanies.Remove(dataItem);
                VendorClassCompanies.Add(editModel);

            }
            if (editModel != null && e.IsNew)
                VendorClassCompanies.Add(editModel);
        }

        private async Task VendorClassCompanies_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            if (e.DataItem != null)
                await DeleteVendorClassCompany((e.DataItem as VendorClassCompanyDto).Id);
        }

        private async Task BtnAdd_VendorClassCompanyGrid_OnClick()
        {
            await VendorClassCompanyGrid.SaveChangesAsync();
            VendorClassCompanyGrid.ClearSelection();
            await VendorClassCompanyGrid.StartEditNewRowAsync();
        }

        private async Task BtnDelete_VendorClassCompanyGrid_OnClick()
        {
            if (selectedVendorClassCompanies != null)
            {
                foreach (VendorClassCompanyDto row in selectedVendorClassCompanies)
                    await DeleteVendorClassCompany(row.Id);
                selectedVendorClassCompanies = null;
            }
        }
        #endregion

        //================================================Application Functions===============================================
        #region

        #endregion

    }
}
