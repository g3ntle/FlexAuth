using FlexAuth.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FlexAuth.Tests
{
    [TestClass]
    public class UserManagerTest
    {
        #region Fields

        private UserManager manager;

        #endregion


        #region Methods

        [TestInitialize]
        public void Initialize()
        {
            manager = UserManager.GetInstance();
        }

        [TestMethod]
        public void ShouldHaveLogin()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            ICredentials creds = mock.Object;

            new UserBase(creds).Login();
            Assert.IsTrue(manager.HasUser, "Login should be successful");
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void ShouldntHaveLogin()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(false);

            ICredentials creds = mock.Object;
            new UserBase(creds).Login();
        }

        #endregion
    }
}
