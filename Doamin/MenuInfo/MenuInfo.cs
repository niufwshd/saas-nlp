using GovTown.Core;
using GovTown.Core.Domain.OptionInfos;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GovTown.Core.Domain.MenuInfos
{
    [DataContract]
    public partial class MenuInfo : BaseEntity
    {
        [DataMember]
        public string MenuName { get; set; }//菜单名称
        [DataMember]
        public int OrderId { get; set; } //排序
        [DataMember]
        public int ParentId { get; set; }//父级Id
        [DataMember]
        public string Link { get; set; }//地址
        [DataMember]
        public string Icon { get; set; }//图标
        [DataMember]
        public string OptionCode { get; set; }//操作code
        //public List<OptionInfo> OptionInfo { get; set; }
    }
}
