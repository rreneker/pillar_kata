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

        [TestMethod]
        public void AttemptPurchaseWithNoCredit(){
            vendingMachine.Buy("Chips");
            Assert.AreEqual("PRICE: 50",vendingMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
        }

        [TestMethod]
        public void AttemptPurchaseWithSufficientCredit(){
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");

            vendingMachine.Buy("Cola");

            Assert.AreEqual("THANK YOU",vendingMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());

            List<string> ExpectedProducts = new List<string>();
            ExpectedProducts.Add("Cola");

            CollectionAssert.AreEqual(ExpectedProducts,vendingMachine.RemoveProducts());
            Assert.AreEqual(0,vendingMachine.Credit);

            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.Buy("Chips");
            Assert.AreEqual("THANK YOU",vendingMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            ExpectedProducts.Clear();
            ExpectedProducts.Add("Chips");
            CollectionAssert.AreEqual(ExpectedProducts,vendingMachine.RemoveProducts());

            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Dime");
            vendingMachine.AddCoin("Nickel");
            vendingMachine.Buy("Candy");
            Assert.AreEqual("THANK YOU",vendingMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            ExpectedProducts.Clear();
            ExpectedProducts.Add("Candy");
            CollectionAssert.AreEqual(ExpectedProducts,vendingMachine.RemoveProducts());


        }
        [TestMethod]
        public void GetCorrectChangeAfterSuccessfulPurchase(){
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.Buy("Candy");

            List<string> ExpectedChange = new List<string>();
            ExpectedChange.Add("Quarter");
            ExpectedChange.Add("Dime");
            CollectionAssert.AreEqual(ExpectedChange,vendingMachine.EmptyCoinReturn());
            ExpectedChange.Clear();

            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.AddCoin("Quarter");
            vendingMachine.Buy("Chips");
            ExpectedChange.Add("Quarter");
            ExpectedChange.Add("Quarter");
            CollectionAssert.AreEqual(ExpectedChange,vendingMachine.EmptyCoinReturn());

        }

        [TestMethod]
        public void SoldOut(){
            VendingMachine vendMachine = new VendingMachine(0,0,0);

            vendMachine.Buy("Cola");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());

            vendMachine.Buy("Candy");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());

            vendMachine.Buy("Chips");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
        }
    }
}
