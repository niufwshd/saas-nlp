using GovTown.Core.Domain.RoleInfos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Data.Mapping
{
    public partial class RoleInfoMap : EntityTypeConfiguration<RoleInfo>
    {
        public RoleInfoMap()
        {
            this.ToTable("RoleInfo");
            this.HasKey(c => c.Id);
            this.Property(c => c.RoleName).IsRequired().HasMaxLength(50);
            this.Property(c => c.RolerReamrk).HasMaxLength(100);
            
        }
    }
}
