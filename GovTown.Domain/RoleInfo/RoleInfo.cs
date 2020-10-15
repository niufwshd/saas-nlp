using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Core.Domain.RoleInfos
{
    [DataContract]
    public partial class RoleInfo:BaseEntity
    {
        public RoleInfo() {
            Page = 0;
            PageSize = 20;
        }

        public RoleInfo(string roleName, string roleReamrk)
        {
            this.RoleName = roleName;
            this.RolerReamrk = roleReamrk;
        }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string RolerReamrk { get; set; }


        [NotMapped]
        public int Page { get; set; } //当前页码
        [NotMapped]
        public int PageSize { get; set; } //页大小
    }
}
