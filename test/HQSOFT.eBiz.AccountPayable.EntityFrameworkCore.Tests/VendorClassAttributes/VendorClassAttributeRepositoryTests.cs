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
                    idAttribute: Guid.Parse("61b89e2c-6927-4843-a50c-2992b760a80c"),
                    isRequired: true,
                    isInternal: true,
                    controlType: "ba9421457904408893e04647b00a2c87ac29566",
                    defaultValue: "44a113"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"));
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
                    idAttribute: Guid.Parse("91d1d7a8-9c3d-42a1-b45d-258a01c3acd1"),
                    isRequired: true,
                    isInternal: true,
                    controlType: "8dff4ebcf4194c45ab1346d446170f9f7a9931ac58d245bf8f094bb6019237ab70bd4fc932af4bf7aaf",
                    defaultValue: "28d69620a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}