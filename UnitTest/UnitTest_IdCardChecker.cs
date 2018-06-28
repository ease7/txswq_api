using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Txs.Core;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_IdCardChecker
    {
        [TestMethod]
        public void Test_Check()
        {
            bool result1 = IdCardChecker.Check("110101199003074290");
            bool result2 = IdCardChecker.Check("110101199003074290");
            bool result3 = IdCardChecker.Check("110101199003074291");
        }
    }
}
