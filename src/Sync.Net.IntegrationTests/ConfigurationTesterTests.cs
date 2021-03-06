﻿using Amazon.S3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sync.Net.Configuration;

namespace Sync.Net.IntegrationTests
{
    [TestClass]
    public class ConfigurationTesterTests
    {
        [TestMethod]
        public void ReturnInvalidResultIfCredentialsAreInvalid()
        {
            ConfigurationTester tester = new ConfigurationTester();
            var result = tester.Test(new ProcessorConfiguration
            {
                CredentialsType = CredentialsType.Basic,
                KeySecret = "xyz",
                KeyId = "abc"
            });
            
            Assert.IsFalse(result.TestPassed);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
        }
    }
}
