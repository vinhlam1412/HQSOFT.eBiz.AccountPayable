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
using HQSOFT.eBiz.AccountPayable.VendorClasses;
using HQSOFT.eBiz.AccountPayable.Permissions;
using HQSOFT.eBiz.AccountPayable.Shared;
using HQSOFT.SharedInformation.Permissions;
using HQSOFT.SharedInformation.Taxes;
using Volo.Abp.AspNetCore.Components.Messages;

namespace HQSOFT.eBiz.AccountPayable.Blazor.Pages.AccountPayable.VendorClass
{
    public partial class VendorClassListView
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<VendorClassDto> VendorClassList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateVendorClass { get; set; }
        private bool CanEditVendorClass { get; set; }
        private bool CanDeleteVendorClass { get; set; }
        private VendorClassCreateDto NewVendorClass { get; set; }
        private Validations NewVendorClassValidations { get; set; } = new();
        private VendorClassUpdateDto EditingVendorClass { get; set; }
        private Validations EditingVendorClassValidations { get; set; } = new();
        private Guid EditingVendorClassId { get; set; }
        private Modal CreateVendorClassModal { get; set; } = new();
        private Modal EditVendorClassModal { get; set; } = new();
        private GetVendorClassesInput Filter { get; set; }
        private List<VendorClassDto> SelectedVendorClasses { get; set; }

        private readonly IUiMessageService _uiMessageService;
        public VendorClassListView(IUiMessageService uiMessageService)
        {
            NewVendorClass = new VendorClassCreateDto();
            EditingVendorClass = new VendorClassUpdateDto();
            Filter = new GetVendorClassesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            _uiMessageService = uiMessageService;
            VendorClassList = new List<VendorClassDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:VendorClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);

            Toolbar.AddButton(L["New"], () =>
            {
                NavigationManager.NavigateTo($"/AccountPayable/VendorClasses/{Guid.Empty}");
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: AccountPayablePermissions.VendorClasses.Create);


            Toolbar.AddButton(L["Delete"], async () =>
            {
                if (SelectedVendorClasses.Count > 0)
                {
                    var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                    if (confirmed)
                    {
                        foreach (VendorClassDto selectedVendorClass in SelectedVendorClasses)
                        {
                            await VendorClassesAppService.DeleteAsync(selectedVendorClass.Id);
                        }
                        await GetVendorClassesAsync();
                    }

                }
            }, IconName.Delete,
           Color.Danger,
           requiredPolicyName: AccountPayablePermissions.VendorClasses.Delete);
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

        private async Task GetVendorClassesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await VendorClassesAppService.GetListAsync(Filter);
            VendorClassList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetVendorClassesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await VendorClassesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("AccountPayable") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/account-payable/vendor-classes/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<VendorClassDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetVendorClassesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateVendorClassModalAsync()
        {
            NewVendorClass = new VendorClassCreateDto{
                
                
            };
            await NewVendorClassValidations.ClearAll();
            await CreateVendorClassModal.Show();
        }

        private async Task CloseCreateVendorClassModalAsync()
        {
            NewVendorClass = new VendorClassCreateDto{
                
                
            };
            await CreateVendorClassModal.Hide();
        }

        private async Task OpenEditVendorClassModalAsync(VendorClassDto input)
        {
            var vendorClass = await VendorClassesAppService.GetAsync(input.Id);
            
            EditingVendorClassId = vendorClass.Id;
            EditingVendorClass = ObjectMapper.Map<VendorClassDto, VendorClassUpdateDto>(vendorClass);
            await EditingVendorClassValidations.ClearAll();
            await EditVendorClassModal.Show();
        }

        private async Task DeleteVendorClassAsync(VendorClassDto input)
        {
            await VendorClassesAppService.DeleteAsync(input.Id);
            await GetVendorClassesAsync();
        }

        private async Task CreateVendorClassAsync()
        {
            try
            {
                if (await NewVendorClassValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassesAppService.CreateAsync(NewVendorClass);
                await GetVendorClassesAsync();
                await CloseCreateVendorClassModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditVendorClassModalAsync()
        {
            await EditVendorClassModal.Hide();
        }

        private async Task UpdateVendorClassAsync()
        {
            try
            {
                if (await EditingVendorClassValidations.ValidateAll() == false)
                {
                    return;
                }

                await VendorClassesAppService.UpdateAsync(EditingVendorClassId, EditingVendorClass);
                await GetVendorClassesAsync();
                await EditVendorClassModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private void GoToEditPage(VendorClassDto vendorClass)
        {
            NavigationManager.NavigateTo($"/AccountPayable/VendorClasses/{vendorClass.Id}");
        }


    }
}
