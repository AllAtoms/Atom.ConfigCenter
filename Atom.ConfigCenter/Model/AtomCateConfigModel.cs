using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    public class AtomCateConfigModel
    {
        [Description("分类配置ID")]
        [Key]
        public int ConfigCateId { get; set; }

        [Description("分类名称")]
        public string CateName { get; set; }

        [Description("分类编码")]
        public string CateCode { get; set; }

        [Description("上级分类编码")]
        public string ParentCateCode { get; set; }

        [Description("扩展分类编码")]
        public string ExtCateCode { get; set; }

        [Description("添加时间")]
        public DateTime AddTime { get; set; }

        [Description("是否可用")]
        public bool Enable { get; set; }

        public List<AtomCateConfigModel>  CateChildren { get; set; }

        

    }
}
