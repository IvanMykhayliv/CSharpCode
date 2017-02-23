using System;

namespace Money
{
    public static class Currency
    {
        //Fire an event whenever the currency number changes
        //So that other objects know when the currency changes
        public delegate void CurrencyChangeHandler(int currentCurrency);
        public static event CurrencyChangeHandler CurrencyChanged;

        //this class is static so that as many pieces that have this code always reference the same object
        // Defaults to zero to make sure it always works.
        private static int currency = 0;

        public static void SetCurrency(int startingCurrency)
        {
            if (CurrencyChanged != null)
                CurrencyChanged(currency);
            // requires a value for initilizaition for if we want different maps to start with more or less currency
            currency = startingCurrency;
        }

        public static int getCurrency()
        {
            // other things might want to know how much money the player has
            return currency;
        }

        public static void increaseCurrency(int windfall)
        {
            // for enemy drops, selling towers, base upgrades, etc.
            currency += windfall;
            if (CurrencyChanged != null)
                CurrencyChanged(currency);
        }

        public static bool spendCurrency(int price)
        {
            //code interface for spending currency
            //the boolean return is to tell the code that calls this function if the operation is valid.
            if (price <= currency)
            {
                currency -= price;
                if (CurrencyChanged != null)
                    CurrencyChanged(currency);
                return true;
            }
            return false;
        }
    }
}
