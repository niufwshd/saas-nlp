using GovTown.Core.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Countries
{
   public interface ICountryService
    {
        void Add(Country country);
    }
}
