using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompaniesAppServiceTests : AccountPayableApplicationTestBase
    {
        private readonly IVendorClassCompaniesAppService _vendorClassCompaniesAppService;
        private readonly IRepository<VendorClassCompany, Guid> _vendorClassCompanyRepository;

        public VendorClassCompaniesAppServiceTests()
        {
            _vendorClassCompaniesAppService = GetRequiredService<IVendorClassCompaniesAppService>();
            _vendorClassCompanyRepository = GetRequiredService<IRepository<VendorClassCompany, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vendorClassCompaniesAppService.GetListAsync(new GetVendorClassCompaniesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.VendorClassCompany.Id == Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02")).ShouldBe(true);
            result.Items.Any(x => x.VendorClassCompany.Id == Guid.Parse("17017f37-6683-4b2d-bf3c-7089fb313500")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorClassCompaniesAppService.GetAsync(Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorClassCompanyCreateDto
            {
                CompanyId = Guid.Parse("a479bdc2-0d84-4356-8f95-75356022fa06"),
                Exclude = true,
                Idx = 1222578963
            };

            // Act
            var serviceResult = await _vendorClassCompaniesAppService.CreateAsync(input);

            // Assert
            var result = await _vendorClassCompanyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyId.ShouldBe(Guid.Parse("a479bdc2-0d84-4356-8f95-75356022fa06"));
            result.Exclude.ShouldBe(true);
            result.Idx.ShouldBe(1222578963);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorClassCompanyUpdateDto()
            {
                CompanyId = Guid.Parse("8e44db62-8d83-4b46-9744-7db092e9729c"),
                Exclude = true,
                Idx = 1276667739
            };

            // Act
            var serviceResult = await _vendorClassCompaniesAppService.UpdateAsync(Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"), input);

            // Assert
            var result = await _vendorClassCompanyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyId.ShouldBe(Guid.Parse("8e44db62-8d83-4b46-9744-7db092e9729c"));
            result.Exclude.ShouldBe(true);
            result.Idx.ShouldBe(1276667739);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorClassCompaniesAppService.DeleteAsync(Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"));

            // Assert
            var result = await _vendorClassCompanyRepository.FindAsync(c => c.Id == Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"));

            result.ShouldBeNull();
        }
    }
}