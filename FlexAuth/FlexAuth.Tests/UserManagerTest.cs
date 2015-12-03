using FlexAuth.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexAuth.Tests
{
    [TestClass]
    public class UserManagerTest
    {
        #region Fields

        private UserManager manager;
        private INodeRepository repo;

        #endregion


        #region Methods

        [TestInitialize]
        public void Initialize()
        {
            manager = UserManager.GetInstance();

            repo = new NodeRepositoryBase();
            var nodes = new HashSet<string>();
            nodes.Add("One");
            nodes.Add("Two");
            repo.Nodes = nodes;
        }

        [TestMethod]
        public void SignInShouldSucceed()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            var user = new UserBase() { Credentials = mock.Object };
            user.SignIn();

            Assert.IsTrue(manager.HasUser, "Login should be successful");
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void SignInShouldFail()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(false);

            var user = new UserBase() { Credentials = mock.Object };
            user.SignIn();
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void SignInShouldBeRedundant()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            var user = new UserBase() { Credentials = mock.Object };
            user.SignIn();
            user.SignIn();
        }

        [TestMethod]
        public void SignInAndOutShouldSucceed()
        {
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            var u = new UserBase() { Credentials = mock.Object };
            u.SignedIn += (sender, e) =>
            {
                (sender as UserBase).Nodes = repo;
            };

            u.SignIn();

            Assert.IsTrue(manager.HasPermission("One"), "User should have the \"One\" node");
            Assert.IsTrue(manager.HasPermission("Two"), "User should have the \"Two\" node");

            u.SignOut();
            Assert.IsFalse(manager.HasPermission("One"), "User shouldn\'t have the \"One\" node");
            Assert.IsFalse(manager.HasPermission("Two"), "User shouldn\'t have the \"Two\" node");
        }

        #endregion
    }
}
