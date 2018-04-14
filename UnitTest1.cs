using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace pillar_kata
{
    [TestClass]
    public class UnitTest1
    {
        VendingMachine vendingMachine;
        [TestInitialize]
        public void TestSetup(){
            vendingMachine = new VendingMachine();
        }
        [TestMethod]
        public void RejectsPennies()
        {
            
            vendingMachine.AddCoin("Penny");

            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Penny");
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void AcceptsQuartersNickelsAndDimesAndDoesNotReturnThem(){
            
            List<string> ExpectedResult = new List<string>();
            vendingMachine.AddCoin("Quarter");
            Assert.AreEqual("CREDIT: 25",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Dime");
            Assert.AreEqual("CREDIT: 35",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
            vendingMachine.AddCoin("Nickel");
            Assert.AreEqual("CREDIT: 40",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void ReturnsCoins(){
            
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
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            Assert.AreEqual(0,vendingMachine.Credit);
        }

        [TestMethod]
        public void ReturnedCoinsAddedToCoinReturnThatAlreadyHasCoins(){
            
            vendingMachine.AddCoin("Penny");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Dime");

            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Penny");
            ExpectedResult.Add("Quarter");
            ExpectedResult.Add("Dime");

            vendingMachine.ReturnCoins();

            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void ProperlyEmptyCoinReturn(){
            
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Dime");
            vendingMachine.ReturnCoins();

            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Quarter");
            ExpectedResult.Add("Dime");

            CollectionAssert.AreEqual(ExpectedResult, vendingMachine.EmptyCoinReturn());
            CollectionAssert.AreEqual(new List<string>(),vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void AttemptPurchaseWithInsufficientCredit(){
            vendingMachine.AddCoin("Quarter");
            vendingMachine.Buy("Chips");

            Assert.AreEqual("PRICE: 50",vendingMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 25",vendingMachine.CheckDisplay());

            vendingMachine.Buy("Cola");
            Assert.AreEqual("PRICE: 100",vendingMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 25",vendingMachine.CheckDisplay());

            vendingMachine.Buy("Candy");
            Assert.AreEqual("PRICE: 65",vendingMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 25",vendingMachine.CheckDisplay());

        }
    }
}
