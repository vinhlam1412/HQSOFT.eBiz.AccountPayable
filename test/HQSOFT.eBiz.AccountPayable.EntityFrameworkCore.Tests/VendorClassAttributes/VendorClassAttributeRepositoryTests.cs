using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.AccountPayable.VendorClassAttributes;
using HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributeRepositoryTests : AccountPayableEntityFrameworkCoreTestBase
    {
        private readonly IVendorClassAttributeRepository _vendorClassAttributeRepository;

        public VendorClassAttributeRepositoryTests()
        {
            _vendorClassAttributeRepository = GetRequiredService<IVendorClassAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorClassAttributeRepository.GetListAsync(
                    isActive: true,
                    idAttribute: "75f934091b02492d92106f28a9f1c07c5b275b9c57aa4761a97efb73da12cf2c24f746cd8c8",
                    isRequired: true,
                    isInternal: true,
                    controlType: "4d3fdcad392c40ea8b",
                    defaultValue: "7eec22dc"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorClassAttributeRepository.GetCountAsync(
                    isActive: true,
                    idAttribute: "bb141de36cc2476ea907f985ca54cba937e8",
                    isRequired: true,
                    isInternal: true,
                    controlType: "186c5effdd5e4f34ae868717fcf2b0af0459e7d84df",
                    defaultValue: "27d5152291054012ac17e1e707545f4995f94a6a8b054dd4aa42b762be399cb54d4099956943440ba48912cfe6b0bf83d2e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}