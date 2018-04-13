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
            Assert.AreEqual("Penny",vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void AcceptsQuartersNickelsAndDimesAndDoesNotReturnThem(){
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.AddCoin("Quarter");
            Assert.AreEqual("CREDIT: .25",vendingMachine.Display);
            Assert.AreEqual("",vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Dime");
            Assert.AreEqual("CREDIT: .35",vendingMachine.Display);
            Assert.AreEqual("",vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Nickel");
            Assert.AreEqual("CREDIT: .40",vendingMachine.Display);
            Assert.AreEqual("",vendingMachine.CoinReturn);
        }
    }
}
