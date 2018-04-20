using System;
using System.Collections.Generic;

namespace pillar_kata
{
    public class Coin{
        public string name;
        public int value;
        public int bank;//number of coins currently in machine
        public Coin(string name,int value,int bank){
            this.name = name;
            this.value = value;
            this.bank = bank;
        }
    }

    public class VendingMachineItem{
        public string name;
        public int cost;
        public int stock;

        public VendingMachineItem(string name,int cost,int stock){
            this.name = name;
            this.cost = cost;
            this.stock = stock;
        }
    }
    public class VendingMachine{
       private string Display;
       private List<string> CoinReturn;
       private List<string> CurrentCoins;
       private List<string> PurchasedProducts;
       private List<VendingMachineItem> Inventory;
       private int Credit;

       private bool tempDisplay;

       private Coin quarters;
       private Coin dimes;
       public Coin nickels;

       private bool exactChangeNeeded;

       public VendingMachine()
       {
           Display = "INSERT COIN";
           CoinReturn = new List<string>();
           CurrentCoins = new List<string>();
           PurchasedProducts = new List<string>();
           Credit = 0;
           tempDisplay = false;
           exactChangeNeeded = false;
           Inventory = new List<VendingMachineItem>();
           Inventory.Add(new VendingMachineItem("Chips",50,10));
           Inventory.Add(new VendingMachineItem("Cola",100,10));
           Inventory.Add(new VendingMachineItem("Candy",65,10));
           nickels = new Coin("Nickel",5,20);
           dimes = new Coin("Dime",10,20);
           quarters = new Coin("Quarter",25,20);
           
       }
       public VendingMachine(List<VendingMachineItem> inventory): this(){
           this.Inventory = inventory;
       }

       public VendingMachine(List<VendingMachineItem> inventory, int quarters, int dimes, int nickels): this(){
           this.Inventory = inventory;
           this.nickels = new Coin("Nickel",5,nickels);
           this.dimes = new Coin("Dime",10,dimes);
           this.quarters = new Coin("Quarter",25,quarters);
           if(this.nickels.bank == 0 || this.dimes.bank == 0){
               Display = "EXACT CHANGE ONLY";
               exactChangeNeeded = true;
           }
           
       }

       public int GetCredit(){
           return Credit;
       }

       public void AddCoin(string coin){
           if(coin == quarters.name){
               Credit += quarters.value;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == dimes.name){
               Credit += dimes.value;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == nickels.name){
               Credit += nickels.value;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else{
               CoinReturn.Add(coin);
           }
           
       }

       public void ReturnCoins(){
           CoinReturn.AddRange(CurrentCoins);
           CurrentCoins.Clear();
           Display="INSERT COIN";
           Credit=0;
       }

       public List<string> EmptyCoinReturn(){
           List<string> Result = new List<string>();
           Result.AddRange(CoinReturn);
           CoinReturn.Clear();
           return Result;
       }

       public string CheckDisplay(){
           string ReturnValue = Display;
           if(tempDisplay == true){
               tempDisplay = false;
               if(nickels.bank == 0 || dimes.bank == 0){
                   Display = "EXACT CHANGE ONLY";
                   exactChangeNeeded = true;
               }
               else if(Credit > 0){
                   Display = "CREDIT: "+Credit.ToString();
               }
               else{
                   Display = "INSERT COIN";
               }
               
           }
           return ReturnValue;
       }
       private void PurchaseHelper(string product){
            Display = "THANK YOU";
            tempDisplay = true;
            Credit = 0;
            PurchasedProducts.Add(product);
       }
       private void MakeChange(int credit){
           
           for(int i = 0; i < CurrentCoins.Count;i++){
               if(CurrentCoins[i] == quarters.name){
                   quarters.bank++;
               }
               else if(CurrentCoins[i] == dimes.name){
                   dimes.bank++;
               }
               else if(CurrentCoins[i] == nickels.name){
                   nickels.bank++;
               }
           }
           CurrentCoins.Clear();
           while(credit != 0){
               if(credit >= 25 && quarters.bank > 0){
                   CoinReturn.Add(quarters.name);
                   credit -= quarters.value;
                   quarters.bank--;
               }
               else if(credit >= 10 && dimes.bank > 0){
                   CoinReturn.Add(dimes.name);
                   credit -= dimes.value;
                   dimes.bank--;
               }
               else if(nickels.bank > 0){
                   
                   CoinReturn.Add(nickels.name);
                   credit -= nickels.value;
                   nickels.bank--;
                   
               }
               else{
                   break;
               }
           }
           if(nickels.bank > 0 && dimes.bank > 0){
               exactChangeNeeded = false;
           }
       }
       public void Buy(string item){
           for(int i=0; i<Inventory.Count;i++){
                if(item == Inventory[i].name){
                    if(Inventory[i].stock == 0){
                        Display = "SOLD OUT";
                        tempDisplay = true;
                    }
                    else if(exactChangeNeeded == true){
                        ReturnCoins();
                    }
                    else if(Credit >= Inventory[i].cost){
                        MakeChange(Credit-Inventory[i].cost);
                        PurchaseHelper(item);
                        Inventory[i].stock--;    
                    }
                    else{
                        Display = "PRICE: "+Inventory[i].cost.ToString();
                        tempDisplay = true;
                    }
                    break;
                }
            }
       }

       public List<string> RemoveProducts(){
           List<string> ReturnProducts = new List<string>();
           ReturnProducts.AddRange(PurchasedProducts);
           PurchasedProducts.Clear();
           return ReturnProducts;
           
       }
   }
}
