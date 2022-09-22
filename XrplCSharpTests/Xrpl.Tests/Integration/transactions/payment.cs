using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xrpl.Client.Models.Common;
using Xrpl.Client.Models.Ledger;
using Xrpl.Client.Models.Methods;
using Xrpl.Client.Models.Transactions;
using Xrpl.Tests.Client.Tests;
using Xrpl.XrplWallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/payment.ts

namespace Xrpl.Tests.Client.Tests.Integration
{
    [TestClass]
    public class TestIPayment
    {
        // private static int Timeout = 20;
        public TestContext TestContext { get; set; }
        public static SetupIntegration runner;

        [ClassInitialize]
        public static async Task MyClassInitializeAsync(TestContext testContext)
        {
            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
        }

        [TestMethod]
        public async Task TestRequestMethod()
        {

            Wallet wallet2 = await Utils.GenerateFundedWallet(runner.client);
            Payment setupTx = new Payment
            {
                Account = runner.wallet.ClassicAddress,
                Destination = wallet2.ClassicAddress,
                Amount = new Currency { ValueAsXrp = 10000 }
            };
            Debug.WriteLine(setupTx.ToJson());
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());
            await Utils.TestTransaction(runner.client, setupJson, runner.wallet);
        }
    }
}