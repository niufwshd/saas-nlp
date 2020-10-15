using GovTown.Core.Domain.MenuInfos;
using GovTown.Core.Domain.RoleInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Core.Domain.PermissionInfo
{
    [DataContract]
    public partial class PermissionInfo : BaseEntity
    {
        public PermissionInfo(){}

        public PermissionInfo(int menuId, string permission, int RoleId)
        {
            this.MenuId = menuId;
            this.Permission = permission;
            this.RoleId = RoleId;
        }

        [DataMember]
        public int MenuId { get; set; } //菜单id
        
        [DataMember]
        public string Permission { get; set; } //权限

        [DataMember]
        public int RoleId { get; set; } //角色id

        //[Column("MenuId")]
        //[ForeignKey("MenuId")]
        //public virtual MenuInfo MenuList { get; set; }

        //[Column("RoleId")]
        //[ForeignKey("RoleId")]
        //public virtual RoleInfo RoleList { get; set; }
    }
}
