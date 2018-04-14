using System;
using System.Collections.Generic;

namespace pillar_kata
{
    public class VendingMachine{
       public string Display;
       public List<string> CoinReturn;
       public List<string> CurrentCoins;
       public int Credit;

       private bool tempDisplay;

       public VendingMachine()
       {
           Display = "INSERT COIN";
           CoinReturn = new List<string>();
           CurrentCoins = new List<string>();
           Credit = 0;
           tempDisplay = false;
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
               Display = "CREDIT: "+Credit.ToString();
           }
           return ReturnValue;
       }

       public void Buy(string item){
           if(item == "Chips"){
               Display = "PRICE: 50";
               tempDisplay = true;
           }
           else if(item == "Cola"){
               Display = "PRICE: 100";
               tempDisplay = true;
           }
           else if(item == "Candy"){
               Display = "PRICE: 65";
               tempDisplay = true;
           }
       }
   }
}
