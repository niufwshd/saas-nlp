using GovTown.Core.Domain.UserInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Core.Domain.DeptInfos
{
    [DataContract]
    public partial class DeptInfo : BaseEntity
    {

       
        [DataMember]
        public string DeptName { get; set; }


        //public virtual UserInfo UserList { get; set; }
        //public List<UserInfo> UserList { get; set; }
    }
}
