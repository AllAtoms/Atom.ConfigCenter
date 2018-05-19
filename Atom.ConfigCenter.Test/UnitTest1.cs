using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atom.ConfigCenter.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.Set("Version","1805052131",extVal: "180505_ext111111111",isAdd:true);
            var r1 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"),"180505");
            var r2 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"), "180505",desc:"配置页面后缀");
            var r3 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"), "180505","PCC","default2","配置页面后缀",DateTime.Now.AddDays(-3),DateTime.Now.AddDays(3),"exetext");
            var r4 = AtomConfigCenterService.Set("dpl_h5_version", "123456","pppp","这是描述的啦", "分类ID", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3), "exetext");

            Assert.IsTrue(true);
        }
    }
}
