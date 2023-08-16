﻿using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HQSOFT.eBiz.AccountPayable.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
