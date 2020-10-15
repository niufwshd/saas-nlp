using GovTown.Core.Domain.Directory;
using GovTown.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Countries
{
    public class CountryService : ICountryService
    {
        public void Add(Country country)
        {
            using (var ctx = new SmartObjectContext())
            {
                country.Name = "China";
                country.AllowsBilling = true;
                country.AllowsShipping = true;
                country.LimitedToStores = false;
                country.NumericIsoCode = 1;
                country.Published = true;
                country.ThreeLetterIsoCode = "a";
                ctx.Countries.Add(country);
                var id = ctx.SaveChanges();
            }
        }
    }
}
