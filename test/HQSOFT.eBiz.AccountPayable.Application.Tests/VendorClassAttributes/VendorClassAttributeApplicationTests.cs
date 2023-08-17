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
            result.Items.Any(x => x.Id == Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("c6e1a9e3-33de-4a49-91ac-d78a5fea47c2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorClassAttributesAppService.GetAsync(Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorClassAttributeCreateDto
            {
                IsActive = true,
                IdAttribute = Guid.Parse("3a069135-a9a8-4e3e-9d47-c24b064bcb26"),
                SortOrder = 821007142,
                IsRequired = true,
                IsInternal = true,
                ControlType = "349d812ddd6348e0847e7fb8164f5cf6f8ed3f99c3ca",
                DefaultValue = "abf52ae0e7d64a6b9aa1d65fe5f33b0f61f28c8a6523472aa0bb09a60788cf2c2cebb2fcd9f4450dab43e27e5f1bbd",
                Idx = 1013801769
            };

            // Act
            var serviceResult = await _vendorClassAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.IdAttribute.ShouldBe(Guid.Parse("3a069135-a9a8-4e3e-9d47-c24b064bcb26"));
            result.SortOrder.ShouldBe(821007142);
            result.IsRequired.ShouldBe(true);
            result.IsInternal.ShouldBe(true);
            result.ControlType.ShouldBe("349d812ddd6348e0847e7fb8164f5cf6f8ed3f99c3ca");
            result.DefaultValue.ShouldBe("abf52ae0e7d64a6b9aa1d65fe5f33b0f61f28c8a6523472aa0bb09a60788cf2c2cebb2fcd9f4450dab43e27e5f1bbd");
            result.Idx.ShouldBe(1013801769);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorClassAttributeUpdateDto()
            {
                IsActive = true,
                IdAttribute = Guid.Parse("68e3f783-baf6-4b49-9d60-8d3c270f72d4"),
                SortOrder = 1158089264,
                IsRequired = true,
                IsInternal = true,
                ControlType = "1bb1fa5fc381421192774a374223daa5882947b0e0f74d46b371c",
                DefaultValue = "0a49b0947c594e1d9455656ff563ce8c530bc537204c484380647948449754f83aa2db24e54f4f178ceed69865",
                Idx = 747643870
            };

            // Act
            var serviceResult = await _vendorClassAttributesAppService.UpdateAsync(Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"), input);

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.IdAttribute.ShouldBe(Guid.Parse("68e3f783-baf6-4b49-9d60-8d3c270f72d4"));
            result.SortOrder.ShouldBe(1158089264);
            result.IsRequired.ShouldBe(true);
            result.IsInternal.ShouldBe(true);
            result.ControlType.ShouldBe("1bb1fa5fc381421192774a374223daa5882947b0e0f74d46b371c");
            result.DefaultValue.ShouldBe("0a49b0947c594e1d9455656ff563ce8c530bc537204c484380647948449754f83aa2db24e54f4f178ceed69865");
            result.Idx.ShouldBe(747643870);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorClassAttributesAppService.DeleteAsync(Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"));

            // Assert
            var result = await _vendorClassAttributeRepository.FindAsync(c => c.Id == Guid.Parse("203ee3f5-eec6-42e7-92c1-f8d3f3d676f9"));

            result.ShouldBeNull();
        }
    }
}