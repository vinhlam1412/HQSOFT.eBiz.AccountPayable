using HQSOFT.eBiz.AccountPayable.VendorClasses;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVendorClassAttributeRepository _vendorClassAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly VendorClassesDataSeedContributor _vendorClassesDataSeedContributor;

        public VendorClassAttributesDataSeedContributor(IVendorClassAttributeRepository vendorClassAttributeRepository, IUnitOfWorkManager unitOfWorkManager, VendorClassesDataSeedContributor vendorClassesDataSeedContributor)
        {
            _vendorClassAttributeRepository = vendorClassAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _vendorClassesDataSeedContributor = vendorClassesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _vendorClassesDataSeedContributor.SeedAsync(context);

            await _vendorClassAttributeRepository.InsertAsync(new VendorClassAttribute
            (
                id: Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"),
                isActive: true,
                idAttribute: Guid.Parse("61b89e2c-6927-4843-a50c-2992b760a80c"),
                sortOrder: 1723381933,
                isRequired: true,
                isInternal: true,
                controlType: "ba9421457904408893e04647b00a2c87ac29566",
                defaultValue: "44a113",
                idx: 1026659433,
                vendorClassId: null
            ));

            await _vendorClassAttributeRepository.InsertAsync(new VendorClassAttribute
            (
                id: Guid.Parse("c6e1a9e3-33de-4a49-91ac-d78a5fea47c2"),
                isActive: true,
                idAttribute: Guid.Parse("91d1d7a8-9c3d-42a1-b45d-258a01c3acd1"),
                sortOrder: 1176262626,
                isRequired: true,
                isInternal: true,
                controlType: "8dff4ebcf4194c45ab1346d446170f9f7a9931ac58d245bf8f094bb6019237ab70bd4fc932af4bf7aaf",
                defaultValue: "28d69620a",
                idx: 1338673633,
                vendorClassId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}