using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace pillar_kata
{
    [TestClass]
    public class UnitTest1
    {
        VendingMachine vendingMachine;
        List<VendingMachineItem> Inventory;
        [TestInitialize]
        public void TestSetup(){
            vendingMachine = new VendingMachine();
            List<VendingMachineItem> Inventory = new List<VendingMachineItem>();
            Inventory.Add(new VendingMachineItem("Chips",50,10));
            Inventory.Add(new VendingMachineItem("Cola",100,10));
            Inventory.Add(new VendingMachineItem("Candy",65,10));
        }
        [TestMethod]
        public void RejectsPenniesAndSlugs()
        {
            
            vendingMachine.AddCoin("Penny");

            List<string> ExpectedResult = new List<string>();
            ExpectedResult.Add("Penny");
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());

            vendingMachine.AddCoin("Slug");

            ExpectedResult.Clear();
            ExpectedResult.Add("Slug");
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
        }

        [TestMethod]
        public void AcceptsQuartersNickelsAndDimesAndDoesNotReturnThem(){
            
            List<string> ExpectedResult = new List<string>();
            vendingMachine.AddCoin("Quarter");
            Assert.AreEqual("CREDIT: 25",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
            vendingMachine.AddCoin("Dime");
            Assert.AreEqual("CREDIT: 35",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
            vendingMachine.AddCoin("Nickel");
            Assert.AreEqual("CREDIT: 40",vendingMachine.CheckDisplay());
            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
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

            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
            Assert.AreEqual("INSERT COIN",vendingMachine.CheckDisplay());
            Assert.AreEqual(0,vendingMachine.GetCredit());
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

            CollectionAssert.AreEqual(ExpectedResult,vendingMachine.EmptyCoinReturn());
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
            CollectionAssert.AreEqual(new List<string>(),vendingMachine.EmptyCoinReturn());
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
            Assert.AreEqual(0,vendingMachine.GetCredit());

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
        public void CheckIfSoldOut(){
            List<VendingMachineItem> EmptyInventory = new List<VendingMachineItem>();
            EmptyInventory.Add(new VendingMachineItem("Chips",50,0));
            EmptyInventory.Add(new VendingMachineItem("Cola",100,0));
            EmptyInventory.Add(new VendingMachineItem("Candy",65,0));
            VendingMachine vendMachine = new VendingMachine(EmptyInventory);

            vendMachine.Buy("Cola");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
            CollectionAssert.AreEqual(new List<string>(), vendMachine.RemoveProducts());

            vendMachine.Buy("Candy");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
            CollectionAssert.AreEqual(new List<string>(), vendMachine.RemoveProducts());

            vendMachine.Buy("Chips");
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
            CollectionAssert.AreEqual(new List<string>(), vendMachine.RemoveProducts());
        }

        [TestMethod]
        public void SellOutOfItems(){
            List<VendingMachineItem> NearlyEmptyInventory = new List<VendingMachineItem>();
            NearlyEmptyInventory.Add(new VendingMachineItem("Chips",50,2));
            NearlyEmptyInventory.Add(new VendingMachineItem("Cola",100,2));
            NearlyEmptyInventory.Add(new VendingMachineItem("Candy",65,2));
            VendingMachine vendMachine = new VendingMachine(NearlyEmptyInventory);
            for(int i =0; i < 3; i++){
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.Buy("Cola");
            }
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 100",vendMachine.CheckDisplay());

            for(int j =0; j < 3; j++){
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.Buy("Candy");
            }
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 75",vendMachine.CheckDisplay());

            for(int k =0; k < 3; k++){
                vendMachine.AddCoin("Quarter");
                vendMachine.AddCoin("Quarter");
                vendMachine.Buy("Chips");
            }
            Assert.AreEqual("SOLD OUT",vendMachine.CheckDisplay());
            Assert.AreEqual("CREDIT: 50",vendMachine.CheckDisplay());
        }

        [TestMethod]
        public void CheckIfExactChangeNeeded(){
            VendingMachine vendMachine = new VendingMachine(Inventory,0,0,0);
            Assert.AreEqual("EXACT CHANGE ONLY",vendMachine.CheckDisplay());
        }

        [TestMethod]
        public void CheckIfMachineRunsOutOfChangeNickels(){
            
            //Console.WriteLine(Inventory.Count);
            List<VendingMachineItem> Inventory = new List<VendingMachineItem>();
            Inventory.Add(new VendingMachineItem("Chips",50,10));
            Inventory.Add(new VendingMachineItem("Cola",100,10));
            Inventory.Add(new VendingMachineItem("Candy",65,10));
            VendingMachine vendMachine = new VendingMachine(Inventory,1,1,1);
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
            vendMachine.AddCoin("Quarter");
            vendMachine.AddCoin("Dime");
            vendMachine.AddCoin("Dime");
            vendMachine.AddCoin("Dime");
            vendMachine.Buy("Chips");
            string dummy = vendMachine.CheckDisplay();
            
            Assert.AreEqual("EXACT CHANGE ONLY",vendMachine.CheckDisplay());
            
            
        }

        [TestMethod]
        public void CheckIfMachineRunsOutOfChangeDimes(){
            List<VendingMachineItem> Inventory = new List<VendingMachineItem>();
            Inventory.Add(new VendingMachineItem("Chips",50,10));
            Inventory.Add(new VendingMachineItem("Cola",100,10));
            Inventory.Add(new VendingMachineItem("Candy",65,10));
            VendingMachine vendMachine = new VendingMachine(Inventory,1,1,1);
            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
            vendMachine.AddCoin("Quarter");
            vendMachine.AddCoin("Quarter");
            vendMachine.AddCoin("Quarter");
            vendMachine.Buy("Candy");
            string dummy = vendMachine.CheckDisplay();
            
            Assert.AreEqual("EXACT CHANGE ONLY",vendMachine.CheckDisplay());
        }

        [TestMethod]
        public void CheckIfMachineGetsOutOfExactChangeMode(){
            List<VendingMachineItem> Inventory = new List<VendingMachineItem>();
            Inventory.Add(new VendingMachineItem("Chips",50,10));
            Inventory.Add(new VendingMachineItem("Cola",100,10));
            Inventory.Add(new VendingMachineItem("Candy",65,10));
            VendingMachine vendMachine = new VendingMachine(Inventory,1,0,0);
            vendMachine.AddCoin("Quarter");
            vendMachine.AddCoin("Quarter");
            vendMachine.AddCoin("Dime");
            vendMachine.AddCoin("Nickel");
            vendMachine.Buy("Candy");
            string dummy = vendMachine.CheckDisplay();

            Assert.AreEqual("INSERT COIN",vendMachine.CheckDisplay());
        }

        [TestMethod]
        public void EnforceExactChangeState(){
            List<VendingMachineItem> Inventory = new List<VendingMachineItem>();
            Inventory.Add(new VendingMachineItem("Chips",50,10));
            Inventory.Add(new VendingMachineItem("Cola",100,10));
            Inventory.Add(new VendingMachineItem("Candy",65,10));
            VendingMachine machine = new VendingMachine(Inventory,0,0,0);
            machine.AddCoin("Quarter");
            machine.AddCoin("Quarter");
            machine.AddCoin("Quarter");
            machine.Buy("Candy");

            List<string> ExpectedChange = new List<string>();
            ExpectedChange.Add("Quarter");
            ExpectedChange.Add("Quarter");
            ExpectedChange.Add("Quarter");
            

            CollectionAssert.AreEqual(new List<string>(), machine.RemoveProducts());
            CollectionAssert.AreEqual(ExpectedChange,machine.EmptyCoinReturn());

        }

        
    }
}
