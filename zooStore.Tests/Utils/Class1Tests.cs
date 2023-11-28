using Microsoft.VisualStudio.TestTools.UnitTesting;
using zoostore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoostore.Utils.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void GetUserRoleTest()
        {
            string expected = "1";
            zoostore.Utils.Class1 c = new Class1();
            object actual = c.GetUserRole();
            Assert.AreEqual(expected, actual, "{0} should be {1}");
        }
    }
}