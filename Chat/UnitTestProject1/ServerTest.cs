﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;

namespace UnitTestChat
{
    [TestClass]
    public class ServerTest
    {
        Server server = new Server();

        [TestMethod]
        public void ServerObjectShouldExist()
        {
            Assert.IsTrue(server != null);
        }

        [TestMethod]
        public void GetPortFromServerExpected8032()
        {
            Assert.AreEqual(8032, server.GetPort());
        }

        [TestMethod]
        public void SetPortFromServerExpectedTrue()
        {
            server.SetPort(666);
            Assert.AreEqual(666, server.GetPort());
        }
    }
    
}
