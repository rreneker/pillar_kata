using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pillar_kata
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RejectsPennies()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.AddCoin("Penny");

            Assert.AreEqual("INSERT COIN",vendingMachine.Display);
        }
    }
}
