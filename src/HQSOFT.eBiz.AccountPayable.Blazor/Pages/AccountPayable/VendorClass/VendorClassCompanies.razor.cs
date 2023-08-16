using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.Permissions;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Pages.AccountPayable.VendorClass
{
    public partial class VendorClassCompanies
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<VendorClassCompanyWithNavigationPropertiesDto> VendorClassCompanyList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateVendorClassCompany { get; set; }
        private bool CanEditVendorClassCompany { get; set; }
        private bool CanDeleteVendorClassCompany { get; set; }
        private VendorClassCompanyCreateDto NewVendorClassCompany { get; set; }
        private Validations NewVendorClassCompanyValidations { get; set; } = new();
        private VendorClassCompanyUpdateDto EditingVendorClassCompany { get; set; }
        private Validations EditingVendorClassCompanyValidations { get; set; } = new();
        private Guid EditingVendorClassCompanyId { get; set; }
        private Modal CreateVendorClassCompanyModal { get; set; } = new();
        private Modal EditVendorClassCompanyModal { get; set; } = new();
        private GetVendorClassCompaniesInput Filter { get; set; }
        private DataGridEntityActionsColumn<VendorClassCompanyWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "vendorClassCompany-create-tab";
        protected string SelectedEditTab = "vendorClassCompany-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> VendorClassesCollection { get; set; } = new List<LookupDto<Guid>>();

        public VendorClassCompanies()
        {
            NewVendorClassCompany = new VendorClassCompanyCreateDto();
            EditingVendorClassCompany = new VendorClassCompanyUpdateDto();
            Filter = new GetVendorClassCompaniesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            VendorClassCompanyList = new List<VendorClassCompanyWithNavigationPropertiesDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetVendorClassCollectionLookupAsync();


        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:VendorClassCompanies"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewVendorClassCompany"], async () =>
            {
                await OpenCreateVendorClassCompanyModalAsync();
            }, IconName.Add, requiredPolicyName: AccountPayablePermissions.VendorClassCompanies.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateVendorClassCompany = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Create);
            CanEditVendorClassCompany = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Edit);
            CanDeleteVendorClassCompany = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassCompanies.Delete);
        }

        private async Task GetVendorClassCompaniesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await VendorClassCompaniesAppService.GetListAsync(Filter);
            VendorClassCompanyList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetVendorClassCompaniesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await VendorClassCompaniesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("AccountPayable") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/account-payable/vendor-class-companies/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<VendorClassCompanyWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetVendorClassCompaniesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateVendorClassCompanyModalAsync()
        {
            NewVendorClassCompany = new VendorClassCompanyCreateDto{
                
                
            };
            await NewVendorClassCompanyValidations.ClearAll();
            await CreateVendorClassCompanyModal.Show();
        }

        private async Task CloseCreateVendorClassCompanyModalAsync()
        {
            NewVendorClassCompany = new VendorClassCompanyCreateDto{
                
                
            };
            await CreateVendorClassCompanyModal.Hide();
        }

        private async Task OpenEditVendorClassCompanyModalAsync(VendorClassCompanyWithNavigationPropertiesDto input)
        {
            var vendorClassCompany = await VendorClassCompaniesAppService.GetWithNavigationPropertiesAsync(input.VendorClassCompany.Id);
            
            EditingVendorClassCompanyId = vendorClassCompany.VendorClassCompany.Id;
            EditingVendorClassCompany = ObjectMapper.Map<VendorClassCompanyDto, VendorClassCompanyUpdateDto>(vendorClassCompany.VendorClassCompany);
            await EditingVendorClassCompanyValidations.ClearAll();
            await EditVendorClassCompanyModal.Show();
        }

        private async Task DeleteVendorClassCompanyAsync(VendorClassCompanyWithNavigationPropertiesDto input)
        {
            await VendorClassCompaniesAppService.DeleteAsync(input.VendorClassCompany.Id);
            await GetVendorClassCompaniesAsync();
        }

        private async Task CreateVendorClassCompanyAsync()
        {
            try
            {
                if (await NewVendorClassCompanyValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassCompaniesAppService.CreateAsync(NewVendorClassCompany);
                await GetVendorClassCompaniesAsync();
                await CloseCreateVendorClassCompanyModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditVendorClassCompanyModalAsync()
        {
            await EditVendorClassCompanyModal.Hide();
        }

        private async Task UpdateVendorClassCompanyAsync()
        {
            try
            {
                if (await EditingVendorClassCompanyValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassCompaniesAppService.UpdateAsync(EditingVendorClassCompanyId, EditingVendorClassCompany);
                await GetVendorClassCompaniesAsync();
                await EditVendorClassCompanyModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

        private async Task GetVendorClassCollectionLookupAsync(string? newValue = null)
        {
            VendorClassesCollection = (await VendorClassCompaniesAppService.GetVendorClassLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
