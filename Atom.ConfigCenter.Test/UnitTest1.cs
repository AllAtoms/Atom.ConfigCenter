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

            //var r0 = AtomConfigCenterService.Set("version","1805052131",extVal: "180505_ext111111111",isAdd:true);
            var r1 = AtomConfigCenterService.Set("PCC", "180505");
            var r2 = AtomConfigCenterService.Set("verstion1", "180505","PCC", desc:"配置页面后缀");
            var r3 = AtomConfigCenterService.Set("verstion2", "180505","PCC","default2","配置页面后缀",DateTime.Now.AddDays(-3),DateTime.Now.AddDays(3),"exetext");
            var r4 = AtomConfigCenterService.Set("verstion3", "123456","pppp","这是描述的啦", "分类ID", null, DateTime.Now.AddDays(3), "exetext");
            var r5 = AtomConfigCenterService.Set("verstion4", "123456","pppp","这是描述的啦", "分类ID", DateTime.Now.AddDays(-3), null, "exetext");
            var r6 = AtomConfigCenterService.Set("verstion5", "123456","pppp","这是描述的啦", "分类ID", DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), "exetext");
            var r7 = AtomConfigCenterService.Set("verstion6", "123456","pppp","这是描述的啦", "分类ID", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2), "exetext");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SetCate()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.SetCate("排班类型配置", "schedule");
            var r1 = AtomConfigCenterService.SetCate("月排班时长", "schedule_mtime_len","schedule");
            var r11 = AtomConfigCenterService.SetCate("月排班天数", "schedule_mday_len","schedule");

            var r2 = AtomConfigCenterService.SetCate("用户配置", "user_conf");
            var r3 = AtomConfigCenterService.SetCate("用户app版本", "user_conf_appversion", "user_conf");
            var r4 = AtomConfigCenterService.SetCate("是否绑定串号", "user_conf_isbindno", "user_conf");
            var r5 = AtomConfigCenterService.SetCate("是否已同步", "user_conf_issync", "user_conf");
            var r6 = AtomConfigCenterService.SetCate("用户是否允许登录", "user_conf_allowlogin", "user_conf");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SetVal()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.SetVal("schedule_mtime_len", "160");
            var r1 = AtomConfigCenterService.SetVal("schedule_mday_len", "20");

            var r2 = AtomConfigCenterService.SetVal("user_conf_appversion", "1.2.0");
            var r3 = AtomConfigCenterService.SetVal("user_conf_isbindno", "1");
            var r4 = AtomConfigCenterService.SetVal("user_conf_issync", "0");
            var r5 = AtomConfigCenterService.SetVal("user_conf_allowlogin", "1");

            var r6 = AtomConfigCenterService.SetVal("user_conf_allowlogin", "0",12970);



            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Get()
        {
            AtomConfigCenterService.Init("conn");
             
            var r0 = AtomConfigCenterService.Conf["Version"];
            var r1 = AtomConfigCenterService.Get("e41436957ff9479e9d7609b5900889b7");
            var r2 = AtomConfigCenterService.Get("e41436957ff9479e9d7609b5900889b7");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Gets()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.Gets("PCC");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetCate()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.GetCate("schedule");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetVal()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.GetVal("user_conf_allowlogin",232);
            var r1 = AtomConfigCenterService.GetVal("user_conf_allowlogin", 12970);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetVals()
        {
            AtomConfigCenterService.Init("conn");

            var r0 = AtomConfigCenterService.GetVals("user_conf", 232);
            var r1 = AtomConfigCenterService.GetVals("user_conf", 12970);

            Assert.IsTrue(true);
        }



    }
}
