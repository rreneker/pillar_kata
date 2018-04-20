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
    public class VendingMachine{
       private string Display;
       private List<string> CoinReturn;
       private List<string> CurrentCoins;
       private List<string> PurchasedProducts;
       private int Credit;

       private bool tempDisplay;

       private int colaStock;
       private int candyStock;
       private int chipsStock;
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
           colaStock = 10;
           candyStock = 10;
           chipsStock = 10;
           nickels = new Coin("Nickel",5,20);
           dimes = new Coin("Dime",10,20);
           quarters = new Coin("Quarter",25,20);
           
       }
       public VendingMachine(int cola, int chips, int candy): this(){
           colaStock = cola;
           candyStock = candy;
           chipsStock = chips;
       }

       public VendingMachine(int cola, int chips, int candy, int quarters, int dimes, int nickels): this(){
           colaStock = cola;
           candyStock = candy;
           chipsStock = chips;
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
           if(coin == "Quarter"){
               Credit += quarters.value;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == "Dime"){
               Credit += dimes.value;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == "Nickel"){
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
               if(CurrentCoins[i] == "Quarter"){
                   quarters.bank++;
               }
               else if(CurrentCoins[i] == "Dime"){
                   dimes.bank++;
               }
               else if(CurrentCoins[i] == "Nickel"){
                   nickels.bank++;
               }
           }
           CurrentCoins.Clear();
           while(credit != 0){
               if(credit >= 25 && quarters.bank > 0){
                   CoinReturn.Add("Quarter");
                   credit -= quarters.value;
                   quarters.bank--;
               }
               else if(credit >= 10 && dimes.bank > 0){
                   CoinReturn.Add("Dime");
                   credit -= dimes.value;
                   dimes.bank--;
               }
               else if(nickels.bank > 0){
                   
                   CoinReturn.Add("Nickel");
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
           if(item == "Chips"){
               if(chipsStock == 0){
                   Display = "SOLD OUT";
                   tempDisplay = true;
               }
               else if(exactChangeNeeded == true){
                   ReturnCoins();
               }
               else if(Credit >= 50){
                   MakeChange(Credit-50);
                   PurchaseHelper(item);
                   chipsStock--;    
               }
               else{
                    Display = "PRICE: 50";
                    tempDisplay = true;
               }
              
           }
           else if(item == "Cola"){
               if(colaStock == 0){
                   Display = "SOLD OUT";
                   tempDisplay = true;
               }
               else if(exactChangeNeeded == true){
                   ReturnCoins();
               }
               else if(Credit >= 100){
                   MakeChange(Credit-100);
                   PurchaseHelper(item);
                   colaStock--;
               }
               else{
                   Display = "PRICE: 100";
                   tempDisplay = true;
               }
           }
           else if(item == "Candy"){
               if(candyStock == 0){
                   Display = "SOLD OUT";
                   tempDisplay = true;
               }
               else if(exactChangeNeeded == true){
                   ReturnCoins();
               }
               else if(Credit >= 65){
                   MakeChange(Credit-65);
                   PurchaseHelper(item);
                   candyStock--;
               }
               else{
                   Display = "PRICE: 65";
                   tempDisplay = true;
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
