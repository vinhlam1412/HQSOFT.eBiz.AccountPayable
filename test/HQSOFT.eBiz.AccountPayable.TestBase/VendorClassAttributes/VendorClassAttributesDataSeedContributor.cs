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
                id: Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"),
                isActive: true,
                idAttribute: "75f934091b02492d92106f28a9f1c07c5b275b9c57aa4761a97efb73da12cf2c24f746cd8c8",
                sortOrder: 1570105945,
                isRequired: true,
                isInternal: true,
                controlType: "4d3fdcad392c40ea8b",
                defaultValue: "7eec22dc",
                idx: 851798728,
                vendorClassId: null
            ));

            await _vendorClassAttributeRepository.InsertAsync(new VendorClassAttribute
            (
                id: Guid.Parse("a6597661-b0ac-4738-9c2d-3c9681543eef"),
                isActive: true,
                idAttribute: "bb141de36cc2476ea907f985ca54cba937e8",
                sortOrder: 843328129,
                isRequired: true,
                isInternal: true,
                controlType: "186c5effdd5e4f34ae868717fcf2b0af0459e7d84df",
                defaultValue: "27d5152291054012ac17e1e707545f4995f94a6a8b054dd4aa42b762be399cb54d4099956943440ba48912cfe6b0bf83d2e",
                idx: 998588650,
                vendorClassId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}