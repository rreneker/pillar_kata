using System;
using System.Collections.Generic;

namespace pillar_kata
{
    public class VendingMachine{
       public string Display;
       public List<string> CoinReturn;
       public List<string> CurrentCoins;
       public List<string> PurchasedProducts;
       public int Credit;

       private bool tempDisplay;

       private int colaStock;
       private int candyStock;
       private int chipsStock;

       public VendingMachine()
       {
           Display = "INSERT COIN";
           CoinReturn = new List<string>();
           CurrentCoins = new List<string>();
           PurchasedProducts = new List<string>();
           Credit = 0;
           tempDisplay = false;
           colaStock = 10;
           candyStock = 10;
           chipsStock = 10;
       }
       public VendingMachine(int cola, int chips, int candy): this(){
           colaStock = cola;
           candyStock = candy;
           chipsStock = chips;
       }

       public void AddCoin(string coin){
           if(coin == "Quarter"){
               Credit += 25;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == "Dime"){
               Credit += 10;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == "Nickel"){
               Credit += 5;
               CurrentCoins.Add(coin);
               Display = "CREDIT: "+Credit.ToString();
           }
           else if(coin == "Penny"){
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
               if(Credit > 0){
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
           while(credit != 0){
               if(credit >= 25){
                   CoinReturn.Add("Quarter");
                   credit -= 25;
               }
               else if(credit >= 10){
                   CoinReturn.Add("Dime");
                   credit -= 10;
               }
               else{
                   CoinReturn.Add("Nickel");
                   credit -= 5;
               }
           }
       }
       public void Buy(string item){
           if(item == "Chips"){
               if(chipsStock == 0){
                   Display = "SOLD OUT";
                   tempDisplay = true;
               }
               else if(Credit >= 50){
                   MakeChange(Credit-50);
                   PurchaseHelper(item);    
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
               else if(Credit >= 100){
                   MakeChange(Credit-100);
                   PurchaseHelper(item);
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
               else if(Credit >= 65){
                   MakeChange(Credit-65);
                   PurchaseHelper(item);
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
