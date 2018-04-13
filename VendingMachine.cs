using System;

namespace pillar_kata
{
    public class VendingMachine{
       public string Display;
       public string CoinReturn;

       public VendingMachine()
       {
           Display = "INSERT COIN";
           CoinReturn = "";
       }

       public void AddCoin(string coin){
           CoinReturn = coin;
       }
   }
}
