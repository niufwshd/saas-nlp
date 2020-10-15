using GovTown.Core.Domain.OptionInfos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Data.Mapping
{
    public partial class OptionInfoMap : EntityTypeConfiguration<OptionInfo>
    {
        public OptionInfoMap()
        {
            this.ToTable("OptionInfo");
            this.HasKey(c => c.Id);
            this.Property(c => c.OptionName).IsRequired().HasMaxLength(50);
            this.Property(c => c.OptionCode).IsRequired().HasMaxLength(50);
            //this.HasMany(d =>)
        }
    }
}
