using System;

namespace pillar_kata
{
    public class VendingMachine{
       public string Display;
       public string CoinReturn;
       public int Credit;

       public VendingMachine()
       {
           Display = "INSERT COIN";
           CoinReturn = "";
           Credit = 0;
       }

       public void AddCoin(string coin){
           if(coin == "Quarter"){
               Credit += 25;
               Display = "CREDIT: ."+Credit.ToString();
           }
           else if(coin == "Dime"){
               Credit += 10;
               Display = "CREDIT: ."+Credit.ToString();
           }
           else if(coin == "Nickel"){
               Credit += 5;
               Display = "CREDIT: ."+Credit.ToString();
           }
           else if(coin == "Penny"){
               CoinReturn = coin;
           }
           
       }
   }
}
