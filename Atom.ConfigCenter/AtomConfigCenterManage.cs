using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter
{
    static internal class AtomConfigCenterManage
    {
        public static readonly object locker = new object();

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
                var exist = SonFact.Cur.Top<AtomConfigValue>(t => t.CateCode == acv.CateCode && t.RelId == acv.RelId);
                if (exist != null)
                {

                }

                return SonFact.Cur.Insert(acv);
            }
        }


    }
}
