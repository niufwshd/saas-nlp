using GovTown.Core.Domain.MenuInfos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Data.Mapping
{
    public partial class MenuInfoMap : EntityTypeConfiguration<MenuInfo>
    {
        public MenuInfoMap() {
            this.ToTable("MenuInfo");
            this.HasKey(c => c.Id);
            this.Property(c => c.MenuName).IsRequired().HasMaxLength(50);
            this.Property(c => c.OrderId).IsRequired();
            this.Property(c => c.ParentId).IsRequired();
            this.Property(c => c.Link).IsRequired().HasMaxLength(500);
            this.Property(c => c.Icon).HasMaxLength(500);
            this.Property(c => c.OptionCode).IsRequired().HasMaxLength(500);
        }
    }
}
