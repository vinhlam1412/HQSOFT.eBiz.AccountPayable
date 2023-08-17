using HQSOFT.eBiz.AccountPayable.VendorClasses;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.AccountPayable.VendorClassCompanies;

namespace HQSOFT.eBiz.AccountPayable.VendorClassCompanies
{
    public class VendorClassCompaniesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVendorClassCompanyRepository _vendorClassCompanyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly VendorClassesDataSeedContributor _vendorClassesDataSeedContributor;

        public VendorClassCompaniesDataSeedContributor(IVendorClassCompanyRepository vendorClassCompanyRepository, IUnitOfWorkManager unitOfWorkManager, VendorClassesDataSeedContributor vendorClassesDataSeedContributor)
        {
            _vendorClassCompanyRepository = vendorClassCompanyRepository;
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

            await _vendorClassCompanyRepository.InsertAsync(new VendorClassCompany
            (
                id: Guid.Parse("562e615b-a925-4278-a24a-19e5481e1d02"),
                companyId: Guid.Parse("ef46a6be-99d7-4220-a87f-d40b267bc93c"),
                exclude: true,
                idx: 868598131,
                vendorClassId: null
            ));

            await _vendorClassCompanyRepository.InsertAsync(new VendorClassCompany
            (
                id: Guid.Parse("17017f37-6683-4b2d-bf3c-7089fb313500"),
                companyId: Guid.Parse("6048a48c-d7c0-4cd0-b14e-df266139e4f3"),
                exclude: true,
                idx: 1353876792,
                vendorClassId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}