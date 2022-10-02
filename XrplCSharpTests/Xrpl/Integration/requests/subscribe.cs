using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Asn1.Ocsp;
using Xrpl;
using Xrpl.Models.Methods;
using XrplTests.Xrpl.ClientLib;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/subscribe.ts

namespace XrplTests.Xrpl.ClientLib.Integration
{
    [TestClass]
    public class TestISubscribe
    {
        // private static int Timeout = 20;
        public TestContext TestContext { get; set; }
        public static SetupIntegration runner;

        [ClassInitialize]
        public static async Task MyClassInitializeAsync(TestContext testContext)
        {
            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
        }
    }
}