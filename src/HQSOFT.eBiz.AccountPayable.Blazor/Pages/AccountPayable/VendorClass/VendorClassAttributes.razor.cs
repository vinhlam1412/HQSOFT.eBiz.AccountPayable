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
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.eBiz.AccountPayable.Permissions;
using HQSOFT.eBiz.AccountPayable.Shared;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Pages.AccountPayable.VendorClass
{
    public partial class VendorClassAttributes
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<VendorClassAttributeWithNavigationPropertiesDto> VendorClassAttributeList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateVendorClassAttribute { get; set; }
        private bool CanEditVendorClassAttribute { get; set; }
        private bool CanDeleteVendorClassAttribute { get; set; }
        private VendorClassAttributeCreateDto NewVendorClassAttribute { get; set; }
        private Validations NewVendorClassAttributeValidations { get; set; } = new();
        private VendorClassAttributeUpdateDto EditingVendorClassAttribute { get; set; }
        private Validations EditingVendorClassAttributeValidations { get; set; } = new();
        private Guid EditingVendorClassAttributeId { get; set; }
        private Modal CreateVendorClassAttributeModal { get; set; } = new();
        private Modal EditVendorClassAttributeModal { get; set; } = new();
        private GetVendorClassAttributesInput Filter { get; set; }
        private DataGridEntityActionsColumn<VendorClassAttributeWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "vendorClassAttribute-create-tab";
        protected string SelectedEditTab = "vendorClassAttribute-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> VendorClassesCollection { get; set; } = new List<LookupDto<Guid>>();

        public VendorClassAttributes()
        {
            NewVendorClassAttribute = new VendorClassAttributeCreateDto();
            EditingVendorClassAttribute = new VendorClassAttributeUpdateDto();
            Filter = new GetVendorClassAttributesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            VendorClassAttributeList = new List<VendorClassAttributeWithNavigationPropertiesDto>();
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:VendorClassAttributes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewVendorClassAttribute"], async () =>
            {
                await OpenCreateVendorClassAttributeModalAsync();
            }, IconName.Add, requiredPolicyName: AccountPayablePermissions.VendorClassAttributes.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateVendorClassAttribute = await AuthorizationService
                .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Create);
            CanEditVendorClassAttribute = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Edit);
            CanDeleteVendorClassAttribute = await AuthorizationService
                            .IsGrantedAsync(AccountPayablePermissions.VendorClassAttributes.Delete);
        }

        private async Task GetVendorClassAttributesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await VendorClassAttributesAppService.GetListAsync(Filter);
            VendorClassAttributeList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetVendorClassAttributesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await VendorClassAttributesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("AccountPayable") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/account-payable/vendor-class-attributes/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<VendorClassAttributeWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetVendorClassAttributesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateVendorClassAttributeModalAsync()
        {
            NewVendorClassAttribute = new VendorClassAttributeCreateDto{
                
                
            };
            await NewVendorClassAttributeValidations.ClearAll();
            await CreateVendorClassAttributeModal.Show();
        }

        private async Task CloseCreateVendorClassAttributeModalAsync()
        {
            NewVendorClassAttribute = new VendorClassAttributeCreateDto{
                
                
            };
            await CreateVendorClassAttributeModal.Hide();
        }

        private async Task OpenEditVendorClassAttributeModalAsync(VendorClassAttributeWithNavigationPropertiesDto input)
        {
            var vendorClassAttribute = await VendorClassAttributesAppService.GetWithNavigationPropertiesAsync(input.VendorClassAttribute.Id);
            
            EditingVendorClassAttributeId = vendorClassAttribute.VendorClassAttribute.Id;
            EditingVendorClassAttribute = ObjectMapper.Map<VendorClassAttributeDto, VendorClassAttributeUpdateDto>(vendorClassAttribute.VendorClassAttribute);
            await EditingVendorClassAttributeValidations.ClearAll();
            await EditVendorClassAttributeModal.Show();
        }

        private async Task DeleteVendorClassAttributeAsync(VendorClassAttributeWithNavigationPropertiesDto input)
        {
            await VendorClassAttributesAppService.DeleteAsync(input.VendorClassAttribute.Id);
            await GetVendorClassAttributesAsync();
        }

        private async Task CreateVendorClassAttributeAsync()
        {
            try
            {
                if (await NewVendorClassAttributeValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassAttributesAppService.CreateAsync(NewVendorClassAttribute);
                await GetVendorClassAttributesAsync();
                await CloseCreateVendorClassAttributeModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditVendorClassAttributeModalAsync()
        {
            await EditVendorClassAttributeModal.Hide();
        }

        private async Task UpdateVendorClassAttributeAsync()
        {
            try
            {
                if (await EditingVendorClassAttributeValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassAttributesAppService.UpdateAsync(EditingVendorClassAttributeId, EditingVendorClassAttribute);
                await GetVendorClassAttributesAsync();
                await EditVendorClassAttributeModal.Hide();                
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
            VendorClassesCollection = (await VendorClassAttributesAppService.GetVendorClassLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
