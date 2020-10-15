using GovTown.Core.Domain.MenuInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Core.Domain.OptionInfos
{
    [DataContract]
    public partial class OptionInfo : BaseEntity
    {
        public OptionInfo() { }

        [DataMember]
        public string OptionCode { get; set; }

        [DataMember]
        public string OptionName { get; set; }

        //[Required]
        //public MenuInfo MenuInfo { get; set; }
    }
}
