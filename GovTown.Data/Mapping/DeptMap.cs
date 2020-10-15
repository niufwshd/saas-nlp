using GovTown.Core.Domain.DeptInfos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Data.Mapping
{
    public partial class DeptInfoMap : EntityTypeConfiguration<DeptInfo>
    {
        public DeptInfoMap()
        {
            this.ToTable("DeptInfo");
            this.HasKey(c => c.Id);
            //this.HasMany().WithRequired().HasForeignKey(c => c.Id)
            this.Property(c => c.DeptName).HasMaxLength(50);
            
            //this.HasRequired(c => c.UserList).WithRequiredDependent(c => c.DeptList);

        }
    }
}
