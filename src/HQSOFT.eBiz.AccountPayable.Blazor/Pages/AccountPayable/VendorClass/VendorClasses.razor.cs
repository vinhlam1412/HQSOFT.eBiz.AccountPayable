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
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.SharedInformation.Attributes;
using Volo.Abp.ObjectMapping;
using HQSOFT.SharedInformation.AttributeDetails;
using HQSOFT.SharedInformation.Permissions;
using Microsoft.AspNetCore.Components.Routing;
using HQSOFT.eBiz.GeneralLedger.CostCenters;
using HQSOFT.SharedInformation.TaxCategoryDetails;
using HQSOFT.SharedInformation.Taxes;
using HQSOFT.SharedInformation.TaxSchedules;
using HQSOFT.eBiz.GeneralLedger.Currencies;
using Microsoft.AspNetCore.Components.Web;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Pages.AccountPayable.VendorClass
{
    public partial class VendorClasses
    {

        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();

        private bool CanCreateVendorClass { get; set; }
        private bool CanEditVendorClass { get; set; }
        private bool CanDeleteVendorClass { get; set; }

        private bool CanCreateVendorClassCompany { get; set; }
        private bool CanEditVendorClassCompany { get; set; }
        private bool CanDeleteVendorClassCompany { get; set; }

        private bool CanCreateVendorClassAttribute { get; set; }
        private bool CanEditVendorClassAttribute { get; set; }
        private bool CanDeleteVendorClassAttribute { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int MaxCount { get; } = 1000;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;

        private IReadOnlyList<CountryDto> CountriesCollection { get; set; } = new List<CountryDto>();
        private IReadOnlyList<CompanyDto> CompaniesCollection { get; set; } = new List<CompanyDto>();
        private IReadOnlyList<AttributeDto> AttributesCollection { get; set; } = new List<AttributeDto>();
        private IReadOnlyList<AccountDto> AccountsCollection { get; set; } = new List<AccountDto>();
        private IReadOnlyList<CostCenterDto> CostCentersCollection { get; set; } = new List<CostCenterDto>();
        private IReadOnlyList<CurrencyDto> CurrenciesCollection { get; set; } = new List<CurrencyDto>();




        private VendorClassDto EditingVendorClass = new VendorClassDto();
        private Guid EditingVendorClassId { get; set; }

        private Guid Term { get; set; }

        private GetVendorClassesInput Filter { get; set; }

        private VendorClassCompanyDto EditingVendorClassCompany { get; set; }
        private VendorClassAttributeDto EditingVendorClassAttribute { get; set; }




        //======================= Grid ====================
        private IGrid VendorClassCompanyGrid { get; set; } //Company grid control name
        private IGrid VendorClassAttributeGrid { get; set; } //Attribute grid control name
        private List<VendorClassCompanyDto> VendorClassCompanies { get; set; } = new List<VendorClassCompanyDto>(); //Data source used to bind to grid
        private List<VendorClassAttributeDto> VendorClassAttributes { get; set; } = new List<VendorClassAttributeDto>(); //Data source used to bind to grid
        private IReadOnlyList<object> selectedVendorClassCompanies { get; set; } = new List<VendorClassCompanyDto>(); //Selected rows on grid
        private IReadOnlyList<object> selectedVendorClassAttributes { get; set; } = new List<VendorClassAttributeDto>(); //Selected rows on grid

        private EditContext _gridVendorClassCompanyEditContext;
        private EditContext _gridVendorClassAttributeEditContext;
        private Guid EditingVendorClassCompaniesId { get; set; } = Guid.Empty; //Editing VendorClassCompanies Id on grid
        private Guid EditingVendorClassAttributesId { get; set; } = Guid.Empty; //Editing VendorClassAttribute Id on grid


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
            AttributesCollection = new List<AttributeDto>();
            CostCentersCollection = new List<CostCenterDto>();
            CurrenciesCollection = new List<CurrencyDto>();
        }

        EditContext GridVendorClassCompanyEditContext
        {
            get { return VendorClassCompanyGrid.IsEditing() ? _gridVendorClassCompanyEditContext : null; }
            set { _gridVendorClassCompanyEditContext = value; }
        }

        EditContext GridVendorClassAttributeEditContext
        {
            get { return VendorClassAttributeGrid.IsEditing() ? _gridVendorClassAttributeEditContext : null; }
            set { _gridVendorClassAttributeEditContext = value; }
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
            EditingVendorClassId = Guid.Parse(Id);
            await LoadDataAsync(EditingVendorClassId);
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionVendorClassAsync();
            await SetPermissionVendorClassAttributeAsync();
            await SetPermissionVendorClassCompanyAsync();
            await GetCountryCollectionLookupAsync();
            await GetCompanyCollectionLookupAsync();
            await GetAccountCollectionLookupAsync();
            await GetAttributeCollectionLookupAsync();
            await GetCostCenterCollectionCollectionLookupAsync();
            await GetCurrencyCollectionCollectionCollectionLookupAsync();
                    
        }
        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:VendorClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {

            Toolbar.AddButton(L["Back"], async () =>
            {
                NavigationManager.NavigateTo($"/AccountPayable/VendorClasses");
            },
            IconName.Undo,
            Color.Secondary);

            Toolbar.AddButton(L["Save"], async () =>
            {
                await SaveVendorClassAsync(false);
            },
            IconName.Save,
            Color.Primary,
            requiredPolicyName: AccountPayablePermissions.VendorClasses.Edit);

            Toolbar.AddButton(L["New"], async () =>
            {
                await SaveVendorClassAsync(true);
            }, IconName.Add, Color.Primary, requiredPolicyName: AccountPayablePermissions.VendorClasses.Create);


            Toolbar.AddButton(L["Delete"],DeleteVendorClass,
            IconName.Delete,
            Color.Danger,
            requiredPolicyName: AccountPayablePermissions.VendorClasses.Delete);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionVendorClassAsync()
        {
            CanCreateVendorClass = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Create);
            CanEditVendorClass = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Edit);
            CanDeleteVendorClass = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClasses.Delete);
        }
        private async Task SetPermissionVendorClassCompanyAsync()
        {
            CanCreateVendorClassCompany = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Create);
            CanEditVendorClassCompany = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Edit);
            CanDeleteVendorClassCompany = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Delete);
        }

        private async Task SetPermissionVendorClassAttributeAsync()
        {
            CanCreateVendorClassAttribute = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Create);
            CanEditVendorClassAttribute = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Edit);
            CanDeleteVendorClassAttribute = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Delete);
        }
        #endregion

        //================================================CRUD & Load Main Data Source Section===============================
        #region

        private async Task LoadDataAsync(Guid vendorClassId)
        {
            if (vendorClassId != Guid.Empty)
            {
                EditingVendorClass = await VendorClassesAppService.GetAsync(vendorClassId);
                await GetVendorClassCompaniesAsync();
                await GetVendorClassAttributesAsync();
                IsDataEntryChanged = false;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                EditingVendorClass = new VendorClassDto();
                VendorClassCompanies = new List<VendorClassCompanyDto> { };
                VendorClassAttributes = new List<VendorClassAttributeDto> { };
                await InvokeAsync(StateHasChanged);
            }
        }
        private async Task GetVendorClassCompaniesAsync()
        {
            var result = await VendorClassCompaniesAppService.GetListAsync(new GetVendorClassCompaniesInput
            {
                VendorClassId = EditingVendorClassId,
                MaxResultCount = MaxCount,

            });
            VendorClassCompanies = ObjectMapper.Map<List<VendorClassCompanyDto>, List<VendorClassCompanyDto>>((List<VendorClassCompanyDto>)result.Items);
        }
        private async Task GetVendorClassAttributesAsync()
        {
            var result = await VendorClassAttributesAppService.GetListAsync(new GetVendorClassAttributesInput
            {
                VendorClassId = EditingVendorClassId,
                MaxResultCount = MaxCount,

            });
            VendorClassAttributes = ObjectMapper.Map<List<VendorClassAttributeDto>, List<VendorClassAttributeDto>>((List<VendorClassAttributeDto>)result.Items);
        }
        private async Task SaveVendorClassAsync(bool IsNewNext)
        {
            try
            {
                if (!EditFormMain.EditContext.Validate())
                {
                    return;
                }
                if (EditingVendorClassId == Guid.Empty)
                {
                    var vendorClass = await VendorClassesAppService.CreateAsync(ObjectMapper.Map<VendorClassDto, VendorClassCreateDto>(EditingVendorClass));
                    EditingVendorClassId = vendorClass.Id;
                    //isVisible = Visibility.Visible;
                }
                else
                {
                    await VendorClassesAppService.UpdateAsync(EditingVendorClassId, ObjectMapper.Map<VendorClassDto, VendorClassUpdateDto>(EditingVendorClass));
                    var vendorClass = await VendorClassesAppService.GetAsync(EditingVendorClassId);
                    EditingVendorClass = vendorClass;
                }

                await SaveVendorClassAttributeDetailAsync();
                await SaveVendorClassCompanyDetailAsync();

                if (IsNewNext)
                    await NewVendorClass();
                else
                    IsDataEntryChanged = false;
                    NavigationManager.NavigateTo($"/AccountPayable/VendorClasses/{EditingVendorClassId}");
                    await LoadDataAsync(EditingVendorClassId);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task NewVendorClass()
        {
            EditingVendorClass = new VendorClassDto
            {
                ConcurrencyStamp = string.Empty,
            };
            EditingVendorClassId = Guid.Empty;
            VendorClassAttributes = new List<VendorClassAttributeDto>();
            VendorClassCompanies = new List<VendorClassCompanyDto>();
            IsDataEntryChanged = false;
            NavigationManager.NavigateTo($"/AccountPayable/VendorClasses/{Guid.Empty}");
            await LoadDataAsync(EditingVendorClassId);
        }

        private async Task DeleteVendorClass()
        {
            var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
            if (confirmed)
            {
                await Task.CompletedTask;
                await DeleteVendorClassCompany();
                await DeleteVendorClassAttribute();
                await DeleteVendorClassAsync(EditingVendorClassId);
            }
        }
        private async Task DeleteVendorClassAsync(Guid Id)
        {
            await VendorClassesAppService.DeleteAsync(Id);
            NavigationManager.NavigateTo("/AccountPayable/VendorClasses");
        }
        private async Task GetVendorClassCompanyAsync()
        {
            var result = await VendorClassCompaniesAppService.GetListAsync(new GetVendorClassCompaniesInput
            {
                VendorClassId = EditingVendorClassId,
                MaxResultCount = MaxCount
            });
            VendorClassCompanies = ObjectMapper.Map<List<VendorClassCompanyDto>, List<VendorClassCompanyDto>>((List<VendorClassCompanyDto>)result.Items);
        }

        private async Task SaveVendorClassAttributeDetailAsync()
        {
            try
            {
                await Task.CompletedTask;
                foreach (var vendorClassAttribute in VendorClassAttributes)
                {
                    if (vendorClassAttribute.Id == Guid.Empty)
                        vendorClassAttribute.VendorClassId = EditingVendorClassId;

                    if (vendorClassAttribute.ConcurrencyStamp == string.Empty)
                        await VendorClassAttributesAppService.CreateAsync(ObjectMapper.Map<VendorClassAttributeDto, VendorClassAttributeCreateDto>(vendorClassAttribute));
                    else
                        await VendorClassAttributesAppService.UpdateAsync(vendorClassAttribute.Id, ObjectMapper.Map<VendorClassAttributeDto, VendorClassAttributeUpdateDto>(vendorClassAttribute));
                }
               await GetVendorClassAttributesAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task SaveVendorClassCompanyDetailAsync()
        {
            try
            {
                await Task.CompletedTask;
                foreach (var vendorClassCompany in VendorClassCompanies)
                {
                    if (vendorClassCompany.Id == Guid.Empty)
                        vendorClassCompany.VendorClassId = EditingVendorClassId;

                    if (vendorClassCompany.ConcurrencyStamp == string.Empty)
                        await VendorClassCompaniesAppService.CreateAsync(ObjectMapper.Map<VendorClassCompanyDto, VendorClassCompanyCreateDto>(vendorClassCompany));
                    else
                        await VendorClassCompaniesAppService.UpdateAsync(vendorClassCompany.Id, ObjectMapper.Map<VendorClassCompanyDto, VendorClassCompanyUpdateDto>(vendorClassCompany));
                }
                await GetVendorClassCompaniesAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task DeleteVendorClassCompany(Guid Id)
        {
            await Task.CompletedTask;
            await VendorClassCompaniesAppService.DeleteAsync(Id);
            await GetVendorClassCompanyAsync();
        }
        private async Task DeleteVendorClassCompany()
        {
            await Task.CompletedTask;
            foreach (var vendorClassCompany in VendorClassCompanies)
            {
                if (vendorClassCompany.ConcurrencyStamp != string.Empty)
                    await VendorClassCompaniesAppService.DeleteAsync(vendorClassCompany.Id);
            }
        }
        private async Task DeleteVendorClassAttribute(Guid Id)
        {
            await Task.CompletedTask;
            await VendorClassAttributesAppService.DeleteAsync(Id);
            await GetVendorClassAttributesAsync();
        }
        private async Task DeleteVendorClassAttribute()
        {
            await Task.CompletedTask;
            foreach (var vendorClassAttribute in VendorClassAttributes)
            {
                if (vendorClassAttribute.ConcurrencyStamp != string.Empty)
                    await VendorClassAttributesAppService.DeleteAsync(vendorClassAttribute.Id);
            }
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
        private async Task GetAttributeCollectionLookupAsync()
        {
            AttributesCollection = (await AttributesAppService.GetListAsync(new GetAttributesInput { MaxResultCount = MaxCount, })).Items;
        }
        private async Task GetCostCenterCollectionCollectionLookupAsync()
        {
            CostCentersCollection = (await CostCentersAppService.GetShortListAsync(new GetCostCentersInput { MaxResultCount = MaxCount, })).Items;
        }
        private async Task GetCurrencyCollectionCollectionCollectionLookupAsync()
        {
            CurrenciesCollection = (await CurrenciesAppService.GetShortListAsync(new GetCurrenciesInput { MaxResultCount = MaxCount, })).Items;
        }

        #endregion


        //================================================Validations=========================================================
        #region
        private async Task<bool> SavingConfirmAsync()
        {
            if (IsDataEntryChanged)
            {
                var confirmed = await _uiMessageService.Confirm(L["SavingConfirmationMessage"]);
                if (confirmed)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            bool checkSaving = await SavingConfirmAsync();
            if (!checkSaving)
                context.PreventNavigation();
        }
        #endregion


        //================================================Controls triggers/events============================================
        #region

        //********************** GRID COMPANY **********************
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
                newRow.VendorClassId = EditingVendorClassId;
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
        //********************** GRID ATTRIBUTE **********************
        private async Task VendorClassAttributes_OnFocusedRowChanged(GridFocusedRowChangedEventArgs e)
        {
            if (VendorClassAttributeGrid.IsEditing() && GridVendorClassAttributeEditContext.IsModified())
            {
                await VendorClassAttributeGrid.SaveChangesAsync();
                IsDataEntryChanged = true;
            }
            else
            {
                await VendorClassAttributeGrid.CancelEditAsync();
            }
        }

        private async Task VendorClassAttributes_OnRowDoubleClick(GridRowClickEventArgs e)
        {
            FocusedColumn = e.Column.Name;
            await e.Grid.StartEditRowAsync(e.VisibleIndex);
            EditingVendorClassAttribute = (VendorClassAttributeDto)e.Grid.GetFocusedDataItem();
            EditingVendorClassAttributesId = EditingVendorClassAttribute.Id;
        }

        private void VendorClassAttributes_OnCustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                var newRow = (VendorClassAttributeDto)e.EditModel;
                newRow.Id = Guid.Empty;
                if (VendorClassAttributeGrid.GetVisibleRowCount() > 0)
                    newRow.Idx = VendorClassAttributes.Max(x => x.Idx) + 1;
                else
                    newRow.Idx = 1;
                //newRow.Id = Guid.NewGuid();
                newRow.VendorClassId = EditingVendorClassId;
                newRow.ConcurrencyStamp = string.Empty;

            }
        }

        private async Task VendorClassAttributes_EditModelSaving(GridEditModelSavingEventArgs e)
        {

            VendorClassAttributeDto editModel = (VendorClassAttributeDto)e.EditModel;
            // Re-query a data item from the data source.
            VendorClassAttributeDto dataItem = e.IsNew ? new VendorClassAttributeDto() : VendorClassAttributes.Find(item => item.Idx == editModel.Idx);
            //Assign changes from the edit model to the data item.
            if (dataItem != null && !e.IsNew)
            {
                VendorClassAttributes.Remove(dataItem);
                VendorClassAttributes.Add(editModel);

            }
            if (editModel != null && e.IsNew)
                VendorClassAttributes.Add(editModel);
        }
        private async Task VendorClassAttributes_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            if (e.DataItem != null)
                await DeleteVendorClassAttribute((e.DataItem as VendorClassCompanyDto).Id);
        }

        private async Task BtnAdd_VendorClassAttributeGrid_OnClick()
        {
            await VendorClassAttributeGrid.SaveChangesAsync();
            VendorClassAttributeGrid.ClearSelection();
            await VendorClassAttributeGrid.StartEditNewRowAsync();
        }

        private async Task BtnDelete_VendorClassAttributeGrid_OnClick()
        {
            if (selectedVendorClassAttributes != null)
            {
                foreach (VendorClassAttributeDto row in selectedVendorClassAttributes)
                    await DeleteVendorClassCompany(row.Id);
                selectedVendorClassAttributes = null;
            }
        }

        private async Task GridVendorClassAttributeDetail_OnKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "F2" && CanEditVendorClassAttribute)
            {
                await VendorClassAttributeGrid.StartEditRowAsync(VendorClassAttributeGrid.GetFocusedRowIndex());
            }
        }

        private async Task GridVendorClassCompanyDetail_OnKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "F2" && CanEditVendorClassCompany)
            {
                await VendorClassCompanyGrid.StartEditRowAsync(VendorClassCompanyGrid.GetFocusedRowIndex());
            }
        }
        #endregion

        //================================================Application Functions===============================================
        #region

        #endregion

    }
}
