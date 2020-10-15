using GovTown.Core.Domain.PermissionInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Data.Mapping.Permission
{
    public partial class PermissionMap : EntityTypeConfiguration<PermissionInfo>
    {
        public PermissionMap()
        {
            this.ToTable("PermissionInfo");
            this.HasKey(c => c.Id);
            this.Property(c => c.MenuId).IsRequired();
            this.Property(c => c.RoleId).IsRequired();
            this.Property(c => c.Permission).IsRequired().HasMaxLength(200);
        }
    }
}
