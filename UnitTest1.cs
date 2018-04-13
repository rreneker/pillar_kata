using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

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

            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Penny");
            Assert.AreEqual("INSERT COIN",vendingMachine.Display);
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void AcceptsQuartersNickelsAndDimesAndDoesNotReturnThem(){
            VendingMachine vendingMachine = new VendingMachine();
            List<string> ExpectedResult = new List<string>();
            vendingMachine.AddCoin("Quarter");
            Assert.AreEqual("CREDIT: 25",vendingMachine.Display);
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Dime");
            Assert.AreEqual("CREDIT: 35",vendingMachine.Display);
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Nickel");
            Assert.AreEqual("CREDIT: 40",vendingMachine.Display);
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void ReturnsCoins(){
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Dime");
            vendingMachine.AddCoin("Nickel");
            vendingMachine.ReturnCoins();
            
            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Quarter");
            ExpectedResult.Add("Quarter");
            ExpectedResult.Add("Dime");
            ExpectedResult.Add("Nickel");

            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
            Assert.AreEqual("INSERT COIN",vendingMachine.Display);
            Assert.AreEqual(0,vendingMachine.Credit);
        }
    }
}
