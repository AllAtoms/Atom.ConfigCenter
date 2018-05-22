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

        //TODO: 添加上下级，阻止无限递归
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

        //TODO: 添加上下级，阻止无限递归
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
                if (existCate == null) throw new Exception("配置Cate不存在");

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

        //TODO:缓存策略
        public static AtomConfigModel Get(string code)
        {
            var now = DateTime.Now;
            var result = SonFact.Cur.Top<AtomConfig, AtomConfigModel>(t => t.ConfigCode == code && t.Enable == true);

            if (result.StartTime != null && result.StartTime.Value > now) return null;
            if (result.EndTime != null && result.EndTime.Value < now) return null;

            return result;
        }

        //TODO:缓存策略
        public static AtomConfigModel Gets(string parentCode)
        {
            var now = DateTime.Now;
            var result = SonFact.Cur.Top<AtomConfig, AtomConfigModel>(t => t.ConfigCode == parentCode && t.Enable == true);
            if (result.StartTime != null && result.StartTime.Value > now) return null;
            if (result.EndTime != null && result.EndTime.Value < now) return null;
            if (result == null) return null;

            result.AtomChildren = new List<AtomConfigModel>();

            var list = SonFact.Cur.FindMany<AtomConfig, AtomConfigModel>(t => t.ParentCode == result.ConfigCode && t.Enable == true);

            foreach (var item in list)
            {
                if (item.StartTime != null && item.StartTime.Value > now) continue;
                if (item.EndTime != null && item.EndTime.Value < now) continue;
                result.AtomChildren.Add(item);
            }

            return result;
        }

        //TODO:缓存策略
        public static AtomCateConfigModel GetCate(string pCode)
        {
            var result = SonFact.Cur.Top<AtomCateConfig, AtomCateConfigModel>(t => t.CateCode == pCode && t.Enable == true);
            if (result == null) return null;

            var cates = SonFact.Cur.FindMany<AtomCateConfig, AtomCateConfigModel>(t => t.ParentCateCode == pCode && t.Enable == true);
            result.CateChildren = cates;

            return result;
        }

        //TODO:缓存策略
        public static AtomConfigValueModel GetVal(string code, int relId)
        {
            var now = DateTime.Now;
            var result = SonFact.Cur.Top<AtomConfigValue, AtomConfigValueModel>(t => t.CateCode == code && t.RelId == relId && t.Enable == true);

            //如果没有可用的配置则使用默认配置
            if (result == null || result.StartTime != null && result.StartTime.Value > now || result.EndTime != null && result.EndTime.Value < now)
                result = SonFact.Cur.Top<AtomConfigValue, AtomConfigValueModel>(t => t.CateCode == code && t.RelId == 0 && t.Enable == true);

            if (result == null) return null;
            if (result.StartTime != null && result.StartTime.Value > now) return null;
            if (result.EndTime != null && result.EndTime.Value < now) return null;

            var cate = SonFact.Cur.Find<AtomCateConfig>(result.ConfigValueId);
            result.CateName = cate.CateName;

            return result;
        }

        //TODO:缓存策略
        public static List<AtomConfigValueModel> GetVals(string pCode, int relId)
        {
            var now = DateTime.Now;
            var result = new List<AtomConfigValueModel>();

            var cate = GetCate(pCode);
            if (cate == null || cate.CateChildren == null || cate.CateChildren.Count == 0)
                return null;

            foreach (var item in cate.CateChildren)
                result.Add(GetVal(item.CateCode,relId));

            return result;
        }


    }
}
