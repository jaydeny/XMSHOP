using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XM.DALFactory;

namespace XM.UnitTest
{
    [TestClass]
    public class UserTest
    {
        internal DALCore DALUtility => DALCore.GetInstance();

        [TestMethod]
        public void UserLogin()
        {
            string UserAccount = "";
            string UserPwd = "";
            var currentUser = DALUtility.User.UserLogin(UserAccount, UserPwd);
            Assert.AreEqual(currentUser.UserAccountName, UserAccount);
        }
    }
}
