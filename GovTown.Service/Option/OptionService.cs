using GovTown.Core.Data;
using GovTown.Core.Domain.OptionInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Option
{
    public class OptionService:IOptionService
    {
        private readonly IRepository<OptionInfo> _optionRepo;

        public OptionService(IRepository<OptionInfo> optionRepo)
        {
            _optionRepo = optionRepo;
        }

        public IQueryable<OptionInfo> GetOption()
        {
            var OptionList = _optionRepo.Get();

            return OptionList;
        }
    }
}
