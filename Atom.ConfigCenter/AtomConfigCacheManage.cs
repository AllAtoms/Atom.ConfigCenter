using Atom.ConfigCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Atom.ConfigCenter
{
    internal class AtomConfigCacheManage
    {
        public static void AddCache<T>(string key, T acc)
        {
            var cache = HttpRuntime.Cache;
            var timeSpan = new TimeSpan(0, 5, 0);
            cache.Insert(key, acc, null, DateTime.MaxValue, timeSpan);
        }

        public static T GetIfExist<T>(string key) where T : new()
        {
            var cache = HttpRuntime.Cache;
            if (cache[key] == null) return default(T);
            return (T)cache[key];
        }

    }
}
