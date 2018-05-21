using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atom.ConfigCenter.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Set()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.Set("Version","1805052131",extVal: "180505_ext111111111",isAdd:true);
            var r1 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"),"180505");
            var r2 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"), "180505",desc:"配置页面后缀");
            var r3 = AtomConfigCenterService.Set(Guid.NewGuid().ToString("N"), "180505","PCC","default2","配置页面后缀",DateTime.Now.AddDays(-3),DateTime.Now.AddDays(3),"exetext");
            var r4 = AtomConfigCenterService.Set("dpl_h5_version", "123456","pppp","这是描述的啦", "分类ID", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3), "exetext");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SetCate()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.SetCate("排版类型", "schedule");
            var r1 = AtomConfigCenterService.SetCate("五休二", "schedule_5_2","schedule");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SetVal()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.SetVal("schedule", "top1");
            var r1 = AtomConfigCenterService.SetVal("schedule_5_2", "2");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Get()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.Conf["Version"];

            Assert.IsTrue(true);
        }


    }
}
