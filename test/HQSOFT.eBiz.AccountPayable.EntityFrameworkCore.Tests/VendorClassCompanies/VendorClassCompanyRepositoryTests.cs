using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;
using HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompanyRepositoryTests : AccountPayableEntityFrameworkCoreTestBase
    {
        private readonly IVendorClassCompanyRepository _vendorClassCompanyRepository;

        public VendorClassCompanyRepositoryTests()
        {
            _vendorClassCompanyRepository = GetRequiredService<IVendorClassCompanyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorClassCompanyRepository.GetListAsync(
                    companyId: Guid.Parse("ef46a6be-99d7-4220-a87f-d40b267bc93c"),
                    exclude: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorClassCompanyRepository.GetCountAsync(
                    companyId: Guid.Parse("6048a48c-d7c0-4cd0-b14e-df266139e4f3"),
                    exclude: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}