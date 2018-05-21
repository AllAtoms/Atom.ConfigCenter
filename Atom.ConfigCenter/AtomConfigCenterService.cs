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

        public static AtomConfigCenterService Conf = new AtomConfigCenterService();

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

        public static AtomConfigModel Get(string code)
        {
            var res = AtomConfigCenterManage.Get(code);
            if (res == null) return null;
            return res;
        }

        public static AtomConfigModel Gets(string code)
        {
            var res = AtomConfigCenterManage.Gets(code);
            if (res == null) return null;
            return res;
        }

    }
}
