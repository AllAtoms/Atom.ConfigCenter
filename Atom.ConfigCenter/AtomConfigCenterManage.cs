using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter
{
    internal static class AtomConfigCenterManage
    {
        private static readonly object locker = new object();



        public static bool CheckOrCreateDb()
        {
            var resB = SonFact.Cur.CreateTable<AtomConfig>();
            var resA = SonFact.Cur.CreateTable<AtomCateConfig>();
            var resC = SonFact.Cur.CreateTable<AtomConfigValue>();
            return resA && resB && resB;
        }

        public static long Set(AtomConfig ac, bool isAdd)
        {
            lock (locker)
            {
                var exist = SonFact.Cur.Top<AtomConfig>(t => t.ConfigCode == ac.ConfigCode);
                if (exist != null && isAdd) throw new Exception("code 已经存在");
                if (exist != null && !isAdd)
                {
                    ac.ConfigId = exist.ConfigId;
                    var result = SonFact.Cur.Update(ac);
                    return Convert.ToInt64(result);
                }

                return SonFact.Cur.Insert(ac);
            }
        }

        public static long SetCate(AtomCateConfig acc, bool isAdd)
        {
            lock (locker)
            {
                var exist = SonFact.Cur.Top<AtomCateConfig>(t => t.CateCode == acc.CateCode);
                if (exist != null && isAdd) throw new Exception("cate code 已经存在");
                if (exist != null && !isAdd)
                {
                    acc.ConfigCateId = exist.ConfigCateId;
                    var result = SonFact.Cur.Update(acc);
                    return Convert.ToInt64(result);
                }

                return SonFact.Cur.Insert(acc);
            }
        }

        public static long SetVal(AtomConfigValue acv, bool isAdd)
        {
            lock (locker)
            {
                var existCate = SonFact.Cur.Top<AtomCateConfig>(t => t.CateCode == acv.CateCode);
                if(existCate==null) throw new Exception("配置Cate不存在");

                var exist = SonFact.Cur.Top<AtomConfigValue>(t => t.CateCode == acv.CateCode && t.RelId == acv.RelId);
                if (exist != null && isAdd) throw new Exception("配置已经存在");
                if (exist != null && !isAdd)
                {
                    acv.ConfigValueId = exist.ConfigValueId;
                    var result = SonFact.Cur.Update(acv);
                    return Convert.ToInt64(result);
                }

                return SonFact.Cur.Insert(acv);
            }
        }


        public static AtomConfigModel Get(string code)
        {
            var now = DateTime.Now;
            var result = SonFact.Cur.Top<AtomConfig, AtomConfigModel>(t=>t.ConfigCode == code && t.Enable==true && now >= t.StartTime && now <= t.EndTime);
            return result;
        }

    }
}
