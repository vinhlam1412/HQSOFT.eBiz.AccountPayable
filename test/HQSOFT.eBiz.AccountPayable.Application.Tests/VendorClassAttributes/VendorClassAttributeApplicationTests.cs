using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.AccountPayable.VendorClassAttributes
{
    public class VendorClassAttributesAppServiceTests : AccountPayableApplicationTestBase
    {
        private readonly IVendorClassAttributesAppService _vendorClassAttributesAppService;
        private readonly IRepository<VendorClassAttribute, Guid> _vendorClassAttributeRepository;

        public VendorClassAttributesAppServiceTests()
        {
            _vendorClassAttributesAppService = GetRequiredService<IVendorClassAttributesAppService>();
            _vendorClassAttributeRepository = GetRequiredService<IRepository<VendorClassAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vendorClassAttributesAppService.GetListAsync(new GetVendorClassAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.VendorClassAttribute.Id == Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c")).ShouldBe(true);
            result.Items.Any(x => x.VendorClassAttribute.Id == Guid.Parse("a6597661-b0ac-4738-9c2d-3c9681543eef")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorClassAttributesAppService.GetAsync(Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorClassAttributeCreateDto
            {
                IsActive = true,
                IdAttribute = "2bb42d94852241fd979",
                SortOrder = 1994881383,
                IsRequired = true,
                IsInternal = true,
                ControlType = "eb2494dd6ac44e28bc220b68e75",
                DefaultValue = "4c835999d9764676b821e9caae1ba4",
                Idx = 802396866
            };

            // Act
            var serviceResult = await _vendorClassAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.IdAttribute.ShouldBe("2bb42d94852241fd979");
            result.SortOrder.ShouldBe(1994881383);
            result.IsRequired.ShouldBe(true);
            result.IsInternal.ShouldBe(true);
            result.ControlType.ShouldBe("eb2494dd6ac44e28bc220b68e75");
            result.DefaultValue.ShouldBe("4c835999d9764676b821e9caae1ba4");
            result.Idx.ShouldBe(802396866);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorClassAttributeUpdateDto()
            {
                IsActive = true,
                IdAttribute = "98f4cc64239941c697528d54d993e4a1fdf4a6faeae44d6e8beae7da648",
                SortOrder = 836511847,
                IsRequired = true,
                IsInternal = true,
                ControlType = "c6f3f41697a64e9eb78a39f11d3e962eadbed562610245ef9afa27b90ca7938661779977d4ad",
                DefaultValue = "7f8d5cc93ddf4d0a836f52018efb755e2d3e8b60e4ab4556bf7739a570e7509ff4fed3",
                Idx = 225101664
            };

            // Act
            var serviceResult = await _vendorClassAttributesAppService.UpdateAsync(Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"), input);

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.IdAttribute.ShouldBe("98f4cc64239941c697528d54d993e4a1fdf4a6faeae44d6e8beae7da648");
            result.SortOrder.ShouldBe(836511847);
            result.IsRequired.ShouldBe(true);
            result.IsInternal.ShouldBe(true);
            result.ControlType.ShouldBe("c6f3f41697a64e9eb78a39f11d3e962eadbed562610245ef9afa27b90ca7938661779977d4ad");
            result.DefaultValue.ShouldBe("7f8d5cc93ddf4d0a836f52018efb755e2d3e8b60e4ab4556bf7739a570e7509ff4fed3");
            result.Idx.ShouldBe(225101664);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorClassAttributesAppService.DeleteAsync(Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"));

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == Guid.Parse("59655bd3-016c-4552-ba9e-674ec6d6cc8c"));

            result.ShouldBeNull();
        }
    }
}