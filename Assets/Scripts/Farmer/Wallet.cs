using Assets.Scripts.Utilities.DayAndWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer
{
    [Serializable]
    public class Wallet
    {
        private decimal _amount;
        
        public Wallet()
        {

        }

        
        public decimal ModifyWallet(decimal amount)
        {
            this._amount += amount;

            if(this._amount <= 0)
            {
                this._amount = 0;    
            }

            DayAndWeatherManager.instance.UpdateWalletInfo(this._amount);

            return this._amount;
        }

        public decimal Amount
        {
            get { return this._amount; }
        }
    }
}
