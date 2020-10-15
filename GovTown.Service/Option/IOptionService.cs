using GovTown.Core.Domain.OptionInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Option
{
    public interface IOptionService
    {
        IQueryable<OptionInfo> GetOption();
    }
}
