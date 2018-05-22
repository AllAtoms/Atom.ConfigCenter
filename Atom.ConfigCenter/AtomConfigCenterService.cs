using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter
{
    public class AtomConfigCenterService
    {
        public static void Init(string dbConnStr)
        {
            SonFact.init(dbConnStr);
            AtomConfigCenterManage.CheckOrCreateDb();

        }

        public string this[string code]
        {
            get
            {
                var res = AtomConfigCenterManage.Get(code);
                if (res == null) return string.Empty;
                return res.ConfigValue;
            }
        }

        /// <summary>
        /// 获取全局配置
        /// </summary>
        public static AtomConfigCenterService Conf = new AtomConfigCenterService();

        /// <summary>
        /// 添加全局配置
        /// </summary>
        /// <param name="code">配置编码</param>
        /// <param name="value">配置值</param>
        /// <param name="pCode">上级编码</param>
        /// <param name="desc">配置描述</param>
        /// <param name="cType">配置类型、分类、环境</param>
        /// <param name="st">生效开始时间</param>
        /// <param name="et">生效结束时间</param>
        /// <param name="extVal">扩展值</param>
        /// <param name="isAdd">是否是添加操作</param>
        /// <returns></returns>
        public static long Set(string code, string value, string pCode = null, string desc = null, string cType = null, DateTime? st = null, DateTime? et = null, string extVal = null, bool isAdd = false)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("配置编码不可为空");
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("配置值不可为空");

            var ac = new AtomConfig
            {
                ConfigCode = code,
                ConfigValue = value,
                ParentCode = pCode,
                ConfigDesc = desc,
                ConfigType = string.IsNullOrWhiteSpace(cType) ? "default" : cType,
                ExtValue = extVal,
                StartTime = st,
                EndTime = et,
                AddTime = DateTime.Now,
                Enable = true
            };
            return AtomConfigCenterManage.Set(ac, isAdd);
        }

        /// <summary>
        /// 添加配置字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="code">字段编码</param>
        /// <param name="pCode">上级编码</param>
        /// <param name="extCode">扩展编码</param>
        /// <param name="isAdd">是否添加操作</param>
        /// <returns></returns>
        public static long SetCate(string name, string code, string pCode = null, string extCode = null, bool isAdd = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("名称不可为空");
            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("配置值不可为空");

            var acc = new AtomCateConfig
            {
                CateName = name,
                CateCode = code,
                ParentCateCode = pCode,
                AddTime = DateTime.Now,
                ExtCateCode = extCode,
                Enable = true
            };
            return AtomConfigCenterManage.SetCate(acc, isAdd);
        }

        /// <summary>
        /// 添加配置值
        /// </summary>
        /// <param name="cateCode">配置字段编码</param>
        /// <param name="value">字段配置值</param>
        /// <param name="relId">关联ID</param>
        /// <param name="extVal">扩展值</param>
        /// <param name="st">生效开始时间</param>
        /// <param name="et">生效结束时间</param>
        /// <param name="isAdd">是否是添加操作</param>
        /// <returns></returns>
        public static long SetVal(string cateCode, string value, int relId = 0, string extVal = null, DateTime? st = null, DateTime? et = null, bool isAdd = false)
        {
            if (string.IsNullOrWhiteSpace(cateCode))
                throw new Exception("编码不可为空");
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("配置值不可为空");

            var acv = new AtomConfigValue
            {
                CateCode = cateCode,
                AddTime = DateTime.Now,
                CateValue = value,
                Enable = true,
                RelId = relId,
                StartTime = st,
                EndTime = et,
                ExtValue = extVal
            };

            return AtomConfigCenterManage.SetVal(acv, isAdd);
        }

        /// <summary>
        /// 获取全局配置
        /// </summary>
        /// <param name="code">全局配置编码</param>
        /// <returns></returns>
        public static AtomConfigModel Get(string code)
        {
            var res = AtomConfigCenterManage.Get(code);
            if (res == null) return null;
            return res;
        }

        /// <summary>
        /// 获取当前配置以及下一层子配置
        /// </summary>
        /// <param name="code">配置编码</param>
        /// <returns></returns>
        public static AtomConfigModel Gets(string code)
        {
            var res = AtomConfigCenterManage.Gets(code);
            if (res == null) return null;
            return res;
        }

        /// <summary>
        /// 根据配置字段编码获取子配置字段
        /// </summary>
        /// <param name="pCode">上级配置编码</param>
        /// <returns></returns>
        public static AtomCateConfigModel GetCate(string pCode)
        {
            var result = AtomConfigCenterManage.GetCate(pCode);
            return result;
        }

        /// <summary>
        /// 获取字段值：没有则使用默认值，默认值relId为0
        /// </summary>
        /// <param name="code">字段编码</param>
        /// <param name="relId">关系ID</param>
        /// <returns></returns>
        public static AtomConfigValueModel GetVal(string code, int relId)
        {
            var result = AtomConfigCenterManage.GetVal(code, relId);
            return result;
        }

        /// <summary>
        /// 获取子配置字段值组：没有则使用默认值，默认值relId为0
        /// </summary>
        /// <param name="pCode">上级字段编码</param>
        /// <param name="relId">关系ID</param>
        public static List<AtomConfigValueModel> GetVals(string pCode, int relId)
        {
            var result = AtomConfigCenterManage.GetVals(pCode, relId);
            return result;
        }

        }
}
