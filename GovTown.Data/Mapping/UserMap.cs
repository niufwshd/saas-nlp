using System.Data.Entity.ModelConfiguration;
using GovTown.Core.Domain.Directory;
using GovTown.Core.Domain.UserInfos;
using GovTown.Core.Domain.DeptInfos;

namespace GovTown.Data.Mapping.User
{
    public partial class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            this.ToTable("UserInfo");
            this.HasKey(c =>c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(50);
            this.Property(c =>c.UserName).IsRequired().HasMaxLength(50);
            this.Property(c =>c.Password).IsRequired().HasMaxLength(500);
            //this.Property(c => c.Job).IsRequired().HasMaxLength(50);
            //this.Property(c => c.Phone).IsRequired().HasMaxLength(50);
            //this.Property(c => c.Telephone).IsRequired().HasMaxLength(50);
            //this.Property(c => c.Email).IsRequired().HasMaxLength(50);
            //this.HasRequired(c => c.DeptList.DeptId).WithRequiredDependent(c => c.User);


        }
    }
}