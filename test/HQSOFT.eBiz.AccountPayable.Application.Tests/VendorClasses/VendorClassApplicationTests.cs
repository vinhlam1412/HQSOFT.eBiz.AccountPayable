using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.AccountPayable.VendorClasses
{
    public class VendorClassesAppServiceTests : AccountPayableApplicationTestBase
    {
        private readonly IVendorClassesAppService _vendorClassesAppService;
        private readonly IRepository<VendorClass, Guid> _vendorClassRepository;

        public VendorClassesAppServiceTests()
        {
            _vendorClassesAppService = GetRequiredService<IVendorClassesAppService>();
            _vendorClassRepository = GetRequiredService<IRepository<VendorClass, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vendorClassesAppService.GetListAsync(new GetVendorClassesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("6e3b6fd7-6177-4926-bd6c-7db0cba2a373")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorClassesAppService.GetAsync(Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorClassCreateDto
            {
                VendorClassCode = "67e50f032637442da4ce0b",
                Description = "4b01b5e35d0549c0baafa18ae",
                Currency = Guid.Parse("e8ba37b6-162e-42a5-b124-0cd93c826ca3"),
                Country = Guid.Parse("c9caaa2e-3742-44e0-b2ea-547f6047e699"),
                PaymentMethodCode = Guid.Parse("e60261e7-5cb0-4eb1-b5b2-d2fa87887631"),
                CashAccount = Guid.Parse("98532835-ae61-4c42-886e-2fa66a852104"),
                TermId = Guid.Parse("c7f792ec-43be-4d81-ab5f-5aab382fd8e1"),
                ReceiptAction = "5e6c5a6eff7c40e7be5a1a1c53fd0945def54f2da320424aac5b799cbc53efec13463b0095d7442",
                APAccount = Guid.Parse("bd22e448-2d1a-4b18-8e21-dfb5f4bcb426"),
                APCostCenter = Guid.Parse("3bb00d62-d9ce-4c1a-bfd7-9f757e25f8d2"),
                ExpeneseAccount = Guid.Parse("5df0012c-479f-4b06-ad4a-5d7acf5aea34"),
                ExpeneseCostCenter = Guid.Parse("03f58080-a3fc-4a3e-a436-ef8e362ddbdd"),
                DiscountAccount = Guid.Parse("33f7cc59-7226-415c-9ec5-55ca922cbd15"),
                DiscountCostCenter = Guid.Parse("26d8fd93-30a6-4d64-bc7b-eddb96daf468"),
                CashDiscountAccount = Guid.Parse("d2217097-41d1-4c7b-b417-d5a842a8b090"),
                CashDiscountCostCenter = Guid.Parse("ee07e74c-9c0a-4226-93a2-c3d7cfeefb3b"),
                PrepaymentAccount = Guid.Parse("7cd09d39-c72f-4156-923b-bee6d351824b"),
                PrepaymentCostCenter = Guid.Parse("1ab50296-bd97-4c6c-9978-cb212859934a"),
                ReclassificationAccount = Guid.Parse("2aff9cfd-c688-4302-8ea9-e6fa5db9a48e"),
                ReclassificationCostCenter = Guid.Parse("1a16fc9a-313c-42a4-afd4-511bf9c56c6b"),
                POAccrualAccount = Guid.Parse("920d5455-6072-4203-a2e2-05c09acbddf9"),
                POAccrualCostCenter = Guid.Parse("e3a06c8e-406d-4d36-a941-80bdd4483cb9"),
                UnrealizedGaintAccount = Guid.Parse("69ce5a1e-9490-4350-8007-85997d7aacae"),
                UnrealizedGaintCostCenter = Guid.Parse("6d7c4348-57c3-4b89-8250-d3b39165bd44"),
                UnrealizedGaintLossAccount = Guid.Parse("38b0c81c-45cd-42ae-bf91-01c2f296e722"),
                UnrealizedGaintLossCostCenter = Guid.Parse("34f42c7a-4ab6-4af4-b528-7829aa196474"),
                RetainagePayableAccount = Guid.Parse("545adf02-2242-440a-9e73-d598e1dbde94"),
                RetainagePayableCostCenter = Guid.Parse("e30a0345-7073-442c-b760-9c5b097f6231")
            };

            // Act
            var serviceResult = await _vendorClassesAppService.CreateAsync(input);

            // Assert
            var result = await _vendorClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VendorClassCode.ShouldBe("67e50f032637442da4ce0b");
            result.Description.ShouldBe("4b01b5e35d0549c0baafa18ae");
            result.Currency.ShouldBe(Guid.Parse("e8ba37b6-162e-42a5-b124-0cd93c826ca3"));
            result.Country.ShouldBe(Guid.Parse("c9caaa2e-3742-44e0-b2ea-547f6047e699"));
            result.PaymentMethodCode.ShouldBe(Guid.Parse("e60261e7-5cb0-4eb1-b5b2-d2fa87887631"));
            result.CashAccount.ShouldBe(Guid.Parse("98532835-ae61-4c42-886e-2fa66a852104"));
            result.TermId.ShouldBe(Guid.Parse("c7f792ec-43be-4d81-ab5f-5aab382fd8e1"));
            result.ReceiptAction.ShouldBe("5e6c5a6eff7c40e7be5a1a1c53fd0945def54f2da320424aac5b799cbc53efec13463b0095d7442");
            result.APAccount.ShouldBe(Guid.Parse("bd22e448-2d1a-4b18-8e21-dfb5f4bcb426"));
            result.APCostCenter.ShouldBe(Guid.Parse("3bb00d62-d9ce-4c1a-bfd7-9f757e25f8d2"));
            result.ExpeneseAccount.ShouldBe(Guid.Parse("5df0012c-479f-4b06-ad4a-5d7acf5aea34"));
            result.ExpeneseCostCenter.ShouldBe(Guid.Parse("03f58080-a3fc-4a3e-a436-ef8e362ddbdd"));
            result.DiscountAccount.ShouldBe(Guid.Parse("33f7cc59-7226-415c-9ec5-55ca922cbd15"));
            result.DiscountCostCenter.ShouldBe(Guid.Parse("26d8fd93-30a6-4d64-bc7b-eddb96daf468"));
            result.CashDiscountAccount.ShouldBe(Guid.Parse("d2217097-41d1-4c7b-b417-d5a842a8b090"));
            result.CashDiscountCostCenter.ShouldBe(Guid.Parse("ee07e74c-9c0a-4226-93a2-c3d7cfeefb3b"));
            result.PrepaymentAccount.ShouldBe(Guid.Parse("7cd09d39-c72f-4156-923b-bee6d351824b"));
            result.PrepaymentCostCenter.ShouldBe(Guid.Parse("1ab50296-bd97-4c6c-9978-cb212859934a"));
            result.ReclassificationAccount.ShouldBe(Guid.Parse("2aff9cfd-c688-4302-8ea9-e6fa5db9a48e"));
            result.ReclassificationCostCenter.ShouldBe(Guid.Parse("1a16fc9a-313c-42a4-afd4-511bf9c56c6b"));
            result.POAccrualAccount.ShouldBe(Guid.Parse("920d5455-6072-4203-a2e2-05c09acbddf9"));
            result.POAccrualCostCenter.ShouldBe(Guid.Parse("e3a06c8e-406d-4d36-a941-80bdd4483cb9"));
            result.UnrealizedGaintAccount.ShouldBe(Guid.Parse("69ce5a1e-9490-4350-8007-85997d7aacae"));
            result.UnrealizedGaintCostCenter.ShouldBe(Guid.Parse("6d7c4348-57c3-4b89-8250-d3b39165bd44"));
            result.UnrealizedGaintLossAccount.ShouldBe(Guid.Parse("38b0c81c-45cd-42ae-bf91-01c2f296e722"));
            result.UnrealizedGaintLossCostCenter.ShouldBe(Guid.Parse("34f42c7a-4ab6-4af4-b528-7829aa196474"));
            result.RetainagePayableAccount.ShouldBe(Guid.Parse("545adf02-2242-440a-9e73-d598e1dbde94"));
            result.RetainagePayableCostCenter.ShouldBe(Guid.Parse("e30a0345-7073-442c-b760-9c5b097f6231"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorClassUpdateDto()
            {
                VendorClassCode = "c98697e639044922a0bc8d060f4f0da55fa03904c3bb478a",
                Description = "19dfcdfb6cb742f2a3e7712bfda6e690b7291157af3449d68e0190c5a61e2b0b7cc693801fb8406696fc10a02cea",
                Currency = Guid.Parse("8d0d42b9-9316-436f-a469-9216e43a5027"),
                Country = Guid.Parse("cef3b47e-d5c8-4c81-be04-93c60193fac2"),
                PaymentMethodCode = Guid.Parse("756488f4-00a0-4482-aa3b-ee424307adeb"),
                CashAccount = Guid.Parse("5ea9fef4-fb40-44ff-9761-03fab81a0296"),
                TermId = Guid.Parse("ef44cc2a-42cb-4e07-a48d-05bba4d545a2"),
                ReceiptAction = "5f15f14aa8b44600b9972",
                APAccount = Guid.Parse("81eb1eb4-5300-4968-a662-2fdad8b33573"),
                APCostCenter = Guid.Parse("cc476863-8345-45dc-9216-6253f8ae72e5"),
                ExpeneseAccount = Guid.Parse("c42c8e7c-9714-4d85-811e-33b738f7e393"),
                ExpeneseCostCenter = Guid.Parse("4dfb7e40-6acc-4e30-a9e2-98c9fe07edb9"),
                DiscountAccount = Guid.Parse("b578f717-e535-44b6-bddc-229a3e936f6d"),
                DiscountCostCenter = Guid.Parse("3a99706f-1e69-4f71-9840-4d1c4e86b26c"),
                CashDiscountAccount = Guid.Parse("04c2ae51-dcd1-4851-a05a-9d6b6e37e631"),
                CashDiscountCostCenter = Guid.Parse("e3ee94c3-eb54-4849-8d62-102bf473aad2"),
                PrepaymentAccount = Guid.Parse("1239adc9-e3cd-44ec-838e-72c2ad2fcb2b"),
                PrepaymentCostCenter = Guid.Parse("18ed9a65-0893-4982-a504-e1300f4dd110"),
                ReclassificationAccount = Guid.Parse("6f214814-3d8a-4d23-9bad-41cdbfd6b0d8"),
                ReclassificationCostCenter = Guid.Parse("d2f64daf-1f91-40c2-806e-e8897d91dfed"),
                POAccrualAccount = Guid.Parse("a5ef1f63-75d4-4856-9f83-5235fc01102e"),
                POAccrualCostCenter = Guid.Parse("e7b3906f-59d3-48f4-9b3f-b981df1f6d2d"),
                UnrealizedGaintAccount = Guid.Parse("ebb963d7-8eec-4f2c-aa1d-dab32fa0485a"),
                UnrealizedGaintCostCenter = Guid.Parse("e3d5c944-b36c-4c9e-ac43-6e11305c7b9a"),
                UnrealizedGaintLossAccount = Guid.Parse("b3019a70-9e50-4299-af87-bff9af326988"),
                UnrealizedGaintLossCostCenter = Guid.Parse("fd484db3-c444-4cb6-8edd-6fa1adea6c57"),
                RetainagePayableAccount = Guid.Parse("0d112e9e-ceac-4377-9095-6862c35ed359"),
                RetainagePayableCostCenter = Guid.Parse("b77a8827-a948-457d-9e52-4c520cde882d")
            };

            // Act
            var serviceResult = await _vendorClassesAppService.UpdateAsync(Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"), input);

            // Assert
            var result = await _vendorClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VendorClassCode.ShouldBe("c98697e639044922a0bc8d060f4f0da55fa03904c3bb478a");
            result.Description.ShouldBe("19dfcdfb6cb742f2a3e7712bfda6e690b7291157af3449d68e0190c5a61e2b0b7cc693801fb8406696fc10a02cea");
            result.Currency.ShouldBe(Guid.Parse("8d0d42b9-9316-436f-a469-9216e43a5027"));
            result.Country.ShouldBe(Guid.Parse("cef3b47e-d5c8-4c81-be04-93c60193fac2"));
            result.PaymentMethodCode.ShouldBe(Guid.Parse("756488f4-00a0-4482-aa3b-ee424307adeb"));
            result.CashAccount.ShouldBe(Guid.Parse("5ea9fef4-fb40-44ff-9761-03fab81a0296"));
            result.TermId.ShouldBe(Guid.Parse("ef44cc2a-42cb-4e07-a48d-05bba4d545a2"));
            result.ReceiptAction.ShouldBe("5f15f14aa8b44600b9972");
            result.APAccount.ShouldBe(Guid.Parse("81eb1eb4-5300-4968-a662-2fdad8b33573"));
            result.APCostCenter.ShouldBe(Guid.Parse("cc476863-8345-45dc-9216-6253f8ae72e5"));
            result.ExpeneseAccount.ShouldBe(Guid.Parse("c42c8e7c-9714-4d85-811e-33b738f7e393"));
            result.ExpeneseCostCenter.ShouldBe(Guid.Parse("4dfb7e40-6acc-4e30-a9e2-98c9fe07edb9"));
            result.DiscountAccount.ShouldBe(Guid.Parse("b578f717-e535-44b6-bddc-229a3e936f6d"));
            result.DiscountCostCenter.ShouldBe(Guid.Parse("3a99706f-1e69-4f71-9840-4d1c4e86b26c"));
            result.CashDiscountAccount.ShouldBe(Guid.Parse("04c2ae51-dcd1-4851-a05a-9d6b6e37e631"));
            result.CashDiscountCostCenter.ShouldBe(Guid.Parse("e3ee94c3-eb54-4849-8d62-102bf473aad2"));
            result.PrepaymentAccount.ShouldBe(Guid.Parse("1239adc9-e3cd-44ec-838e-72c2ad2fcb2b"));
            result.PrepaymentCostCenter.ShouldBe(Guid.Parse("18ed9a65-0893-4982-a504-e1300f4dd110"));
            result.ReclassificationAccount.ShouldBe(Guid.Parse("6f214814-3d8a-4d23-9bad-41cdbfd6b0d8"));
            result.ReclassificationCostCenter.ShouldBe(Guid.Parse("d2f64daf-1f91-40c2-806e-e8897d91dfed"));
            result.POAccrualAccount.ShouldBe(Guid.Parse("a5ef1f63-75d4-4856-9f83-5235fc01102e"));
            result.POAccrualCostCenter.ShouldBe(Guid.Parse("e7b3906f-59d3-48f4-9b3f-b981df1f6d2d"));
            result.UnrealizedGaintAccount.ShouldBe(Guid.Parse("ebb963d7-8eec-4f2c-aa1d-dab32fa0485a"));
            result.UnrealizedGaintCostCenter.ShouldBe(Guid.Parse("e3d5c944-b36c-4c9e-ac43-6e11305c7b9a"));
            result.UnrealizedGaintLossAccount.ShouldBe(Guid.Parse("b3019a70-9e50-4299-af87-bff9af326988"));
            result.UnrealizedGaintLossCostCenter.ShouldBe(Guid.Parse("fd484db3-c444-4cb6-8edd-6fa1adea6c57"));
            result.RetainagePayableAccount.ShouldBe(Guid.Parse("0d112e9e-ceac-4377-9095-6862c35ed359"));
            result.RetainagePayableCostCenter.ShouldBe(Guid.Parse("b77a8827-a948-457d-9e52-4c520cde882d"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorClassesAppService.DeleteAsync(Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"));

            // Assert
            var result = await _vendorClassRepository.FindAsync(c => c.Id == Guid.Parse("9f0e1107-c8f8-429f-9cc5-1231e1cdce05"));

            result.ShouldBeNull();
        }
    }
}