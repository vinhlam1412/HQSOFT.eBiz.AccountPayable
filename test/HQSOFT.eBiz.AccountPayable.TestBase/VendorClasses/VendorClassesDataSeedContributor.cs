using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.AccountPayable.VendorClasses;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class VendorClassesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVendorClassRepository _vendorClassRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VendorClassesDataSeedContributor(IVendorClassRepository vendorClassRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _vendorClassRepository = vendorClassRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _vendorClassRepository.InsertAsync(new VendorClass
            (
                id: Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"),
                vendorClassCode: "660d4ab39ac14",
                description: "586556a481c24bf1ac",
                currency: Guid.Parse("ac9286f0-8f7c-4112-8808-99fb99d47f4b"),
                country: Guid.Parse("1b3a7a50-96db-4dca-ba89-ec9146848dae"),
                paymentMethodCode: Guid.Parse("c55bfacf-0643-4eaa-ad4c-b3522694b528"),
                cashAccount: Guid.Parse("58f7da7f-4f4a-4121-8b6c-b266051e9521"),
                termId: Guid.Parse("c076e2d2-1538-4b61-87bc-2cf577a0a0b5"),
                receiptAction: "4ff00dea14f440f1916d2be0e1f9c796947dcc096df34cee96b64c9276f9",
                aPAccount: Guid.Parse("3331aee7-ac82-4c21-9ae9-b69d7a427f60"),
                aPCostCenter: Guid.Parse("0904bb1b-6d63-4848-af27-02c9d5ccd7ad"),
                expeneseAccount: Guid.Parse("7c54dc4c-2e25-4cd6-b9d9-a56ebcfb60ba"),
                expeneseCostCenter: Guid.Parse("9f757e53-54ac-4362-97e6-3f8407c7336c"),
                discountAccount: Guid.Parse("93871d00-6dc6-46d2-8aa4-28f0436ad146"),
                discountCostCenter: Guid.Parse("cf3c599b-3189-4c86-b1c4-73fa2b41f5fc"),
                cashDiscountAccount: Guid.Parse("492d00ba-f304-4204-a84c-a72caeb84883"),
                cashDiscountCostCenter: Guid.Parse("9139861d-a631-4518-8aa4-4616aaec157c"),
                prepaymentAccount: Guid.Parse("b15c8164-6f37-4f72-b53b-162dcabdce80"),
                prepaymentCostCenter: Guid.Parse("c26a2caa-3e56-4036-a82e-7f41d328211d"),
                reclassificationAccount: Guid.Parse("39af027d-ad85-440a-81f2-7ebab8b4ee4b"),
                reclassificationCostCenter: Guid.Parse("cfc4a11d-a40f-4ee3-9bf0-66a084f837c8"),
                pOAccrualAccount: Guid.Parse("6a8f2894-e31a-4dd2-8ead-a80338e7c7e8"),
                pOAccrualCostCenter: Guid.Parse("0fe59a08-4131-495c-afe5-50c02879caa7"),
                unrealizedGaintAccount: Guid.Parse("3feffee9-7e74-4534-9170-e162dc0451a5"),
                unrealizedGaintCostCenter: Guid.Parse("f1c17765-c32a-4545-8f34-eb392d723457"),
                unrealizedGaintLossAccount: Guid.Parse("35e7e8bf-ad7c-4d72-a71c-97509e203e0c"),
                unrealizedGaintLossCostCenter: Guid.Parse("a0efcf57-b6d1-42dc-a340-600c05febdca"),
                retainagePayableAccount: Guid.Parse("8ad44c2a-5cd5-4f67-bdb0-329256da038f"),
                retainagePayableCostCenter: Guid.Parse("e8838446-fc7c-4b39-8ebf-22dea1caf847")
            ));

            await _vendorClassRepository.InsertAsync(new VendorClass
            (
                id: Guid.Parse("6e3b6fd7-6177-4926-bd6c-7db0cba2a373"),
                vendorClassCode: "5d29ed70916640f1aa8d606516467",
                description: "eb1bedd4d8ad4494bb393efe8950f9ca86393",
                currency: Guid.Parse("bfc728d3-25b1-4394-8465-08f030881ff9"),
                country: Guid.Parse("51cc9b8d-7a27-48b3-b421-5ff99d616cd8"),
                paymentMethodCode: Guid.Parse("910e7a72-be9f-41e7-8a16-ebd3300818d4"),
                cashAccount: Guid.Parse("fbe81425-f71d-4572-b528-22b281e9258b"),
                termId: Guid.Parse("a42f96c5-0f0f-4218-a15c-04a27d627f44"),
                receiptAction: "0bdabc46f8fe4944b2a9e37d13648732ee15719a29fe4faeb29fdf90581f4a0378dd3c0d9b604a26babca",
                aPAccount: Guid.Parse("f48c8ed4-e38b-4105-9a88-7c13eac15229"),
                aPCostCenter: Guid.Parse("74a9d925-668a-4cbe-b355-e7e749c28d5e"),
                expeneseAccount: Guid.Parse("e8b29dfb-d93c-4569-88b5-77fcb40ddfb3"),
                expeneseCostCenter: Guid.Parse("d43f0e2a-105c-4468-94fb-246685c580d8"),
                discountAccount: Guid.Parse("60d48aa8-be0a-4cca-9c44-cbfec0a30a02"),
                discountCostCenter: Guid.Parse("6a95b2b7-2655-4e49-8c8e-e27a64c612aa"),
                cashDiscountAccount: Guid.Parse("2e10d3f3-796b-4c98-ad80-bf35f21ea3f9"),
                cashDiscountCostCenter: Guid.Parse("939cd9c3-c699-40c9-a27a-ef16962a3151"),
                prepaymentAccount: Guid.Parse("0119ead8-0184-4d61-9069-d87f6c3f123e"),
                prepaymentCostCenter: Guid.Parse("50778675-b1d5-461b-a488-1ababb432ac0"),
                reclassificationAccount: Guid.Parse("c28abaed-7995-4ee0-85fc-315c32e2a60d"),
                reclassificationCostCenter: Guid.Parse("2c0a5963-5b45-4dc5-ab5b-52f1c36cbb7f"),
                pOAccrualAccount: Guid.Parse("f2649376-b12c-4fef-baed-3322ef256399"),
                pOAccrualCostCenter: Guid.Parse("24e70c4f-7901-42aa-8061-16cbd6d391fd"),
                unrealizedGaintAccount: Guid.Parse("1a2aef9a-30c9-4feb-a93f-866f9b8050c9"),
                unrealizedGaintCostCenter: Guid.Parse("8dd4d61c-6819-4c08-bff1-bb1ac2179aa0"),
                unrealizedGaintLossAccount: Guid.Parse("ca35ebf8-0747-4846-92ff-c4be5491d6ac"),
                unrealizedGaintLossCostCenter: Guid.Parse("6a416fd1-2765-4dfe-9a10-b7ba6d198cb2"),
                retainagePayableAccount: Guid.Parse("8b145096-29e4-4f12-8392-bb8f686f2b0f"),
                retainagePayableCostCenter: Guid.Parse("4fd2e137-8732-4ab2-ab6f-e44f464770ff")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}