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
            manager.CleanUp();
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            var user = new UserBase() { Credentials = mock.Object };
            user.SignIn();

            Assert.IsTrue(manager.HasUser, "Login should be successful");
        }

        [TestMethod]
        [ExpectedException(typeof(SignInException))]
        public void SignInShouldFail()
        {
            manager.CleanUp();
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(false);

            var user = new UserBase() { Credentials = mock.Object };
            user.SignIn();
        }

        [TestMethod]
        [ExpectedException(typeof(SignInException))]
        public void SignInShouldBeRedundant()
        {
            manager.CleanUp();
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
            manager.CleanUp();
            var mock = new Mock<ICredentials>();

            mock.Setup(m => m.Check())
                .Returns(true);

            var u = new UserBase() { Credentials = mock.Object };
            u.SignedIn += (sender, e) =>
            {
                (sender as UserBase).Nodes = repo;
            };

            u.SignIn();

            Assert.IsTrue(manager.HasUser, "User should be signed in");
            Assert.IsTrue(manager.HasPermission("One"), "User should have the \"One\" node");
            Assert.IsTrue(manager.HasPermission("Two"), "User should have the \"Two\" node");

            u.SignOut();
            Assert.IsFalse(manager.HasUser, "User should be signed out");
            Assert.IsFalse(manager.HasPermission("One"), "User shouldn\'t have the \"One\" node");
            Assert.IsFalse(manager.HasPermission("Two"), "User shouldn\'t have the \"Two\" node");
        }

        #endregion
    }
}
