using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ISysEnterpriseArchivesService
    {

        SysEnterpriseArchives Get();

        void Save(SysEnterpriseArchives entity);
    }
}